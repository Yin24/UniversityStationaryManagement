using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SSISTeam2.Classes.Models;
using System.Text;
using SSISTeam2.Classes;

namespace SSISTeam2
{
    public partial class MonthlyCheckConfirmation : System.Web.UI.Page
    {

        SSISEntities context;

        protected void Page_Load(object sender, EventArgs e)
        {
            context = new SSISEntities();
            if (!IsPostBack)
            {
                List<MonthlyCheckModel> itemList = (List<MonthlyCheckModel>)Session["Confirmation"];
                try
                {
                    if (itemList == null)
                    {
                        confirmationGV.Visible = false;
                        CheckLabel.Text = "There are not discrepancies for this month, confirm?";
                    }
                    confirmationGV.DataSource = itemList;
                    confirmationGV.DataBind();

                }
                catch (Exception exec)
                {
                }
            }
            //else
            //{
            //    List<MonthlyCheckModel> itemList = (List<MonthlyCheckModel>)Session["Confirmation"];
            //    confirmationGV.DataSource = itemList;
            //    confirmationGV.DataBind();
            //    Label1.Text = itemList.Count.ToString();
            //}

        }

        protected void confirmBtn_Click(object sender, EventArgs e)
        {
            //Inventory_Adjustment inventoryAdj = new Inventory_Adjustment();
            //foreach (Adjustment_Details i in adjDetails)
            //{
            //    inventoryAdj.Adjustment_Details.Add(i);
            //}
            //Label1.Text = inventoryAdj.Adjustment_Details.Count.ToString();
            List<MonthlyCheckModel> itemList = (List<MonthlyCheckModel>)Session["Confirmation"];
            Monthly_Check_Records checkRecord = new Monthly_Check_Records();

            if (itemList == null)
            {
                checkRecord.date_checked = DateTime.Today;
                checkRecord.clerk_user = HttpContext.Current.User.Identity.Name;
                checkRecord.deleted = "N";
                checkRecord.discrepancy = "N";

                context.Monthly_Check_Records.Add(checkRecord);
                context.SaveChanges();

                Response.Redirect("~/Views/StoreClerk/Dashboard.aspx");
            }
            else
            {
                //UpdateMonthlyCheck(itemList, HttpContext.Current.User.Identity.Name);
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



                checkRecord.date_checked = DateTime.Today;
                checkRecord.clerk_user = HttpContext.Current.User.Identity.Name;
                checkRecord.deleted = "N";
                checkRecord.discrepancy = "Y";

                if (invAdjustmentSup.Adjustment_Details.Count != 0)
                {
                    context.Inventory_Adjustment.Add(invAdjustmentSup);
                    context.SaveChanges();

                    _sendEmail(User.Identity.Name, false);
                }
                if (invAdjustmentMan.Adjustment_Details.Count != 0)
                {
                    context.Inventory_Adjustment.Add(invAdjustmentMan);
                    context.SaveChanges();

                    _sendEmail(User.Identity.Name, true);
                }

                context.Monthly_Check_Records.Add(checkRecord);
                context.SaveChanges();
                Session["Confirmation"] = null;
                Session["Monthly"] = null;
                Response.Redirect("~/Views/StoreClerk/Dashboard.aspx");
            }
        }

        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/MonthlyCheck.aspx");
        }

        protected void reasonTB_TextChanged(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = (GridViewRow)(sender as Control).Parent.Parent;
            TextBox reasonTB = (TextBox)sender;
            Label rowIndex = (Label)gridViewRow.FindControl("rowIndex");
            int index = int.Parse(rowIndex.Text) - 1;
            List<MonthlyCheckModel> itemList = (List<MonthlyCheckModel>)Session["Confirmation"];
            itemList[index].Reason = reasonTB.Text;

            Session["Confirmation"] = itemList;
            confirmationGV.DataSource = itemList;
            confirmationGV.DataBind();
        }

        public static void _sendEmail(string username, bool allTheWayToManager)
        {

            /* Email logic */
            UserModel currentUserModel = new UserModel(username);

            var depth = currentUserModel.FindDelegateOrDeptHead();
            var sup = currentUserModel.FindStoreSupervisor();

            string superiorUserName = allTheWayToManager ? currentUserModel.FindDelegateOrDeptHead().Username : currentUserModel.FindStoreSupervisor().Username;

            UserModel superior = new UserModel(superiorUserName);

            string fromEmail = currentUserModel.Email;
            string fromName = currentUserModel.Fullname;

            string toEmail = superior.Email;
            string toName = superior.Fullname;

            string subject = string.Format("Inventory adjustment to review");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Dear " + toName + ",");
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");
            sb.AppendLine(string.Format("{0} has filed an inventory adjustment. Please review and approve it.", fromName));
            sb.AppendLine("<br />");
            sb.AppendLine(string.Format("Please <a href=\"{0}\">follow this link to view pending adjustments</a>.", "https://rebrand.ly/ssis-store-viewadjust"));
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");
            sb.AppendLine("Thank you.");
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");
            sb.AppendLine("<i>This message was auto-generated by the Stationery Store Inventory System.</i>");

            string body = sb.ToString();

            new Emailer(fromEmail, fromName).SendEmail(toEmail, toName, subject, body);
            /* End of email logic */
        }

        public void UpdateMonthlyCheck(List<MonthlyCheckModel> list, string username)
        {
            Inventory_Adjustment inventoryAdjMan = new Inventory_Adjustment();
            inventoryAdjMan.clerk_user = username;
            inventoryAdjMan.deleted = "N";
            inventoryAdjMan.status = "Pending";
            inventoryAdjMan.date = DateTime.Today;
            inventoryAdjMan.status_date = DateTime.Today;

            Inventory_Adjustment inventoryAdjSup = new Inventory_Adjustment();
            inventoryAdjMan.clerk_user = username;
            inventoryAdjMan.deleted = "N";
            inventoryAdjMan.status = "Pending";
            inventoryAdjMan.date = DateTime.Today;
            inventoryAdjMan.status_date = DateTime.Today;

            foreach (MonthlyCheckModel i in list)
            {
                int adjusted = i.CurrentQuantity - i.ActualQuantity;

                Stock_Inventory inventory = context.Stock_Inventory.Where(x => x.item_code == i.ItemCode).ToList().First();
                MonthlyCheckModel itemModel = new MonthlyCheckModel(inventory);
                double cost = Math.Abs(adjusted) * itemModel.AveragePrice;

                Adjustment_Details adjustmentDetail = new Adjustment_Details();
                adjustmentDetail.item_code = i.ItemCode;
                adjustmentDetail.quantity_adjusted = adjusted;
                adjustmentDetail.reason = i.Reason;
                adjustmentDetail.deleted = "N";


                if (cost >= 250)
                {
                    inventoryAdjMan.Adjustment_Details.Add(adjustmentDetail);
                }
                else if (cost < 250)
                {
                    inventoryAdjSup.Adjustment_Details.Add(adjustmentDetail);
                }
                context.Adjustment_Details.Add(adjustmentDetail);
            }

            if (inventoryAdjMan.Adjustment_Details.Count != 0)
            {
                context.Inventory_Adjustment.Add(inventoryAdjMan);
                context.SaveChanges();
            }
            if (inventoryAdjSup.Adjustment_Details.Count != 0)
            {
                context.Inventory_Adjustment.Add(inventoryAdjSup);
                context.SaveChanges();
            }
        }
    }
}