using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSISTeam2.Classes.Models;
using SSISTeam2.Classes.Exceptions;

namespace SSISTeam2.Classes.EFFServices
{
    public class DisbursementService : IDisbursementService
    {
        private SSISEntities context;
        public DisbursementService(SSISEntities context)
        {
            this.context = context;
        }
        public DisbursementModel findLatestPossibleDisbursingByRequestId(int requestId)
        { // For generating disbursement forms (all those as retrieved)

            return _findLatestBetweenStatuses(requestId, EventStatus.RETRIEVED, EventStatus.DISBURSING, null);
        }

        public DisbursementModel findLatestSignOffsByRequestId(int requestId, string currentUser)
        { // For signing off forms (all disbursing ones)

            return _findLatestBetweenStatuses(requestId, EventStatus.DISBURSING, EventStatus.DISBURSED, currentUser);
        }

        private DisbursementModel _findLatestBetweenStatuses(int requestId, string fromStatus, string toStatus, string currentUser)
        { // For signing off forms (all disbursing ones)
            Request efRequest = context.Requests
                .Where(x => x.request_id == requestId
                            && (x.current_status == RequestStatus.APPROVED
                            || x.current_status == RequestStatus.PART_DISBURSED)
                ).First();

            if (efRequest == null)
            {
                throw new ItemNotFoundException("No records exist");
            }

            Dictionary<ItemModel, int> itemsToFulfill = new Dictionary<ItemModel, int>();

            List<Request_Details> details = efRequest.Request_Details.ToList();
            foreach (var detail in details)
            {
                //int itemQty = 0;
                if (detail.deleted == "Y")
                {
                    continue;
                }

                List<Request_Event> events = detail.Request_Event.Where(w => w.deleted != "Y").ToList();

                // If there are no events for some reason, SKIP
                if (events.Count == 0) continue;

                Request_Event eventItem = events.First();

                // If the event does not have anything allocated to it, SKIP
                if (eventItem.allocated.HasValue && eventItem.allocated.Value == 0) continue;



                // Only add if it's retrieving AND it was by the currentUser
                if (eventItem.status == fromStatus && eventItem.allocated.HasValue)
                {
                    // currentUser was specified, skip if it's not by them
                    if (currentUser != null
                        && eventItem.username != currentUser)
                    {
                        continue;
                    }

                    Stock_Inventory s = detail.Stock_Inventory;
                    itemsToFulfill.Add(new ItemModel(s), eventItem.allocated.Value);
                }

            }

            if (itemsToFulfill.Count == 0)
            {
                return null;
            }

            DisbursementModel retrieved = new DisbursementModel(efRequest, itemsToFulfill);

            return retrieved;
        }

        public DisbursementModelCollection getAllThatCanBeSignedOff(string currentUser)
        {
            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            List<Request> efRequests = context.Requests
                .Where(x => x.current_status == RequestStatus.APPROVED
                            || x.current_status == RequestStatus.PART_DISBURSED
                            && x.deleted != "Y"
                ).ToList();

            List<DisbursementModel> results = new List<DisbursementModel>();

            if (efRequests.Count == 0)
            {
                //throw new ItemNotFoundException("No records exist");
                return new DisbursementModelCollection(results);
            }

            foreach (var efRequest in efRequests)
            {
                DisbursementModel disbursing = findLatestSignOffsByRequestId(efRequest.request_id, currentUser);
                if (disbursing == null) continue; // SKIP
                results.Add(disbursing);
            }

            return new DisbursementModelCollection(results);
        }

        public DisbursementModelCollection getAllThatWereDisbursed()
        {
            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            List<Request> efRequests = context.Requests
                .Where(x => (x.current_status == RequestStatus.DISBURSED
                        || x.current_status == RequestStatus.PART_DISBURSED)
                        && x.deleted != "Y"
                ).ToList();

            List<DisbursementModel> results = new List<DisbursementModel>();

            if (efRequests.Count == 0)
            {
                //throw new ItemNotFoundException("No records exist");
                return new DisbursementModelCollection(results);
            }

            foreach (var efRequest in efRequests)
            {
                Dictionary<ItemModel, int> items = new Dictionary<ItemModel, int>();

                var details = efRequest.Request_Details.Where(x => x.deleted != "Y");

                var events = details.Select(s => s.Request_Event.OrderByDescending(o => o.date_time)
                    .Where(w => w.status == EventStatus.DISBURSED).DefaultIfEmpty(null).FirstOrDefault());

                //var list = efRequest.Request_Details
                //    .Select(s => s.Request_Event.OrderByDescending(o => o.date_time)
                //    .Where(w => w.status == EventStatus.DISBURSED)
                //    .First());

                foreach (var item in events) {

                    if (item == null) continue;

                    items.Add(new ItemModel(item.Request_Details.Stock_Inventory), item.quantity);
                }

                DisbursementModel disbursed = new DisbursementModel(efRequest, items);
                if (disbursed == null) continue; // SKIP
                results.Add(disbursed);
            }

            return new DisbursementModelCollection(results);
        }

