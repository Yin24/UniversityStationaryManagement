using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSISTeam2.Classes.EFFServices
{
    public class TenderListService : ITenderListService
    {
        public void RedoRankingsForTenderList()
        {
            // Get all items for this year
            // Group by item code
            // For each item code, sort its contents by price
            // With the sorted, rank in order 1, 2, 3
            // Save changes

            using (SSISEntities context = new SSISEntities())
            {
                // For all item codes that are not set, go and add things to stuff
                int currentYear = DateTime.Now.Year;

                var groups = context.Tender_List_Details
                    .Where(w => w.Tender_List.tender_date.Year == currentYear)
                    .GroupBy(x => x.item_code)
                    .ToList();

                foreach (var group in groups)
                {
                    var sorted = group.OrderBy(o => o.price).ToList();

                    int rank = 1;
                    foreach (var item in sorted)
                    {
                        item.rank = rank;
                        rank++;
                    }
                }

                context.SaveChanges();
            }
        }
    }
}