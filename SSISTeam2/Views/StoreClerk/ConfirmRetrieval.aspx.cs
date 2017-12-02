using SSISTeam2.Classes.EFFacades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class ConfirmRetrieval : System.Web.UI.Page
    {
        private string SESSION_RETRIEVING_LIST = "ConfirmRetrieval_RetrievingList";
        public const string PUBLIC_SESSION_DISCREPANCY_DICT = "DiscrepanciesList";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            panelNoItems.Visible = false;
            panelNormal.Visible = false;

            using (SSISEntities context = new SSISEntities())
            {
                var allocated = FacadeFactory.getRetrievalService(context).getAllRetrievingByClerk(User.Identity.Name);
                if (allocated.Count == 0)
                {
                    panelNoItems.Visible = true;
                    return;
                }

                panelNormal.Visible = true;

                var itemGroups = allocated.SelectMany(sm =>
                    sm.Items
                    .Select(s => new { s.Key.ItemCode, s.Key.Description, Quantity = s.Value, sm.Department.dept_code, sm.RequestId, sm.Department.name })
                ).GroupBy(k => k.ItemCode, v => v).ToList();

                List<ConfirmRetrievalViewModel> list = new List<ConfirmRetrievalViewModel>();

                foreach (var itemGroup in itemGroups)
                {

                    int itemQty = itemGroup.Select(s => s.Quantity).Aggregate((a, b) => a + b);
                    List<int> reqIds = itemGroup.Select(s => s.RequestId).ToList();

                    ConfirmRetrievalViewModel model = new ConfirmRetrievalViewModel();
                    model.ItemCode = itemGroup.Key;
                    model.ItemDescription = itemGroup.First().Description;
                    //model.DeptCode = deptGroup.Key;
                    //model.DeptName = deptGroup.First().name;
                    model.QuantityExpected = itemQty;
                    model.QuantityActual = itemQty;
                    model.RequestIds = reqIds;
                    //model.Include = true;

                    list.Add(model);
                }

                Session[SESSION_RETRIEVING_LIST] = list;

                _refreshGrid(list);
            }

        }


        // For paganation

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                gvToRetrieve.PageIndex = 0;
            }
            else
            {
                gvToRetrieve.PageIndex = e.NewPageIndex;
            }

            _refreshGrid((List<ConfirmRetrievalViewModel>)Session[SESSION_RETRIEVING_LIST]);

        }

        // for paganation

        protected void GridView_EditBooks_DataBound(object sender, EventArgs e)
        {
            GridViewRow topPagerRow = gvToRetrieve.TopPagerRow;
            GridViewRow bottomPagerRow = gvToRetrieve.BottomPagerRow;

            DropDownList topJumpToPage = (DropDownList)topPagerRow.FindControl("DropDownList_JumpToPage");
            DropDownList bottomJumpToPage = (DropDownList)bottomPagerRow.FindControl("DropDownList_JumpToPage");

            if (topJumpToPage != null)
            {
                for (int i = 0; i < gvToRetrieve.PageCount; i++)
                {
                    ListItem item = new ListItem("Page " + (i + 1));
                    topJumpToPage.Items.Add(item);
                    bottomJumpToPage.Items.Add(item);
                }
            }

            topJumpToPage.SelectedIndex = gvToRetrieve.PageIndex;
            bottomJumpToPage.SelectedIndex = gvToRetrieve.PageIndex;
        }


        protected void DropDownList_JumpToPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow topPagerRow = gvToRetrieve.TopPagerRow;
            GridViewRow bottomPagerRow = gvToRetrieve.BottomPagerRow;

            DropDownList topJumpToPage = (DropDownList)topPagerRow.FindControl("DropDownList_JumpToPage");
            DropDownList bottomJumpToPage = (DropDownList)bottomPagerRow.FindControl("DropDownList_JumpToPage");

            if ((DropDownList)sender == bottomJumpToPage)
            {
                gvToRetrieve.PageIndex = bottomJumpToPage.SelectedIndex;
            }
            else
            {
                gvToRetrieve.PageIndex = topJumpToPage.SelectedIndex;

            }

            _refreshGrid((List<ConfirmRetrievalViewModel>)Session[SESSION_RETRIEVING_LIST]);


        }

        private void _refreshGrid(List<ConfirmRetrievalViewModel> list)
        {
            gvToRetrieve.DataSource = list;
            gvToRetrieve.DataBind();
            //MergeCells(gvToRetrieve);
        }

        private void MergeCells(GridView gv)
        {
            int totalQty = 0;
            int i = gv.Rows.Count - 2;
            while (i >= 0)
            {
                GridViewRow curRow = gv.Rows[i];
                GridViewRow preRow = gv.Rows[i + 1];

                string curRowDescription = (curRow.FindControl("lblDescription") as Label).Text;
                string preRowDescription = (preRow.FindControl("lblDescription") as Label).Text;
                Label curRowTotalQtyLabel = curRow.FindControl("lblTotalQty") as Label;
                Label preRowTotalQtyLabel = preRow.FindControl("lblTotalQty") as Label;

                string curRowTotalQty = curRowTotalQtyLabel.Text;
                string preRowTotalQty = preRowTotalQtyLabel.Text;

                if (curRowDescription == preRowDescription)
                {
                    if (preRow.Cells[1].RowSpan < 2)
                    {
                        curRow.Cells[1].RowSpan = 2;
                        preRow.Cells[1].Visible = false;
                        totalQty = Convert.ToInt32(preRowTotalQty) + Convert.ToInt32(curRowTotalQty);
                        curRowTotalQtyLabel.Text = totalQty.ToString();
                        curRow.Cells[2].RowSpan = 2;
                        preRow.Cells[2].Visible = false;
                    }
                    else
                    {
                        curRow.Cells[1].RowSpan = preRow.Cells[1].RowSpan + 1;
                        preRow.Cells[1].Visible = false;
                        totalQty = totalQty + Convert.ToInt32(curRowTotalQty);
                        curRowTotalQtyLabel.Text = totalQty.ToString();
                        curRow.Cells[2].RowSpan = preRow.Cells[2].RowSpan + 1;
                        preRow.Cells[2].Visible = false;
                    }
                }
                i--;
            }


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Get all the models
            List<ConfirmRetrievalViewModel> list = Session[SESSION_RETRIEVING_LIST] as List<ConfirmRetrievalViewModel>;

            List<ConfirmRetrievalViewModel> okayItems = new List<ConfirmRetrievalViewModel>();
            Dictionary<ConfirmRetrievalViewModel, int> notOkayItems = new Dictionary<ConfirmRetrievalViewModel, int>();


            // For each gvr, check if actual == expected
            foreach (ConfirmRetrievalViewModel model in list)//gvToRetrieve.Rows)
            {
                //string expected = (gvr.FindControl("lblQtyExpected") as Label).Text;
                //string actual = (gvr.FindControl("tbQtyActual") as Label).Text;

                //int expectedQty = int.Parse(expected);
                //int actualQty = int.Parse(actual);
                int expectedQty = model.QuantityExpected;
                int actualQty = model.QuantityActual;

                if (actualQty > expectedQty)
                {
                    lblWarningInfo.Text = "Items cannot have a higher actual quantity than what was to be retrieved.";
                        //string.Format("Row {0} cannot have a higher actual quantity than expected.", gvr.DataItemIndex + 1);
                }

                if (expectedQty == actualQty)
                {
                    okayItems.Add(model);
                } else
                {
                    int difference = expectedQty - actualQty;
                    notOkayItems.Add(model, difference);
                }
            }

            using (SSISEntities context = new SSISEntities())
            {

                // Save the Okay Items
                // Get a list of all request ids and item code (de-normalized)
                okayItems.SelectMany(sm => sm.RequestIds
                            .Select(s => new { RequestId = s, sm.ItemCode })
                        )
                        // Normalise and group by requestId
                        .GroupBy(k => k.RequestId, v => v.ItemCode)
                        .ToList()
                        // For each, get the Request object, match the items in the request, and save to DB
                        .ForEach(idAndItemCode =>
                        {
                            Request request = context.Requests.Find(idAndItemCode.Key);

                            Dictionary<string, int> itemCodeAndQty =
                                request.Request_Details
                                .Where(w => w.deleted != "Y" 
                                            && idAndItemCode.Contains(w.item_code)
                                        )
                                .ToDictionary(
                                    k => k.item_code,
                                    v => v.Request_Event.First().allocated.Value
                                );

                            FacadeFactory.getRequestMovementService(context).moveFromRetrievingToRetrieved(idAndItemCode.Key, itemCodeAndQty, User.Identity.Name);
                        });

                Dictionary<
                    string,
                    Dictionary<int, int>
                    >
                    itemCodeAndIdAndQty = new Dictionary<string, Dictionary<int, int>>();


                // Go through not okay items and shift quantities around
                foreach (var item in notOkayItems)
                {
                    // Get list of all requestIds
                    List<Request> requestsByDateDesc = item.Key.RequestIds.Select(s => context.Requests.Find(s)).Where(w => w.deleted != "Y").OrderByDescending(o => o.date_time).ToList();
                    int shortfall = item.Value;

                    Dictionary<int, int> idAndQty = new Dictionary<int, int>();

                    // First is the latest made request
                    foreach (var request in requestsByDateDesc)
                    {
                        Request_Details detail = request.Request_Details.Where(w => w.deleted != "Y" && w.item_code == item.Key.ItemCode).DefaultIfEmpty(null).FirstOrDefault();
                        if (detail == null) continue;

                        int origQty = detail.Request_Event.First().allocated.Value;
                        int retrievedQty = origQty;

                        if (shortfall > 0)
                        { 
                            if (shortfall > origQty)
                            {
                                shortfall -= origQty;
                                retrievedQty = 0;
                            } else if (shortfall == origQty)
                            {
                                shortfall = 0;
                                retrievedQty = 0;
                            } else
                            {
                                // shortfall < origQty
                                retrievedQty = origQty - shortfall;
                                shortfall = 0;
                            }
                        }

                        idAndQty.Add(request.request_id, retrievedQty);
                    }

                    itemCodeAndIdAndQty.Add(item.Key.ItemCode, idAndQty);

                    // Minus the expected quantity for each request's item origQty, while expected quantity is > 0,
                    // and don't minus if origQty is > expectedQty
                    // Once you've hit zero or cannot minus, assign the leftover into the next request
                    // Then the rest of the requests are gonna be zero
                    // Then save all these requests using the MovementService

                }

                var notOkayGroupingsByRequestId = itemCodeAndIdAndQty
                    .SelectMany(sm =>
                        sm.Value.Select(s => new { requestId = s.Key, itemCode = sm.Key, retrievedQty = s.Value })
                    )
                    .GroupBy(k => k.requestId, v => new { v.itemCode, v.retrievedQty });

                foreach (var grouping in notOkayGroupingsByRequestId)
                {
                    var items = grouping.ToDictionary(k => k.itemCode, v => v.retrievedQty);
                    FacadeFactory.getRequestMovementService(context).moveFromRetrievingToRetrieved(grouping.Key, items, User.Identity.Name);
                }

                context.SaveChanges();

            } // Disposal of context

            // If there were not okay items, record into file discrepancy session variable
            if (notOkayItems.Count > 0)
            {
                DiscrepancyDictionary discrepancyDict = Session[PUBLIC_SESSION_DISCREPANCY_DICT] as DiscrepancyDictionary;

                if (discrepancyDict == null) discrepancyDict = new DiscrepancyDictionary();

                foreach (var item in notOkayItems)
                {
                    discrepancyDict.AddOrIncrease(item.Key.ItemCode, item.Value);
                }

                Session[PUBLIC_SESSION_DISCREPANCY_DICT] = discrepancyDict;

                // Enable button to continue to fileDiscrepancies

                // Change to redirect to heng tiong's thing
                Response.Redirect("FileDiscrepency.aspx", false);

            } else
            {
                Response.Redirect(Request.Url.ToString(), false);
            }

        }

        protected void tbQtyActual_TextChanged(object sender, EventArgs e)
        {
            List<ConfirmRetrievalViewModel> list = Session[SESSION_RETRIEVING_LIST] as List<ConfirmRetrievalViewModel>;

            TextBox tb = sender as TextBox;
            GridViewRow gvr = tb.Parent.Parent as GridViewRow;

            int index = gvr.DataItemIndex;

            string valStr = tb.Text;
            int value = 0;

            if (int.TryParse(valStr, out value))
            {
                // Could convert
                int expectedQty = list[index].QuantityExpected;
                int updatedValue = value;

                if (value > expectedQty)
                {
                    updatedValue = expectedQty;
                } else if (value < 0)
                {
                    updatedValue = 0;
                }

                list[index].QuantityActual = updatedValue;
            }

            Session[SESSION_RETRIEVING_LIST] = list;

            _checkRowsForDiscrepancies(list);

            _refreshGrid(list);
        }

        protected void btnResetRowQty_Click(object sender, EventArgs e)
        {
            List<ConfirmRetrievalViewModel> list = Session[SESSION_RETRIEVING_LIST] as List<ConfirmRetrievalViewModel>;

            Button btn = sender as Button;
            GridViewRow gvr = btn.Parent.Parent as GridViewRow;

            int index = gvr.DataItemIndex;

            list[index].QuantityActual = list[index].QuantityExpected;

            Session[SESSION_RETRIEVING_LIST] = list;

            _checkRowsForDiscrepancies(list);

            _refreshGrid(list);
        }

        private void _checkRowsForDiscrepancies(List<ConfirmRetrievalViewModel> list)
        {
            bool noDiscrepancies = list.All(a => a.QuantityActual == a.QuantityExpected);

            if (noDiscrepancies)
            {
                btnSubmit.Text = "Confirm quantities";
            }
            else
            {
                btnSubmit.Text = "Confirm quantities, then file discrepancies";
            }
        }
    }

    public class DiscrepancyDictionary : Dictionary<string, int>
    {
        public void AddOrIncrease(string key, int value)
        {
            if (this.ContainsKey(key))
            {
                // Increase
                int orig = this[key];
                int newVal = orig + value;
                this[key] = newVal;
            } else
            {
                // Just add
                this.Add(key, value);
            }
        }
    }

    class ConfirmRetrievalViewModel
    {
        string itemCode, itemDescription;
        int quantityExpected, quantityActual;
        List<int> requestIds;

        public List<int> RequestIds
        {
            get
            {
                return requestIds;
            }

            set
            {
                requestIds = value;
            }
        }

        public string ItemCode
        {
            get
            {
                return itemCode;
            }

            set
            {
                itemCode = value;
            }
        }

        public string ItemDescription
        {
            get
            {
                return itemCode + " - " + itemDescription;
            }

            set
            {
                itemDescription = value;
            }
        }

        public int QuantityExpected
        {
            get
            {
                return quantityExpected;
            }

            set
            {
                quantityExpected = value;
            }
        }

        public int QuantityActual
        {
            get
            {
                return quantityActual;
            }

            set
            {
                quantityActual = value;
            }
        }

    }

}