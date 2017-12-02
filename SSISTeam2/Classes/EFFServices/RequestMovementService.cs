using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSISTeam2.Classes.EFFServices
{
    public class RequestMovementService : IRequestMovementService
    {
        private SSISEntities context;
        public RequestMovementService(SSISEntities context)
        {
            this.context = context;
        }
        public List<string> getRequestNonAllocatedItemCodes(int requestId)
        {
            List<string> outstandingItems = new List<string>();
            Request request = context.Requests.Find(requestId);

            // Get all the latest approved
            List<Request_Event> events = request.Request_Details.Select(s => s.Request_Event.Where(w => w.status == EventStatus.APPROVED && w.deleted != "Y").OrderBy(o => o.date_time).Last()).ToList();

            foreach (var ev in events)
            {
                if (ev.deleted == "Y") continue;
                if (ev.quantity > 0)
                {
                    outstandingItems.Add(ev.Request_Details.item_code);
                }
            }
            return outstandingItems;
        }
        public void allocateRequest(int requestId, string currentUser)
        {
            DateTime now = DateTime.Now;
            // Get request
            Request targetRequest = context.Requests.Find(requestId);
            if (targetRequest.current_status != RequestStatus.APPROVED
                && targetRequest.current_status != RequestStatus.PART_DISBURSED)
            {
                return;
            }
            // Get items
            List<Request_Details> targetDetails = targetRequest.Request_Details.ToList();
            // Get approved quantities
            foreach (var detail in targetDetails)
            {
                if (detail.deleted == "Y") continue;

                Request_Event approvedEvent = detail.Request_Event.Where(w => w.status == EventStatus.APPROVED).DefaultIfEmpty(null).FirstOrDefault();

                if (approvedEvent == null) continue;

                ItemModel item = new ItemModel(context.Stock_Inventory.Find(approvedEvent.Request_Details.item_code));

                int approvedQty = approvedEvent.quantity;
                int availableQty = item.AvailableQuantity;
                int newApprovedQty = 0;
                int newAllocQty = 0;

                // See if there's available
                // Allocate whatever you can
                if (availableQty >= approvedQty)
                {
                    // Can allocate all
                    newAllocQty = approvedQty;
                    newApprovedQty = 0;
                } else if (availableQty > 0)
                {
                    // Can allocate some
                    newAllocQty = availableQty;
                    newApprovedQty = approvedQty - newAllocQty;
                } else
                {
                    // Cannot allocate at all, skip this item
                    continue;
                }

                // Update event
                int eventId = approvedEvent.request_event_id;
                context.Request_Event.Find(eventId).not_allocated = newApprovedQty;
                context.Request_Event.Find(eventId).allocated = newAllocQty;

                // Save new events
                //Request_Event allocEv = _newRequestEvent(now, newAllocQty, detail.request_detail_id, currentUser, EventStatus.ALLOCATED);
                //Request_Event approvedEv = _newRequestEvent(now, newApprovedQty, detail.request_detail_id, currentUser, EventStatus.APPROVED);

                //context.Request_Event.Add(allocEv);
                //context.Request_Event.Add(approvedEv);
            }
        }

        public void moveFromAllocatedToRetrieving(int requestId, List<string> itemCodes, string currentUser)
        {
            _moveToTransient(requestId, itemCodes, currentUser, EventStatus.ALLOCATED, EventStatus.RETRIEVING, resetAllocQty: true);
        }
        public void moveFromRetrievedToDisbursing(int requestId, List<string> itemCodes, string currentUser)
        {
            _moveToTransient(requestId, itemCodes, currentUser, EventStatus.RETRIEVED, EventStatus.DISBURSING, resetAllocQty: true);
        }

        public void moveFromRetrievingToRetrieved(int requestId, Dictionary<string, int> items, string currentUser)
        {
            Request request = context.Requests.Find(requestId);
            _moveFromTransient(request, items, currentUser, EventStatus.RETRIEVING, EventStatus.RETRIEVED);
        }
        public void moveFromDisbursingToDisbursed(int requestId, Dictionary<string, int> items, string currentUser)
        {
            Request request = context.Requests.Find(requestId);

            // Check if all were disbursed
            bool fullyDisbursed = _moveFromTransient(request, items, currentUser, EventStatus.DISBURSING, EventStatus.DISBURSED);

            if (fullyDisbursed)
            {
                // Check if request has any outstanding items
                Request requestCheck = context.Requests.Find(requestId);

                bool allFulfilled = requestCheck.Request_Details.ToList()
                            .TrueForAll(detail =>
                                detail.Request_Event.ToList()
                                    .TrueForAll(e => e.status == EventStatus.DISBURSED)
                            );

                if (allFulfilled)
                {
                    request.current_status = RequestStatus.DISBURSED;
                } else
                {
                    request.current_status = RequestStatus.PART_DISBURSED;
                }
            } else
            {
                request.current_status = RequestStatus.PART_DISBURSED;
            }
        }

        private Request_Event _newRequestEvent(DateTime dateTime, int quantity, int notAllocQty, int allocQty, int detailId, string username, string status)
        {
            Request_Event newEv = new Request_Event();
            newEv.date_time = dateTime;
            newEv.deleted = "N";
            newEv.quantity = quantity;
            newEv.request_detail_id = detailId;
            newEv.username = username;
            newEv.status = status;
            newEv.not_allocated = notAllocQty;
            newEv.allocated = allocQty;

            return newEv;
        }

        private void _moveToTransient(int requestId, List<string> itemCodes, string currentUser, string fromStatus, string toStatus, bool resetAllocQty)
        {
            DateTime now = DateTime.Now;
            Request request = context.Requests.Find(requestId);

            List<Request_Details> details = request.Request_Details.ToList();

            foreach (var detail in details)
            {
                if (detail.deleted == "Y") continue;

                if (!itemCodes.Contains(detail.item_code)) continue;

                // Get the latest allocated & approved
                //Request_Event alloc = detail.Request_Event.Where(w => w.status == EventStatus.ALLOCATED && w.deleted != "Y").OrderBy(o => o.date_time).Last();
                //Request_Event approv = detail.Request_Event.Where(w => w.status == EventStatus.APPROVED && w.deleted != "Y").OrderBy(o => o.date_time).Last();
                // Get latest nonTransient
                Request_Event nonTransient = detail.Request_Event.Where(w => w.status == fromStatus && w.deleted != "Y").OrderBy(o => o.date_time).Last();

                if (nonTransient == null) continue;

                int eventId = nonTransient.request_event_id;

                context.Request_Event.Find(eventId).status = toStatus;
                context.Request_Event.Find(eventId).date_time = now;
                context.Request_Event.Find(eventId).username = currentUser;
                //int allocQty = resetAllocQty ? 0 : alloc.quantity;

                //Request_Event transientEv = _newRequestEvent(now, nonTransient.quantity, detail.request_detail_id, currentUser, toStatus);
                //Request_Event allocEv = _newRequestEvent(now, allocQty, detail.request_detail_id, currentUser, EventStatus.ALLOCATED);
                //Request_Event approvedEv = _newRequestEvent(now, approv.quantity, detail.request_detail_id, currentUser, EventStatus.APPROVED);

                //context.Request_Event.Add(transientEv);
                //context.Request_Event.Add(allocEv);
                //context.Request_Event.Add(approvedEv);
            }
        }

        private bool _moveFromTransient(Request request, Dictionary<string, int> items, string currentUser, string fromStatus, string toStatus)
        {
            bool wasFullyAllocated = true;

            DateTime now = DateTime.Now;

            List<Request_Details> details = request.Request_Details.ToList();

            foreach (var detail in details)
            {
                if (detail.deleted == "Y") continue;
                var itemCode = detail.item_code;
                // Match item codes
                var itemMatches = items.Where(w => w.Key == itemCode);

                KeyValuePair<string, int> item;

                // If there are no matches, skip this detail
                if (itemMatches.Count() == 0) continue;

                item = itemMatches.First();
                // Quantity to move to nonTransient
                var quantity = item.Value;

                // Get the latest allocated & approved & transient
                //Request_Event alloc = detail.Request_Event.Where(w => w.status == EventStatus.ALLOCATED && w.deleted != "Y").OrderBy(o => o.date_time).Last();
                //Request_Event approv = detail.Request_Event.Where(w => w.status == EventStatus.APPROVED && w.deleted != "Y").OrderBy(o => o.date_time).Last();
                Request_Event transient = detail.Request_Event.Where(w => w.status == fromStatus && w.deleted != "Y").First();

                if (transient == null) continue;

                int initialQty = transient.quantity;

                int newAllocQty = quantity;
                int newNonAllocQty = initialQty - quantity;


                // Check if the quantity was changed
                if (newNonAllocQty > 0) wasFullyAllocated = false;

                context.Request_Event.Find(transient.request_event_id).allocated = newAllocQty;
                context.Request_Event.Find(transient.request_event_id).not_allocated = newNonAllocQty;
                context.Request_Event.Find(transient.request_event_id).status = toStatus;
                context.Request_Event.Find(transient.request_event_id).date_time = now;
                context.Request_Event.Find(transient.request_event_id).username = currentUser;

                if (toStatus == EventStatus.DISBURSED)
                {
                    // If moving to disbursed, we need to minus the Stock_Inventory current_quantity
                    // Get the relevant Stock_Inv
                    //Stock_Inventory stock = context.Stock_Inventory.Find(detail.item_code);
                    // newAllocQty is what was finally minused off. So:

                    context.Stock_Inventory.Find(detail.item_code).current_qty -= newAllocQty;
                }
            }
            return wasFullyAllocated;

            // Was in ForEach loop previously:
            //int shortfall = transient.quantity - quantity;
            //if (shortfall > 0)
            //{
            //    // There was a discrepany, so try to allocate
            //    // Find out how much system can allocate
            //    int availableQty = item.Key.AvailableQuantity;
            //    int canAllocateQty = availableQty - shortfall;
            //    if (canAllocateQty >= 0)
            //    {
            //        // System can allocate all shortfall
            //        newAllocQty = shortfall;
            //        newNonAllocQty = 0;
            //    }
            //    if (canAllocateQty < 0)
            //    {
            //        // System can allocate some
            //        newAllocQty = availableQty;
            //        newNonAllocQty = shortfall - newAllocQty;
            //    }
            //}

            //Request_Event nonTransientEv = _newRequestEvent(now, quantity, detail.request_detail_id, currentUser, EventStatus.RETRIEVED);
            //Request_Event allocEv = _newRequestEvent(now, 0, detail.request_detail_id, currentUser, EventStatus.ALLOCATED);
            //Request_Event approvedEv = _newRequestEvent(now, approv.quantity, detail.request_detail_id, currentUser, EventStatus.APPROVED);

            //context.Request_Event.Add(nonTransientEv);
            //context.Request_Event.Add(allocEv);
            //context.Request_Event.Add(approvedEv);
        }
    }

    public static class Ext
    {
        public static void ZipDo<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second, Action<T1, T2> action)
        {
            using (var e1 = first.GetEnumerator())
            using (var e2 = second.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext())
                {
                    action(e1.Current, e2.Current);
                }
            }
        }

        public static void ZipDo<T1, T2, T3>(this IEnumerable<T1> first,
            IEnumerable<T2> second, IEnumerable<T3> third,
            Action<T1, T2, T3> action)
        {
            using (var e1 = first.GetEnumerator())
            using (var e2 = second.GetEnumerator())
            using (var e3 = third.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext())
                {
                    action(e1.Current, e2.Current, e3.Current);
                }
            }
        }
    }
}