using SSISTeam2.Classes.EFFacades;
using SSISTeam2.Classes.Exceptions;
using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.EFFServices
{
    public class RequestService : IRequestService
    {
        private SSISEntities context;
        public RequestService(SSISEntities context)
        {
            this.context = context;
        }

        public RequestModel findRequestById(int requestId)
        {
            Request efRequest = context.Requests.Find(requestId);

            if (efRequest == null)
            {
                //return null;
                throw new ItemNotFoundException();
            }

            RequestModel request = new RequestModel(efRequest, efRequest.Department, ItemGetter._getPendingOrUpdatedItemsForRequest(efRequest));
            return request;
        }

        public RequestModelCollection getAllApprovedRequests()
        {
            List<Request> approved = context.Requests
                .Where(x => x.deleted != "Y"
                    && x.current_status == RequestStatus.APPROVED).ToList();
            if (approved.Count == 0)
            {
                throw new ItemNotFoundException();
            } else
            {
                List<RequestModel> records = approved.Select(x => new RequestModel(x, x.Department, ItemGetter._getItemsForRequest(x, RequestStatus.APPROVED))).ToList();

                return new RequestModelCollection(records);
            }
        }

        public RequestModelCollection getAllPendingRequestsOfDepartment(string departmentCode)
        {
            List<Request> pending = context.Requests
                .Where(x => x.deleted != "Y"
                    && (x.current_status == RequestStatus.PENDING || x.current_status == RequestStatus.UPDATED)
                    && x.dept_code == departmentCode
                    ).ToList();
            if (pending.Count == 0)
            {
                throw new ItemNotFoundException();
            }
            else
            {
                List<RequestModel> records = pending.Select(x => new RequestModel(x, ItemGetter._getPendingOrUpdatedItemsForRequest(x))).ToList();

                return new RequestModelCollection(records);
            }
        }

        public RequestModelCollection getAllRequestsByUser(string username)
        {
            List<Request> all = context.Requests
                .Where(x => x.deleted != "Y"
                    && x.username == username
                    ).ToList();
            if (all.Count == 0)
            {
                throw new ItemNotFoundException();
            }
            else
            {
                List<RequestModel> records = all.Select(x =>
                {
                    string status = x.current_status;
                    return new RequestModel(x, x.Department, ItemGetter._getItemsForRequest(x, status));
                }).ToList();

                return new RequestModelCollection(records);
            }
        }

        public RequestModelCollection getAllRequestsOfDepartment(string departmentCode)
        {
            List<Request> all = context.Requests
                .Where(x => x.deleted != "Y"
                    && x.dept_code == departmentCode
                ).ToList();
            if (all.Count == 0)
            {
                throw new ItemNotFoundException();
            }
            else
            {
                List<RequestModel> records = all.Select(x => new RequestModel(x, x.Department, ItemGetter._getPendingOrUpdatedItemsForRequest(x))).ToList();

                return new RequestModelCollection(records);
            }
        }

        private bool _setStatusOfRequest(RequestModel request, string currentUser, string status)
        {
            List<RequestModel> requests = new List<RequestModel>();
            requests.Add(request);
            int qty = _setStatusOfRequests(requests, currentUser, status);
            return qty == 0 ? false : true;
        }

        private int _setStatusOfRequests(List<RequestModel> requests, string currentUser, string status)
        {
            int added = 0;
            DateTime timestamp = DateTime.Now;
            using (var transaction = context.Database.BeginTransaction())
            {
                //try
                //{
                    foreach (RequestModel request in requests)
                    {
                        if (RequestStatus.requestHasHadStatus(request, status))
                        {
                            continue;
                        }
                        Request efRequest = context.Requests.Find(request.RequestId);
                        efRequest.current_status = status;
                        List<Request_Details> efDetails = context.Request_Details.Where(x => x.request_id == request.RequestId).ToList();
                        if (efRequest == null) throw new ItemNotFoundException("Could not find Request object");
                        if (efDetails.Count == 0) throw new ItemNotFoundException("Could not find Request Details objects");

                        foreach (Request_Details efDetail in efDetails)
                        {
                            // For each item, add a new event that is "_the_status"
                            Request_Event newEvent = new Request_Event();
                            newEvent.request_detail_id = efDetail.request_detail_id;

                            // get the quantity from the provided item quantity in the method arguments
                            newEvent.quantity = request.Items.Where(x => x.Key.ItemCode == efDetail.item_code).First().Value;
                            newEvent.status = status;
                            newEvent.username = currentUser;
                            newEvent.date_time = timestamp;
                            newEvent.deleted = "N";

                            context.Request_Event.Add(newEvent);
                            added++;
                        }
                    }
                //} catch (Exception exec)
                //{
                //    transaction.Rollback();
                //    throw exec;
                //}

                transaction.Commit();
            }
            return added;
        }

        public bool approveRequest(RequestModel request, string currentUser)
        {
            bool status = _setStatusOfRequest(request, currentUser, RequestStatus.APPROVED);
            FacadeFactory.getRequestMovementService(context).allocateRequest(request.RequestId, currentUser);
            return status;
        }

        public bool rejectRequest(RequestModel request, string currentUser)
        {
            return _setStatusOfRequest(request, currentUser, RequestStatus.REJECTED);
        }

        public bool saveNewRequest(RequestModel request)
        {
            DateTime timestamp = DateTime.Now;
            using (var transaction = context.Database.BeginTransaction())
            {
                try
            {
                    Request newRequest = new Request();
                    newRequest.current_status = RequestStatus.PENDING;
                    newRequest.deleted = "N";
                    newRequest.dept_code = request.Department.dept_code;
                    newRequest.reason = request.Reason;
                    newRequest.rejected = "N";
                    newRequest.rejected_reason = "";
                    newRequest.username = request.UserModel.Username;
                    newRequest.date_time = timestamp;

                    List<Request_Details> newDetails = new List<Request_Details>();
                    foreach (KeyValuePair<ItemModel, int> itemAndQty in request.Items)
                    {
                        Request_Details newDetail = new Request_Details();
                        newDetail.deleted = "N";
                        newDetail.item_code = itemAndQty.Key.ItemCode;
                        newDetail.orig_quantity = itemAndQty.Value;

                        Request_Event newEvent = new Request_Event();
                        newEvent.deleted = "N";
                        newEvent.date_time = timestamp;
                        newEvent.quantity = itemAndQty.Value;
                        newEvent.allocated = 0;
                        newEvent.not_allocated = 0;
                        newEvent.status = RequestStatus.PENDING;
                        newEvent.username = request.UserModel.Username;
                        
                        // Establish relationships
                        newDetail.Request_Event.Add(newEvent);
                        newRequest.Request_Details.Add(newDetail);
                    }

                    // Add to DB
                    context.Requests.Add(newRequest);
                }
                catch (Exception exec)
                {
                    transaction.Rollback();
                    throw exec;
                }

                transaction.Commit();
            }
            return true;
        }

        public bool updateRequestChanges(RequestModel newRequest)
        {
            if (RequestStatus.requestHasHadStatus(newRequest, RequestStatus.APPROVED))
            {
                throw new RequestAlreadyApprovedException("Already approved or rejected.");
                //return false;
            }
            DateTime timestamp = DateTime.Now;
            using (var transaction = context.Database.BeginTransaction())
            {
                //try
                //{
                Request targetRequest = context.Requests.Find(newRequest.RequestId);

                List<Request_Details> targetDetails = targetRequest.Request_Details.ToList();

                RequestModel targetRequestModel = findRequestById(newRequest.RequestId);
                foreach (var item in targetRequestModel.Items)
                {
                    if (!newRequest.Items.ContainsKey(item.Key))
                    { // New requestModel does not contain old key
                      // So add it in, but set quantity to 0
                        newRequest.Items.Add(item.Key, 0);
                    }
                }

                foreach (KeyValuePair<ItemModel, int> itemAndQty in newRequest.Items)
                {
                    //Request_Event newEvent = new Request_Event();
                    //newEvent.deleted = "N";
                    //newEvent.date_time = timestamp;
                    //newEvent.quantity = itemAndQty.Value;
                    //newEvent.status = EventStatus.UPDATED;
                    //newEvent.username = newRequest.UserModel.Username;

                    // Establish relationships
                    Request_Details targetDetail = targetDetails.Where(x => x.item_code == itemAndQty.Key.ItemCode && x.deleted != "Y").DefaultIfEmpty(null).FirstOrDefault();
                    if (targetDetail == null)
                    {
                        // Need to insert new detail and event
                        Request_Details newDetail = new Request_Details();
                        newDetail.deleted = "N";
                        newDetail.item_code = itemAndQty.Key.ItemCode;
                        newDetail.orig_quantity = itemAndQty.Value;

                        Request_Event newEvent = new Request_Event();
                        newEvent.deleted = "N";
                        newEvent.date_time = timestamp;
                        newEvent.quantity = itemAndQty.Value;
                        newEvent.status = EventStatus.PENDING;
                        newEvent.username = newRequest.UserModel.Username;

                        newDetail.Request_Event.Add(newEvent);
                        newDetail.request_id = newRequest.RequestId;
                        context.Request_Details.Add(newDetail);
                    }
                    else
                    {
                        // Edit db
                        context.Request_Details.Find(targetDetail.request_detail_id).orig_quantity = itemAndQty.Value;
                        Request_Event existingEvent = targetDetail.Request_Event.Where(w => w.deleted != "Y").First();
                        context.Request_Event.Find(existingEvent.request_event_id).quantity = itemAndQty.Value;
                        context.Request_Event.Find(existingEvent.request_event_id).date_time = timestamp;
                        context.Request_Event.Find(existingEvent.request_event_id).status = EventStatus.UPDATED;
                    }

                    //targetDetail.Request_Event.Add(newEvent);
                }

                // Update Status
                context.Requests.Find(newRequest.RequestId).current_status = RequestStatus.UPDATED;
                context.Requests.Find(newRequest.RequestId).reason = newRequest.Reason;
                //}
                //catch (Exception exec)
                //{
                //transaction.Rollback();
                //throw exec;
                //}

                transaction.Commit();
            }
            return true;
        }

        public bool setRequestToCancelled(int requestId, string username)
        {
            Request request = context.Requests.Find(requestId);
            if (request.current_status != RequestStatus.PENDING && request.current_status != RequestStatus.UPDATED)
            {
                throw new RequestAlreadyApprovedException("Already approved or rejected.");
                //return false;
            }
            DateTime timestamp = DateTime.Now;
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<Request_Details> targetDetails = request.Request_Details.ToList();
                    foreach (Request_Details detail in targetDetails)
                    {
                        Request_Event existingEvent = detail.Request_Event.Where(w => w.deleted != "Y").First();
                        if (existingEvent == null) continue;

                        context.Request_Event.Find(existingEvent.request_event_id).date_time = timestamp;
                        context.Request_Event.Find(existingEvent.request_event_id).status = EventStatus.CANCELLED;

                        //Request_Event newEvent = new Request_Event();
                        //newEvent.deleted = "N";
                        //newEvent.date_time = timestamp;
                        //newEvent.quantity = detail.Request_Event.OrderBy(o => o.date_time).Where(w => (w.status == EventStatus.UPDATED || w.status == EventStatus.PENDING) && w.deleted != "Y").Last().quantity;
                        //newEvent.status = EventStatus.CANCELLED;
                        //newEvent.username = username;

                        // Establish relationships
                        //newEvent.request_detail_id = detail.request_detail_id;

                        // Add to DB
                        //context.Request_Event.Add(newEvent);
                    }

                    // Update Status
                    context.Requests.Find(requestId).current_status = RequestStatus.CANCELLED;
                }
                catch (Exception exec)
                {
                    transaction.Rollback();
                    throw exec;
                }

                transaction.Commit();
            }
            return true;
        }
    }
}
