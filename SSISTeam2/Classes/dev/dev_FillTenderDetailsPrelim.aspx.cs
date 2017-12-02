using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Classes.dev
{
    public partial class dev_FillTenderDetailsPrelim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnOldGo_Click(object sender, EventArgs e)
        {
            using (SSISEntities context = new SSISEntities())
            {
                // Get all tender list details currently
                // Get item codes from there
                // get price too
                // foreach tender list detail, add one for supplier ALPA (year_id 7)
                // set price to be price += random from -0.5 to 0.5. if price is below zero, add 1

                // set rank 0, set deleted 'N'

                var details = context.Tender_List_Details.GroupBy(x => x.item_code).ToList();

                Random r = new Random();

                int count = details.Count;
                int i = 0;
                foreach (var detail in details)
                {
                    string code = detail.Key;
                    decimal price = detail.First().price;

                    decimal priceChange = (decimal)(r.NextDouble() - 0.2);

                    decimal newPrice = price + priceChange;

                    newPrice = newPrice < 0 ? newPrice + 0.5m : newPrice;

                    Tender_List_Details newDetail = new Tender_List_Details();

                    newDetail.price = newPrice;
                    newDetail.rank = 0;
                    if (i <= count / 2)
                    {
                        newDetail.tender_year_id = 10;
                    }
                    else
                    {
                        newDetail.tender_year_id = 11;
                    }

                    newDetail.item_code = code;
                    newDetail.deleted = "N";

                    context.Tender_List_Details.Add(newDetail);

                    i++;
                }

                context.SaveChanges();
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            using (SSISEntities context = new SSISEntities())
            {
                // For all item codes that are not set, go and add things to stuff
                var details = context.Tender_List_Details.GroupBy(x => x.item_code).ToList();

                var existingItemCodes = details.Select(s => s.Key);
                var otherItemCodes = context.Stock_Inventory.Select(s => s.item_code).ToList();

                foreach (var item in existingItemCodes)
                {
                    otherItemCodes.Remove(item);
                }

                // otherItemCodes has been filtered

                Random r = new Random();

                List<int> tenderYearIds = context.Tender_List.Select(x => x.tender_year_id).ToList();

                foreach (var item in otherItemCodes)
                {
                    int toRemove = r.Next(0, 4);

                    List<int> filteredTenderYearIds = tenderYearIds.ToList();

                    // select 3 supplier ids
                    filteredTenderYearIds.RemoveAt(toRemove);

                    // add a newdetail for each of the supplier ids, with each a random price
                    double price = r.NextDouble() * 2 + 0.2d;

                    foreach (int tenderYeadId in filteredTenderYearIds)
                    {
                        // add a new tenderListDetail
                        Tender_List_Details newDetail = new Tender_List_Details();

                        decimal priceChange = (decimal)((r.NextDouble()  * 0.4) - 0.2);

                        decimal newPrice = ((decimal) price) + priceChange;

                        newDetail.price = newPrice;
                        newDetail.rank = 0;
                        newDetail.tender_year_id = tenderYeadId;
                        newDetail.item_code = item;
                        newDetail.deleted = "N";

                        context.Tender_List_Details.Add(newDetail);

                    }
                }

                context.SaveChanges();
            }
        }

        protected void btnSetRanks_Click(object sender, EventArgs e)
        {
            // Get all items
            // Group by item code
            // For each item code, sort its contents by price
            // With the sorted, rank in order 1, 2, 3
            // Save changes

            using (SSISEntities context = new SSISEntities())
            {
                // For all item codes that are not set, go and add things to stuff
                var groups = context.Tender_List_Details.GroupBy(x => x.item_code).ToList();

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