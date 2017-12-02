using SSISTeam2.Classes.EFFacades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class GenerateRetrieval : System.Web.UI.Page
    {
        private const string SESSION_ALLOC_LIST = "MakeRetrievalForm_AllocList";

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    Response.Redirect("~/login.aspx?return=/Views/StoreClerk/DEMO_MakeRetrievalForm.aspx");
            //}

            if (IsPostBack)
            {
                return;
            }

            panelNoItems.Visible = false;
            panelNormal.Visible = false;
            using (SSISEntities context = new SSISEntities())
            {
                //lblDebug.Text += User.Identity.Name;

                var allocated = FacadeFactory.getAllocatedService(context).getAllAllocated();
                if (allocated.Count == 0)
                {
                    panelNoItems.Visible = true;
                    return;
                }


                panelNormal.Visible = true;

                //var items = allocated.SelectMany(sm =>
                //    sm.Items.Select(s => new { s.Key.ItemCode, s.Key.Description, s.Value, sm.Department.dept_code, sm.RequestId, sm.Department.name })
                //).OrderBy(o => o.name)
                //.OrderBy(o => o.ItemCode)
                //.ToList();//.GroupBy(k => k.ItemCode, v => new { v.dept_code, v.RequestId });

                var itemGroups = allocated.SelectMany(sm =>
                    sm.Items
                    //.Where(w => w.Value > 0)
                    .Select(s => new { s.Key.ItemCode, s.Key.Description, s.Value, sm.Department.dept_code, sm.RequestId, sm.Department.name })
                ).GroupBy(k => k.ItemCode, v => v).ToList();

                List<RetrievalFormViewModel> list = new List<RetrievalFormViewModel>();

                foreach (var itemGroup in itemGroups)
                {
                    var deptGroups = itemGroup.GroupBy(k => k.dept_code, v => v);
                    foreach (var deptGroup in deptGroups)
                    {
                        int deptQty = deptGroup.Select(s => s.Value).Aggregate((a, b) => a + b);
                        List<int> reqIds = deptGroup.Select(s => s.RequestId).ToList();

                        RetrievalFormViewModel model = new RetrievalFormViewModel();
                        model.ItemCode = itemGroup.Key;
                        model.ItemDescription = itemGroup.First().Description;
                        model.DeptCode = deptGroup.Key;
                        model.DeptName = deptGroup.First().name;
                        model.Quantity = deptQty;
                        model.RequestIds = reqIds;
                        model.Include = true;

                        list.Add(model);
                    }
                }

                Session[SESSION_ALLOC_LIST] = list;

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

            _refreshGrid((List<RetrievalFormViewModel>)Session[SESSION_ALLOC_LIST]);

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

            _refreshGrid((List<RetrievalFormViewModel>)Session[SESSION_ALLOC_LIST]);


        }




        protected void chkbxInclude_CheckedChanged(object sender, EventArgs e)
        {
            List<RetrievalFormViewModel> list = Session[SESSION_ALLOC_LIST] as List<RetrievalFormViewModel>;
            CheckBox chkBox = sender as CheckBox;
            GridViewRow gvr = chkBox.Parent.Parent as GridViewRow;

            int selectedIndex = gvr.DataItemIndex;

            // Flip the include boolean
            list[selectedIndex].Include = !list[selectedIndex].Include;

            Session[SESSION_ALLOC_LIST] = list;

            _refreshGrid(list);
        }

        private void _refreshGrid(List<RetrievalFormViewModel> list)
        {
            gvToRetrieve.DataSource = list;
            gvToRetrieve.DataBind();
            MergeCells(gvToRetrieve);
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
            List<RetrievalFormViewModel> list = Session[SESSION_ALLOC_LIST] as List<RetrievalFormViewModel>;
            // Convert to ids and items to retrieve
            var listByRequestIds = list
                .SelectMany(sm => sm.RequestIds
                .Select(s => new { RequestId = s, sm.ItemCode, sm.Include }))
                .Where(w => w.Include)
                .GroupBy(k => k.RequestId, v => v.ItemCode);

            using (SSISEntities context = new SSISEntities())
            {
                foreach (var request in listByRequestIds)
                {
                    FacadeFactory.getRequestMovementService(context).moveFromAllocatedToRetrieving(request.Key, request.ToList(), User.Identity.Name);
                }

                context.SaveChanges();
            }

            Response.Redirect(Request.Url.ToString(), false);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    class RetrievalFormViewModel
    {
        string itemCode, itemDescription, deptCode, deptName;
        int quantity;
        List<int> requestIds;
        bool include;

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

        public string DeptCode
        {
            get
            {
                return deptCode;
            }

            set
            {
                deptCode = value;
            }
        }

        public string DeptName
        {
            get
            {
                return deptName;
            }

            set
            {
                deptName = value;
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }

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

        public bool Include
        {
            get
            {
                return include;
            }

            set
            {
                include = value;
            }
        }

        public int IncludedQty
        {
            get
            {
                return include ? quantity : 0;
            }
        }

    


    }



}