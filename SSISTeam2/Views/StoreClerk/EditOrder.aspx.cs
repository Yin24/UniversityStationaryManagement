using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class EditOrder : System.Web.UI.Page
    {
        SSISEntities context = new SSISEntities();
          int orderId;
        protected void Page_Load(object sender, EventArgs e)
        {
            orderId = Convert.ToInt32(Session["order"]);

            if (!IsPostBack)
            {
                               
                var list = context.Purchase_Order_Details.Where(x => x.order_id == orderId && x.deleted != "Y").Select(x => new { x.Tender_List_Details.Stock_Inventory.item_description, x.quantity, x.status, x.order_details_id }).ToList();
                GridView1.DataSource = list;
                GridView1.DataBind();
                if (list.Count != 0)
                {
                    if (list.First().status != "Pending")
                    {
                        finish.Enabled = false;
                        finish.Visible = false;
                    } else
                        finish.Visible = true;
                }
                else
                {
                    lblResult.Visible = true;
                    lblResult.Text = "Oh Snap! Nothing to show!";
                    finish.Visible = false;
                }
            }
           




        }

        protected void delete_OrderItem(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            int orderDetailId = Convert.ToInt32(((Label)gvr.FindControl("Label_OrderDetailId")).Text);

            Purchase_Order_Details order = context.Purchase_Order_Details.Where(x => x.order_details_id == orderDetailId).First();

            

            order.deleted = "Y";

            context.SaveChanges();

            Response.Redirect("~/Views/StoreClerk/EditOrder.aspx");
        }

        protected void ReceiveOrder(object sender, EventArgs e)
        {
            Purchase_Order p =context.Purchase_Order.Where(x => x.order_id == orderId).First();
            Session["suppliername"] = p.Supplier.name;

            Response.Redirect("~/Views/StoreClerk/ReceiveOrder.aspx");
        }
    }
}