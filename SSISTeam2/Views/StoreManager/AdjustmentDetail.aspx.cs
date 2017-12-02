using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SSISTeam2.Classes.Models;

namespace SSISTeam2
{
    public partial class ApproveStockAdjustment : System.Web.UI.Page
    {
        SSISEntities context;
        protected void Page_Load(object sender, EventArgs e)
        {
            context = new SSISEntities();
            InventoryAdjustmentModel invModel = (InventoryAdjustmentModel)Session["ConfirmAdj"];
            //List<InventoryAdjustmentModel> invModelList = new List<InventoryAdjustmentModel>();
            //invModelList.Add(invModel);
            //GridView2.DataSource = invModelList;
            //GridView2.DataBind();

            List<AdjustmentModel> adjList = invModel.AdjModel;
          

            GridView1.DataSource = adjList;
            GridView1.DataBind();

        }

        protected void Approve_Click(object sender, EventArgs e)
        {
            InventoryAdjustmentModel invModel = (InventoryAdjustmentModel)Session["ConfirmAdj"];
            Inventory_Adjustment inventoryAdjustment = context.Inventory_Adjustment.Where(x => x.voucher_id == invModel.VoucherID).ToList().First();
            inventoryAdjustment.status = "Approved";
            inventoryAdjustment.status_date = DateTime.Today;

            context.SaveChanges();
            Response.Redirect("~/Views/DepartmentHead/HeadDashboard.aspx");
        }

        protected void Reject_Click(object sender, EventArgs e)
        {
            InventoryAdjustmentModel invModel = (InventoryAdjustmentModel)Session["ConfirmAdj"];
            Inventory_Adjustment inventoryAdjustment = context.Inventory_Adjustment.Where(x => x.voucher_id == invModel.VoucherID).ToList().First();
            inventoryAdjustment.status = "Rejected";
            inventoryAdjustment.status_date = DateTime.Today;

            context.SaveChanges();
            Response.Redirect("~/Views/DepartmentHead/HeadDashboard.aspx");
        }
    }
}