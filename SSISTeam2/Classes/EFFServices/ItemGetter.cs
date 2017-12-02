using SSISTeam2.Classes.Exceptions;
using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSISTeam2.Classes.EFFServices
{
    public class ItemGetter
    {
        public static Dictionary<ItemModel, int> _getPendingOrUpdatedItemsForRequest(Request efRequest)
        {
            try
            {
                List<Request_Details> details = efRequest.Request_Details.Where(x => x.deleted != "Y").ToList();

                Dictionary<ItemModel, int> itemsAndQuantities = new Dictionary<ItemModel, int>();

                //string status = RequestStatus.PENDING;

                //bool wasUpdated = false;

                details.ForEach(x =>
                {
                    // Get the latest event that is either pending or updated
                    Request_Event eventItem = x.Request_Event
                        .Where(e => e.deleted != "Y"
                        && (e.status == RequestStatus.PENDING || e.status == RequestStatus.UPDATED)
                        )
                        .OrderBy(o => o.date_time)
                        .Last();

                    itemsAndQuantities.Add(new ItemModel(x.Stock_Inventory), eventItem.quantity);
                });

                return itemsAndQuantities;

            }
            catch (NullReferenceException nullExec)
            {
                throw new ItemNotFoundException("Item not found", nullExec);
            }
        }

        public static Dictionary<ItemModel, int> _getItemsForRequest(Request efRequest, string status)
        {
            try
            {
                List<Request_Details> details = efRequest.Request_Details.Where(x => x.deleted != "Y").ToList();

                Dictionary<ItemModel, int> itemsAndQuantities = new Dictionary<ItemModel, int>();

                details.ForEach(x =>
                {
                    Request_Event eventItem = x.Request_Event.Where(e => e.deleted != "Y").OrderBy(o => o.date_time).Last();

                    int qty = 0;
                    if (eventItem != null)
                    {
                        qty = eventItem.quantity;
                    }

                    itemsAndQuantities.Add(new ItemModel(x.Stock_Inventory), qty);
                });

                return itemsAndQuantities;

            }
            catch (NullReferenceException nullExec)
            {
                throw new ItemNotFoundException("NULLEXEC: Item not found", nullExec);
            }
        }
    }
}