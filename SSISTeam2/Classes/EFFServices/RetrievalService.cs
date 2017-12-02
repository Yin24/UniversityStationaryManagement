using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSISTeam2.Classes.Models;
using SSISTeam2.Classes.Exceptions;

namespace SSISTeam2.Classes.EFFServices
{
    public class RetrievalService : IRetrievalService
    {
        private SSISEntities context;
        public RetrievalService(SSISEntities context)
        {
            this.context = context;
        }
        public RetrievalModelCollection getAllRetrievingByClerk(string currentUser)
        {
            List<Request> efRequests = context.Requests
                .Where(x => x.current_status == RequestStatus.APPROVED
                            || x.current_status == RequestStatus.PART_DISBURSED
                ).ToList();

            List<RetrievalModel> results = new List<RetrievalModel>();
            if (efRequests.Count == 0)
            {
                //throw new ItemNotFoundException("No records exist");
                return new RetrievalModelCollection(results);
            }
            int count = 0;
            foreach (var efRequest in efRequests)
            {
                count++;
                RetrievalModel retrieval = findLatestRetrievingByRequestId(efRequest.request_id, currentUser);
                if (retrieval == null) continue; // SKIP
                results.Add(retrieval);
            }

            return new RetrievalModelCollection(results);
        }

        public RetrievalModel findLatestRetrievingByRequestId(int requestId, string currentUser)
        { // For at the warehouse

            // Get all allocated: depending on:
            // Determine there's any latest retrieval
            // Find the difference with the previous retrieval, to see how much to fulfill

            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            Request efRequest = context.Requests
                .Where(x => x.request_id == requestId
                            && (x.current_status == RequestStatus.APPROVED
                            || x.current_status == RequestStatus.PART_DISBURSED)
                            && x.deleted != "Y"
                ).First();

            if (efRequest == null)
            {
                throw new ItemNotFoundException("No records exist");
            }

            Dictionary<ItemModel, int> itemsToFulfill = new Dictionary<ItemModel, int>();

            List<Request_Details> details = efRequest.Request_Details.ToList();
            foreach (var detail in details)
            {
                if (detail.deleted == "Y")
                {
                    continue;
                }

                List<Request_Event> events = detail.Request_Event.Where(w => w.deleted != "Y").ToList();

                // If there are no events for some reason, SKIP
                if (events.Count == 0) continue;

                Request_Event eventItem = events.First();

                // If the event does not have anything allocated to it, SKIP
                if (eventItem.allocated == 0) continue;

                // Only add if it's retrieving AND it was by the currentUser
                if (eventItem.status == EventStatus.RETRIEVING && eventItem.username == currentUser && eventItem.allocated.HasValue)
                {
                    Stock_Inventory s = detail.Stock_Inventory;
                    itemsToFulfill.Add(new ItemModel(s), eventItem.allocated.Value);
                }
            }

            if (itemsToFulfill.Count == 0)
            {
                return null;
            }

            RetrievalModel retrieval = new RetrievalModel(efRequest, itemsToFulfill);

            return retrieval;
        }

        public RetrievalModelCollection getAllRetrieved()
        {
            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            List<Request> efRequests = context.Requests
                .Where(x => x.current_status == RequestStatus.APPROVED
                            || x.current_status == RequestStatus.PART_DISBURSED
                ).ToList();

            List<RetrievalModel> results = new List<RetrievalModel>();
            if (efRequests.Count == 0)
            {
                //throw new ItemNotFoundException("No records exist");
                return new RetrievalModelCollection(results);
            }

            foreach (var efRequest in efRequests)
            {
                RetrievalModel retrieval = null;//findLatestRetrievalsByRequestId(efRequest.request_id);
                if (retrieval == null) continue; // SKIP
                results.Add(retrieval);
            }

            return new RetrievalModelCollection(results);
        }

        public RetrievalModelCollection getAllRetrievedForCollectionPoint(int collectionPointId)
        {
            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            List<Request> efRequests = context.Requests
                .Where(x =>
                    (x.current_status == RequestStatus.APPROVED
                    || x.current_status == RequestStatus.PART_DISBURSED)
                    && x.Department.collection_point == collectionPointId
                ).ToList();

            if (efRequests.Count == 0)
            {
                //throw new ItemNotFoundException("No records exist");
                return new RetrievalModelCollection(new List<RetrievalModel>());
            }

            List<RetrievalModel> results = new List<RetrievalModel>();
            foreach (var efRequest in efRequests)
            {
                RetrievalModel retrieval = null;//findLatestRetrievalsByRequestId(efRequest.request_id);
                if (retrieval == null) continue; // SKIP
                results.Add(retrieval);
            }

            return new RetrievalModelCollection(results);
        }

        public RetrievalModelCollection getAllRetrievedFromDepartment(string deptCode)
        {
            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            List<Request> efRequests = context.Requests
                .Where(x =>
                    (x.current_status == RequestStatus.APPROVED
                    || x.current_status == RequestStatus.PART_DISBURSED)
                    && x.dept_code == deptCode
                ).ToList();

            if (efRequests.Count == 0)
            {
                //throw new ItemNotFoundException();
                return new RetrievalModelCollection(new List<RetrievalModel>());
            }

            List<RetrievalModel> results = new List<RetrievalModel>();
            foreach (var efRequest in efRequests)
            {
                RetrievalModel retrieval = null;//findLatestRetrievalsByRequestId(efRequest.request_id);
                if (retrieval == null) continue; // SKIP
                results.Add(retrieval);
            }

            return new RetrievalModelCollection(results);
        }
        /*
        public int markRequestAsRetrieved(RequestModel toAllocate, string currentUser)
        {
            int added = 0;
            // For each item provided, find the related item in DB, and add an event to it

            if (RequestStatus.requestHasHadStatus(toAllocate, RequestStatus.DISBURSED))
            {
                throw new RequestAlreadyApprovedException("Already fully disbursed.");
                //return false;
            }
            DateTime timestamp = DateTime.Now;
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Request targetRequest = context.Requests.Find(toAllocate.RequestId);

                    List<Request_Details> targetDetails = targetRequest.Request_Details.ToList();
                    foreach (KeyValuePair<ItemModel, int> itemAndQty in toAllocate.Items)
                    {
                        if (itemAndQty.Value == 0)
                        {
                            // No items to retrieve
                            continue;
                        }
                        Request_Event newEvent = new Request_Event();
                        newEvent.deleted = "N";
                        newEvent.date_time = timestamp;
                        newEvent.quantity = itemAndQty.Value;
                        newEvent.status = EventStatus.RETRIEVED;
                        newEvent.username = toAllocate.UserModel.Username;

                        // Establish relationships
                        Request_Details targetDetail = targetDetails.Where(x => x.item_code == itemAndQty.Key.ItemCode).First();
                        targetDetail.Request_Event.Add(newEvent);
                        added++;
                    }
                }
                catch (Exception exec)
                {
                    transaction.Rollback();
                    throw exec;
                }

                transaction.Commit();
            }
            return added;
        }
        */
    }
}