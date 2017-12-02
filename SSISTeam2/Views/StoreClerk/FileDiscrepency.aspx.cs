using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SSISTeam2.Classes.Models;

namespace SSISTeam2
{
    public partial class FileDiscrepency : System.Web.UI.Page
    {
        public const string PUBLIC_SESSION_DISCREPANCY_DICT = Views.StoreClerk.ConfirmRetrieval.PUBLIC_SESSION_DISCREPANCY_DICT;

        SSISEntities context;
        protected void Page_Load(object sender, EventArgs e)
        {
            context = new SSISEntities();

            List<MonthlyCheckModel> modelList;
            Dictionary<string, int> SessionInfo;

            if (!IsPostBack)
            {
                modelList = new List<MonthlyCheckModel>();
                SessionInfo = (Dictionary<string, int>)Session[PUBLIC_SESSION_DISCREPANCY_DICT];

                // Early quit if no session
                if (SessionInfo == null)
                {
                    lblWarning.Text = "No discrepancies to be filed.";
                    ConfirmBtn.Visible = false;
                    return;
                }

                lblWarning.Visible = false;

                foreach (KeyValuePair<string, int> pair in SessionInfo)
                {
                    string itemCode = pair.Key;
                    int qtyAdjusted = pair.Value;

                    Stock_Inventory inventory = context.Stock_Inventory.Where(x => x.item_code == itemCode).ToList().First();

                    MonthlyCheckModel model = new MonthlyCheckModel(inventory);
                    model.ActualQuantity = model.CurrentQuantity - qtyAdjusted;
                    modelList.Add(model);
                }

                Session["DisDetail"] = modelList;

                FileDiscrepencyGV.DataSource = modelList;
                FileDiscrepencyGV.DataBind();
            }
        }

        protected void reasonTB_TextChanged(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = (GridViewRow)(sender as Control).Parent.Parent;
            TextBox reasonTB = (TextBox)sender;
            Label rowIndex = (Label)gridViewRow.FindControl("rowIndex");
            int index = int.Parse(rowIndex.Text) - 1;
            List<MonthlyCheckModel> itemList = (List<MonthlyCheckModel>)Session["DisDetail"];
            itemList[index].Reason = reasonTB.Text;

            Session["DisDetail"] = itemList;
            FileDiscrepencyGV.DataSource = itemList;
            FileDiscrepencyGV.DataBind();
        }

        protected void ConfirmBtn_Click(object sender, EventArgs e)
        {
            List<MonthlyCheckModel> itemList = (List<MonthlyCheckModel>)Session["DisDetail"];
            if (itemList == null)
            {
                //Monthly_Check_Records checkRecord = new Monthly_Check_Records();
                //checkRecord.date_checked = DateTime.Today;
                //checkRecord.clerk_user = HttpContext.Current.User.Identity.Name;
                //checkRecord.deleted = "N";
                //checkRecord.discrepancy = "N";

                Response.Redirect("~/Default.aspx");
                Session["DisDetail"] = null;
                Session[PUBLIC_SESSION_DISCREPANCY_DICT] = null;
            }
            else
            {
                Inventory_Adjustment invAdjustmentSup = new Inventory_Adjustment();
                invAdjustmentSup.date = DateTime.Today;
                invAdjustmentSup.clerk_user = HttpContext.Current.User.Identity.Name;
                invAdjustmentSup.status = "Pending";
                invAdjustmentSup.status_date = DateTime.Today;
                invAdjustmentSup.deleted = "N";

                Inventory_Adjustment invAdjustmentMan = new Inventory_Adjustment();
                invAdjustmentMan.date = DateTime.Today;
                invAdjustmentMan.clerk_user = HttpContext.Current.User.Identity.Name;
                invAdjustmentMan.status = "Pending";
                invAdjustmentMan.status_date = DateTime.Today;
                invAdjustmentMan.deleted = "N";


                foreach (MonthlyCheckModel i in itemList)
                {
                    //get price of adjustment for MonthlyCheckModel
                    double priceAdj = i.AveragePrice * Math.Abs(i.ActualQuantity - i.CurrentQuantity);

                    Stock_Inventory inventory = context.Stock_Inventory.Where(x => x.item_code == i.ItemCode).ToList().First();
                    inventory.current_qty = i.ActualQuantity;

                    Adjustment_Details adjDetails = new Adjustment_Details();
                    adjDetails.deleted = "N";
                    adjDetails.item_code = i.ItemCode;
                    adjDetails.quantity_adjusted = i.ActualQuantity - i.CurrentQuantity;
                    adjDetails.reason = i.Reason;

                    if (priceAdj < 250)
                    {
                        invAdjustmentSup.Adjustment_Details.Add(adjDetails);
                    }
                    else if (priceAdj >= 250)
                    {
                        invAdjustmentMan.Adjustment_Details.Add(adjDetails);
                    }

                    context.Adjustment_Details.Add(adjDetails);
                }

                if (invAdjustmentSup.Adjustment_Details.Count != 0)
                {
                    context.Inventory_Adjustment.Add(invAdjustmentSup);
                    context.SaveChanges();

                    MonthlyCheckConfirmation._sendEmail(User.Identity.Name, false);
                }
                if (invAdjustmentMan.Adjustment_Details.Count != 0)
                {
                    context.Inventory_Adjustment.Add(invAdjustmentMan);
                    context.SaveChanges();

                    MonthlyCheckConfirmation._sendEmail(User.Identity.Name, true);
                }
            }

            // Clear the session once done
            Session["DisDetail"] = null;
            Session[PUBLIC_SESSION_DISCREPANCY_DICT] = null;

            Response.Redirect("~/Default.aspx");
        }
    }
}