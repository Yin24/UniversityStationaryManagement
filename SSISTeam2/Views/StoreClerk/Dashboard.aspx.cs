using SSISTeam2.Classes.EFFacades;
using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            string currentUser = User.Identity.Name;

            SSISEntities context = new SSISEntities();

            string fullName = "";

            fullName = context.Dept_Registry.Find(currentUser).fullname;

            lblFullName.Text = "Welcome, " + fullName;

            /* Emp side Dash */
            FillPage();

            /* Items to retrieve */
            #region Items to retrieve
            /* Items for retrieving */
            var allocated = FacadeFactory.getAllocatedService(context).getAllAllocated();

            bool anyAllocated = false;

            if (allocated != null)
            {
                anyAllocated = allocated.Count > 0;

                var allocatedItems = allocated.SelectMany(sm =>
                        sm.Items
                        .Select(s => new { s.Key.ItemCode, s.Key.Description, Quantity = s.Value
                        //, sm.Department.dept_code, sm.RequestId, sm.Department.name
                        })
                    )
                    .GroupBy(k => k.ItemCode, v => v)
                    .Select(s => s.Aggregate((a, b) => {
                        int Quantity = a.Quantity + b.Quantity;
                        return new { a.ItemCode, a.Description, Quantity };
                    })
                    )
                    .ToList();
                gridViewToRetrieve_FromWarehouse.DataSource = allocatedItems.Take(3);
                gridViewToRetrieve_FromWarehouse.DataBind();

                lblNumToRetrieve.Text = string.Format("({0} in total)", allocatedItems.Count);
            }


            /* Items to confirm */
            var toConfirm = FacadeFactory.getRetrievalService(context).getAllRetrievingByClerk(currentUser);

            bool anyRetrievingToConfirm = toConfirm.Count > 0;

            var toConfirmItems = toConfirm.SelectMany(sm =>
                    sm.Items
                    .Select(s => new { s.Key.ItemCode, s.Key.Description, Quantity = s.Value })
                )
                .GroupBy(k => k.ItemCode, v => v)
                .Select(s => s.Aggregate((a, b) => {
                    int Quantity = a.Quantity + b.Quantity;
                    return new { a.ItemCode, a.Description, Quantity };
                })
                )
                .ToList();

            gridViewToRetrieve_ToConfirm.DataSource = toConfirmItems.Take(3);
            gridViewToRetrieve_ToConfirm.DataBind();

            /* Display toggling */
            panelToRetrieve_Empty.Visible = true;
            panelToRetrieve_FromWarehouse.Visible = true;
            panelToRetrieve_ToConfirm.Visible = true;

            if (anyAllocated)
            {
                panelToRetrieve.CssClass = "panel panel-info";
                panelToRetrieve_Empty.Visible = false;
                
            } else
            {
                panelToRetrieve_FromWarehouse.Visible = false;
            }

            if (anyRetrievingToConfirm)
            {
                panelToRetrieve.CssClass = "panel panel-info";
                panelToRetrieve_Empty.Visible = false;
                lblNumToConfirm.Text = string.Format("({0} in total)", toConfirmItems.Count);
            } else
            {
                panelToRetrieve_ToConfirm.Visible = false;
            }
            #endregion

            /* Disbursements */
            #region Disbursements
            /* To disburse to collection points */
            var toBeDisbursed = FacadeFactory.getDisbursementService(context).getAllPossibleDisbursements();

            bool anyToBeDisbursed = toBeDisbursed.Count > 0;

            var collectionPtIds = toBeDisbursed.SelectMany(sm =>
                sm.Items
                .Select(s => new { s.Key.ItemCode, s.Key.Description, s.Value, sm.Department.dept_code, sm.RequestId, sm.Department.name, sm.CollectionPtId })
            )
            .GroupBy(k => k.CollectionPtId)
            .Select(s => s.Key)
            .ToList();

            List<Collection_Point> collectionPts = context.Collection_Point.Where(w => w.deleted != "Y" && collectionPtIds.Contains(w.collection_pt_id)).ToList();

            gridViewToDisburse_ToCollectionPt.DataSource = collectionPts.Take(3);
            gridViewToDisburse_ToCollectionPt.DataBind();

            /* To sign-off */
            DisbursementModelCollection disbursingList = FacadeFactory.getDisbursementService(context).getAllThatCanBeSignedOff(currentUser);

            bool anyToBeSignedOff = disbursingList.Count > 0;

            var toBeSignedOff = disbursingList
                .Select(sm => sm.Department.name )
                .Distinct()
                .Select(s => new { DepartmentName = s })
                .ToList();

            gridViewToDisburse_ToConfirm.DataSource = toBeSignedOff.Take(3);
            gridViewToDisburse_ToConfirm.DataBind();

            /* Display toggling */
            panelToDisburse_Empty.Visible = true;
            panelToDisburse_ToCollectionPt.Visible = true;
            panelToDisburse_ToConfirm.Visible = true;

            if (anyToBeDisbursed)
            {
                panelToDisburse.CssClass = "panel panel-success";
                panelToDisburse_Empty.Visible = false;
                lblNumToDisburse.Text = string.Format("({0} in total)", collectionPts.Count);
            }
            else
            {
                panelToDisburse_ToCollectionPt.Visible = false;
            }

            if (anyToBeSignedOff)
            {
                panelToDisburse.CssClass = "panel panel-success";
                panelToDisburse_Empty.Visible = false;
                lblNumToSignOff.Text = string.Format("({0} in total)", toBeSignedOff.Count);
            }
            else
            {
                panelToDisburse_ToConfirm.Visible = false;
            }
            #endregion

            /* Low Stocks */
            #region Low Stocks
            List<Stock_Inventory> stocks = context.Stock_Inventory.ToList();
            List<ItemModel> lowStocks = new List<ItemModel>();

            foreach (var stock in stocks)
            {
                ItemModel im = new ItemModel(stock);
                if (im.AvailableQuantity < im.ReorderLevel)
                {
                    lowStocks.Add(im);
                }
            }

            gridViewLowStocks.DataSource = lowStocks.OrderBy(o => o.AvailableQuantity).Take(5);
            gridViewLowStocks.DataBind();

            if (lowStocks.Count > 0)
            {
                panelLowStocksEmpty.Visible = false;
                panelLowStocksNormal.Visible = true;
                panelLowStocksBtn.Visible = true;
                panelLowStocks.CssClass = "panel panel-warning";
                gridViewLowStocks.HeaderRow.CssClass = "warning";
                lblNumLowStock.Text = string.Format("({0} in total)", lowStocks.Count);
            } else
            {
                panelLowStocksEmpty.Visible = true;
                panelLowStocksNormal.Visible = false;
                panelLowStocksBtn.Visible = false;
            }
            #endregion

            context.Dispose();
        }

        private void FillPage()
        {
            // need to login
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/login.aspx?return=Views/Employee/EmpDashboard.aspx");
            }
          

            string currentUser = Page.User.Identity.Name;
            string fullName = "";
            using (SSISEntities context = new SSISEntities())
            {
                fullName = context.Dept_Registry.Find(currentUser).fullname;
                lblFullName.Text = "Welcome, " + fullName;

                string username = User.Identity.Name.ToString();
                UserModel user = new UserModel(username);
                string currentDept = user.Department.dept_code;
                var requestInfos = (from x in context.Requests
                         where username == x.username
                         select new { x.request_id, x.date_time, x.reason, x.current_status }).OrderByDescending(o => o.date_time).Take(3).ToList();
                GridView1.DataSource = requestInfos;
                GridView1.DataBind();

                var q2 = (from x in context.Requests
                          where currentDept == x.dept_code
                          select new { x.request_id, x.username, x.Dept_Registry.fullname, x.date_time, x.reason, x.current_status }).OrderByDescending(o => o.date_time).Take(3).ToList();
                GridView2.DataSource = q2;
                GridView2.DataBind();
            }
        }

        protected void btnMakeNewReq_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/MakeNewRequest.aspx");
        }

        protected void btnShowHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/EmpRequestHistory.aspx");
        }

        protected void btnShowAllToRetrieve_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/GenerateRetrieval.aspx");
        }

        protected void btnConfirmRetrieval_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/ConfirmRetrieval.aspx");
        }

        protected void btnGenerateDisbursement_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/GenerateDisbursement.aspx");
        }

        protected void btnConfirmDisbursement_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/ConfirmDisbursement.aspx");
        }

        protected void btnLowStock_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/lowstock.aspx");
        }
    }
}