using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSISTeam2.Classes.Models;
using SSISTeam2.Classes.Exceptions;

namespace SSISTeam2.Classes.EFFServices
{
    public class RetrievalServiceOld //: IRetrievalService
    {
        private SSISEntities context;
        public RetrievalServiceOld(SSISEntities context)
        {
            this.context = context;
        }
        public RetrievalModel findLatestRetrievalsByRequestId(int requestId)
        {
            // Get all allocated: depending on:
            // Determine there's any latest retrieval
            // Find the difference with the previous retrieval, to see how much to fulfill

            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            Request efRequest = context.Requests
                .Where(x => x.request_id == requestId
                            && (x.current_status == RequestStatus.APPROVED
                            || x.current_status == RequestStatus.PART_DISBURSED)
                ).First();

            if (efRequest == null)
            {
                throw new ItemNotFoundException("No records exist");
            }

            IEnumerable<IGrouping<string, Request_Event>> events = efRequest.Request_Details.SelectMany(x => x.Request_Event).GroupBy(g => g.Request_Details.item_code).ToList();

            Dictionary<ItemModel, int> itemsToFulfill = new Dictionary<ItemModel, int>();

            foreach (IGrouping<string, Request_Event> eventItem in events)
            {
                // Grouping:
                // A101
                // - Approved, 10
                // - Allocated, 9
                // A102
                // - Approved, 10
                List<Request_Event> latestRetrieval = eventItem.Where(x => x.status == EventStatus.RETRIEVED).OrderBy(o => o.date_time).ToList();
                if (latestRetrieval.Count == 0) continue;

                int quantityToFulfil = 0;

                if (latestRetrieval.Count > 1)
                {
                    Request_Event last = latestRetrieval.Last();
                    Request_Event secondLast = latestRetrieval[latestRetrieval.Count - 2];

                    quantityToFulfil = last.quantity - secondLast.quantity;
                }
                else
                {
                    quantityToFulfil = latestRetrieval.Last().quantity;
                }

                Stock_Inventory inv = context.Stock_Inventory.Find(eventItem.Key);
                itemsToFulfill.Add(new ItemModel(inv), quantityToFulfil);
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

            if (efRequests.Count == 0)
            {
                //throw new ItemNotFoundException("No records exist");
                return new RetrievalModelCollection(new List<RetrievalModel>());
            }

            List<RetrievalModel> results = new List<RetrievalModel>();
            foreach (var efRequest in efRequests)
            {
                RetrievalModel retrieval = findLatestRetrievalsByRequestId(efRequest.request_id);
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
                return null;
            }

            List<RetrievalModel> results = new List<RetrievalModel>();
            foreach (var efRequest in efRequests)
            {
                RetrievalModel retrieval = findLatestRetrievalsByRequestId(efRequest.request_id);
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
                return null;
            }

            List<RetrievalModel> results = new List<RetrievalModel>();
            foreach (var efRequest in efRequests)
            {
                RetrievalModel retrieval = findLatestRetrievalsByRequestId(efRequest.request_id);
                if (retrieval == null) continue; // SKIP
                results.Add(retrieval);
            }

            return new RetrievalModelCollection(results);
        }

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
    }
}