        public DisbursementModelCollection getAllSignOffsForCollectionPoint(int collectionPointId, string currentUser)
        {
            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            List<Request> efRequests = context.Requests
                .Where(x =>
                    (x.current_status == RequestStatus.DISBURSED
                    || x.current_status == RequestStatus.PART_DISBURSED)
                    && x.Department.collection_point == collectionPointId
                    && x.deleted != "Y"
                ).ToList();

            List<DisbursementModel> results = new List<DisbursementModel>();

            if (efRequests.Count == 0)
            {
                //throw new ItemNotFoundException("No records exist");
                return new DisbursementModelCollection(results);
            }

            foreach (var efRequest in efRequests)
            {
                DisbursementModel disbursing = findLatestSignOffsByRequestId(efRequest.request_id, currentUser);
                if (disbursing == null) continue; // SKIP
                results.Add(disbursing);
            }

            return new DisbursementModelCollection(results);
        }

        public DisbursementModelCollection getAllPossibleDisbursementsForCollectionPoint(int collectionPointId)
        {
            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            List<Request> efRequests = context.Requests
                .Where(x =>
                    (x.current_status == RequestStatus.DISBURSED
                    || x.current_status == RequestStatus.PART_DISBURSED)
                    && x.Department.collection_point == collectionPointId
                    && x.deleted != "Y"
                ).ToList();

            List<DisbursementModel> results = new List<DisbursementModel>();

            if (efRequests.Count == 0)
            {
                //throw new ItemNotFoundException("No records exist");
                return new DisbursementModelCollection(results);
            }

            foreach (var efRequest in efRequests)
            {
                DisbursementModel disbursement = findLatestPossibleDisbursingByRequestId(efRequest.request_id);
                if (disbursement == null) continue; // SKIP
                results.Add(disbursement);
            }

            return new DisbursementModelCollection(results);
        }

        public DisbursementModelCollection getAllPossibleDisbursements()
        {
            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            List<Request> efRequests = context.Requests
                .Where(x =>
                    (x.current_status == RequestStatus.APPROVED
                    || x.current_status == RequestStatus.PART_DISBURSED)
                    && x.deleted != "Y"
                ).ToList();

            List<DisbursementModel> results = new List<DisbursementModel>();

            if (efRequests.Count == 0)
            {
                //throw new ItemNotFoundException("No records exist");
                return new DisbursementModelCollection(results);
            }

            foreach (var efRequest in efRequests)
            {
                DisbursementModel disbursement = findLatestPossibleDisbursingByRequestId(efRequest.request_id);
                if (disbursement == null) continue; // SKIP
                results.Add(disbursement);
            }

            return new DisbursementModelCollection(results);
        }
        /*
        public DisbursementModelCollection getAllDisbursementsFromDepartment(string deptCode)
        {
           //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
           List<Request> efRequests = context.Requests
               .Where(x =>
                   (x.current_status == RequestStatus.DISBURSED
                   || x.current_status == RequestStatus.PART_DISBURSED)
                   && x.dept_code == deptCode
               ).ToList();

           if (efRequests.Count == 0)
           {
               //throw new ItemNotFoundException();
               return null;
           }

           List<DisbursementModel> results = new List<DisbursementModel>();
           foreach (var efRequest in efRequests)
           {
               DisbursementModel disbursements = findLatestDisbursementByRequestId(efRequest.request_id);
               if (disbursements == null) continue; // SKIP
               results.Add(disbursements);
           }

           return new DisbursementModelCollection(results);
        }
        */
    }
}