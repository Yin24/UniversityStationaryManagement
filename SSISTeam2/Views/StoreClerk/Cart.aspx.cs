using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class Cart : System.Web.UI.Page
    {

        SSISEntities context = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                List<string> itemnameList = new List<string>();
                HashSet<Stock_Inventory> itemList = (HashSet<Stock_Inventory>)Session["item"];


                if (itemList == null)
                {
                    LabelOrderSummary.Text = "Your cart is empty.";
                }

                else
                {
                    string newText = string.Format("You have {0} items", itemList.Count);
                    LabelOrderSummary.Text = newText;

                    foreach (Stock_Inventory s in itemList)
                    {
                        string name = s.item_description;
                        itemnameList.Add(name);
                    }

                    DropDownList1.DataSource = itemnameList;
                    DropDownList1.DataBind();
                    string itemname = itemnameList[0];
                    GridView1.DataSource = context.Tender_List_Details.Where(x => x.Stock_Inventory.item_description == itemname).Select(x => new { x.tender_id, x.item_code, x.Stock_Inventory.item_description, x.Tender_List.Supplier.name, x.Tender_List.Supplier.contact_num, x.rank, x.price }).ToList();
                    GridView1.DataBind();



                }


            }


            HashSet<OrderDetailsView> orderList = (HashSet<OrderDetailsView>)Session["tender"];
            GridView2.DataSource = orderList;
            GridView2.DataBind();




        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = DropDownList1.SelectedValue;

            SSISEntities s = new SSISEntities();
            var list = s.Tender_List_Details.Where(x => x.Stock_Inventory.item_description == name).Select(x => new { x.tender_id, x.item_code, x.Stock_Inventory.item_description, x.Tender_List.Supplier.name, x.Tender_List.Supplier.contact_num, x.rank, x.price }).ToList();


            GridView1.DataSource = list;
            GridView1.DataBind();





        }

        protected void MakeOrder(object sender, EventArgs e)
        {

            bool duplicate = false;
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            int itemcode = Convert.ToInt32(((Label)gvr.FindControl("Label_tenderId")).Text);
            Tender_List_Details td = context.Tender_List_Details.Where(x => x.tender_id == itemcode).First();


            HashSet<OrderDetailsView> itemList = (HashSet<OrderDetailsView>)Session["tender"];


            next.Visible = true;




            if (itemList == null)
            {
                itemList = new HashSet<OrderDetailsView>();
                itemList.Add(new OrderDetailsView(td));
                lblResult.Visible = true;
                lblduplicate.Visible = false;
                lblResult.Text = "Added Item  Successfully";
                Session["tender"] = itemList;

                GridView2.DataSource = itemList;
                GridView2.DataBind();


            }
            else
            {



                foreach (OrderDetailsView s in itemList)
                {

                    if (s.TenderId == td.tender_id)
                    {
                        duplicate = true;
                        break;
                    }

                }

                if (!duplicate)
                {

                    itemList.Add(new OrderDetailsView(td));
                    lblResult.Visible = true;
                    lblduplicate.Visible = false;
                    lblResult.Text = "Added Item Successfully";
                    GridView2.DataSource = itemList;
                    GridView2.DataBind();
                    Session["tender"] = itemList;

                }
                else
                {
                    lblduplicate.Visible = true;
                    lblResult.Visible = false;
                    lblduplicate.Text = "Duplicate item!";
                    Session["tender"] = itemList;
                }

            }

        }

        protected void ButtonRemove_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            string itemdesc = ((Label)gvr.FindControl("Label_ItemDesc")).Text;

            HashSet<OrderDetailsView> itemList = (HashSet<OrderDetailsView>)Session["tender"];
            foreach (OrderDetailsView od in itemList)
            {
                if (od.ItemDesc == itemdesc)
                {
                    itemList.Remove(od);

                    break;
                }
            }

            if (itemList.Count == 0)
            {
                next.Visible = false;
            }
            Session["cart"] = itemList;
            lblResult.Visible = false;
            lblduplicate.Visible = false;
            GridView2.DataSource = itemList;
            GridView2.DataBind();
        }


        protected void Purchase(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/purchaseorder.aspx");
        }
    }


    class OrderDetailsView
    {
        string itemDesc;
        string supplierName;
        decimal price;
        int tenderId;
        int quantity;
        string supplierId;

        public int TenderId { get { return tenderId; } }

        public string ItemDesc { get { return itemDesc; } }

        public string SupplierName { get { return supplierName; } }

        public string SupplierId { get { return supplierId; } }

        public decimal Price { get { return price; } }

        public int  Quantity { get { return quantity; }
            set
            {
                quantity = value;
            }
        }


        public OrderDetailsView()
        { }

        public OrderDetailsView(Tender_List_Details tender)
        {
            fillFields(tender);

        }

        private void fillFields(Tender_List_Details order)
        {
            using (SSISEntities context = new SSISEntities())
            {
                Stock_Inventory item = context.Stock_Inventory.Where(x => x.item_code == order.item_code).First();//Find(order.ISBN);

                itemDesc = item.item_description;
                supplierName = context.Suppliers.Where(x => x.supplier_id == order.Tender_List.supplier_id).Select(x => x.name).First();
                price = order.price;
                tenderId = order.tender_id;
                quantity = item.reorder_qty;
                supplierId = order.Tender_List.supplier_id;
            }
        }



    }
}