using SSISTeam2.Classes;
using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class PurchaseOrder : System.Web.UI.Page
    {
        SSISEntities s = new SSISEntities();
        decimal total = Convert.ToDecimal(0.0);
 
        protected void Page_Load(object sender, EventArgs e)
        {
           
                if (!IsPostBack)
                {
                    string itemcode = Request.QueryString["itemcode"];
                    Calendar c = new Calendar();


                    HashSet<OrderDetailsView> orderList = (HashSet<OrderDetailsView>)Session["tender"];
                    if (orderList == null)
                    {
                        LabelOrderSummary.Text = "Your order is empty.";
                    }

                    else
                    {
                        string newText = string.Format("You have {0} items", orderList.Count);
                        LabelOrderSummary.Text = newText;

                        GridView1.DataSource = orderList;
                        GridView1.DataBind();

                        order.Visible = true;

                        foreach (OrderDetailsView o in orderList)
                        {
                            total += o.Quantity * o.Price;
                        }

                        Lbl_total.Text = total.ToString();
                    }

                

             
               
            }





        }

        protected void quantity_TextChanged(object sender, EventArgs e)
        {
            TextBox text = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)text.NamingContainer;

            int quantity = Convert.ToInt32(((TextBox)gvr.FindControl("quantity")).Text);

            HashSet<OrderDetailsView> orderList = (HashSet<OrderDetailsView>)Session["tender"];

            string itemdesc = ((Label)gvr.FindControl("Label_ItemDesc")).Text;





            foreach (OrderDetailsView o in orderList)
            {
                total += quantity * o.Price;
                if (o.ItemDesc == itemdesc)
                {
                    o.Quantity = quantity;
                }
            }

            Lbl_total.Text = total.ToString();


        }


        protected void MakeOrder(object sender, EventArgs e)
        {




            HashSet<OrderDetailsView> orderList = (HashSet<OrderDetailsView>)Session["tender"];

            IEnumerable<IGrouping<string, OrderDetailsView>> groups= orderList.GroupBy(k => k.SupplierId);
            // Create new Order
          
           
            foreach (var o in groups)
            {
                Purchase_Order po = new Purchase_Order();
                po.supplier_id = o.Key;
                po.clerk_user = User.Identity.Name;
                po.delivery_by = DateTime.Parse(deliverydate.Text);
                po.order_date = DateTime.Today;
                po.deliver_address = "1 University Road, #01-00 Store Warehouse, Singapore 786541";
                po.deleted = "N";
                s.Purchase_Order.Add(po);
                s.SaveChanges();


                // Get OrderID
                Purchase_Order createdorder = s.Purchase_Order.Where(x => x.clerk_user == User.Identity.Name).OrderBy(x => x.order_id).ToList().Last();

                

                foreach (var item in o)
                {
                    Purchase_Order_Details orderDetail = new Purchase_Order_Details();
                    orderDetail.order_id = createdorder.order_id;
                    orderDetail.tender_id = item.TenderId;
                    orderDetail.quantity = item.Quantity;
                    orderDetail.status = "Pending";
                    orderDetail.cancelled = "N";
                    orderDetail.deleted = "N";
                    s.Purchase_Order_Details.Add(orderDetail);
                    s.SaveChanges();
                }
                
            }

            UserModel currentUserModel = new UserModel(User.Identity.Name);

            /* Email logic */
            string fromEmail = currentUserModel.Email;
            string fromName = currentUserModel.Fullname;
            UserModel deptHead = currentUserModel.FindDelegateOrDeptHead();
            string toEmail = deptHead.Email;
            string toName = deptHead.Fullname;

            string subject = string.Format("Your purchase order confirmation, {0}", fromName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Dear " + fromName + ",");
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");
            sb.AppendLine(string.Format("You have sent a purchase order."));
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");
            sb.AppendLine(string.Format("Please <a href=\"{0}\">follow this link to view the request</a>.", "https://rebrand.ly/ssis-store-receiveorder"));
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");
            sb.AppendLine("Thank you.");
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");
            sb.AppendLine("<i>This message was auto-generated by the Stationery Store Inventory System.</i>");

            string body = sb.ToString();

            new Emailer(fromEmail, fromName).SendEmail(fromEmail, fromName, subject, body);
            /* End of email logic */


            Session["tender"] = null;
            Session["item"] = null;

            Response.Redirect("~/Views/StoreClerk/PurchaseOrderSuccess.aspx");


            











        }






    }


}