using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class ReceiveOrder : System.Web.UI.Page
    {
        SSISEntities context = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                int orderID = Convert.ToInt32(Session["order"]);

                DateTime today = DateTime.Today;
                string todayDate = string.Format("{0:MMMM d,yyyy}", today);

                deliverydate.Text = todayDate;
                var list= context.Purchase_Order_Details.Where(x => x.order_id == orderID && x.status == "Pending").Select(x => new { x.order_details_id, x.Tender_List_Details.Stock_Inventory.item_description, x.quantity }).ToList();
                if (list.Count == 0)
                {
                    confirm.Visible = false;
                }

                GridView1.DataSource = list;
                GridView1.DataBind();
            }


        }

        protected void Confirm(object sender, EventArgs e)
        {
            int orderID = Convert.ToInt32(Session["order"]);

            Delivery_Orders d = new Delivery_Orders();

            d.clerk_user = User.Identity.Name;
            d.deleted = "N";
            d.order_id = orderID;
            d.receive_date = DateTime.Parse(deliverydate.Text);

            context.Delivery_Orders.Add(d);
            context.SaveChanges();

            List<Purchase_Order_Details> podList = context.Purchase_Order_Details.Where(x => x.order_id == orderID).ToList<Purchase_Order_Details>();

            foreach (Purchase_Order_Details pod in podList)
            {
                pod.status = "Completed";
                context.SaveChanges();
            }


            //Get DeliveryOrderID
            Delivery_Orders order = context.Delivery_Orders.Where(x => x.clerk_user == User.Identity.Name).OrderBy(x => x.delivery_id).ToList().Last();

        

            foreach (GridViewRow rows in GridView1.Rows)
            {
                TextBox qty = (TextBox)rows.FindControl("quantity");
                TextBox r = (TextBox)rows.FindControl("remark");

                Label exp_quantity = (Label)rows.FindControl("Label_quantity");

                int exp_qty =Convert.ToInt32( exp_quantity.Text);

                Label itemdesc = (Label)rows.FindControl("Label_itemDesc");
                Label orderdetailid = (Label)rows.FindControl("Label_OrderDetailId");

                string itemName = itemdesc.Text;
                int quantity = Convert.ToInt32(qty.Text);


                string remarks = r.Text;
                int orderDetail = Convert.ToInt32(orderdetailid.Text);

                Stock_Inventory item = context.Stock_Inventory.Where(x => x.item_description == itemName).First();
                int currentQuantity = item.current_qty;
                item.current_qty = currentQuantity + quantity;
                context.SaveChanges();

                Purchase_Order_Details pod = context.Purchase_Order_Details.Where(x => x.order_details_id == orderDetail).First();

                Delivery_Details dd = new Delivery_Details();
                dd.delivery_id = order.delivery_id;
                dd.deleted = "N";
                dd.tender_id = pod.tender_id;
                dd.quantity = quantity;
                dd.remarks = remarks;

                context.Delivery_Details.Add(dd);
                context.SaveChanges();

            }


            Response.Redirect("~/views/storeclerk/viewpendingorder.aspx");


        }

  

    }
}