using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class MakeOrder : System.Web.UI.Page
    {
        SSISEntities s = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GridView1.DataSource = s.Stock_Inventory.Where(x => x.deleted == "N").ToList<Stock_Inventory>();
                GridView1.DataBind();
            }
            
        }

        protected void CreateOrder(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            bool duplicate = false;


            string itemcode = ((Label)gvr.FindControl("Label_ItemCode")).Text;

            SSISEntities context = new SSISEntities();
            Stock_Inventory item = context.Stock_Inventory.Where(x => x.item_code == itemcode).First();

            HashSet<Stock_Inventory> itemList = (HashSet<Stock_Inventory>)Session["item"];

            if (itemList == null)
            {
                itemList = new HashSet<Stock_Inventory>();
                itemList.Add(item);
                lblResult.Visible = true;
                lblduplicate.Visible = false;
                lblResult.Text = "Added Item Code (" + itemcode + ") Successfully";
                Session["item"] = itemList;
            }

            else
            {



                foreach (Stock_Inventory s in itemList)
                {

                    if (s.item_code == item.item_code)
                    {
                        duplicate = true;
                        break;
                    }

                }

                if (!duplicate)
                {

                    itemList.Add(item);
                    lblResult.Visible = true;
                    lblduplicate.Visible = false;
                    lblResult.Text = "Added Item Code (" + itemcode + ") Successfully";
                    Session["item"] = itemList;
                }
                else
                {
                    lblduplicate.Visible = true;
                    lblResult.Visible = false;
                    lblduplicate.Text = "Duplicate item!";
                    Session["item"] = itemList;
                }

            }





        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                GridView1.PageIndex = 0;
            }
            else
            {
                GridView1.PageIndex = e.NewPageIndex;
            }

            GridView1.DataSource = s.Stock_Inventory.Where(x => x.deleted == "N").ToList<Stock_Inventory>();
            GridView1.DataBind();

        }

        protected void GridView_EditBooks_DataBound(object sender, EventArgs e)
        {
            GridViewRow topPagerRow = GridView1.TopPagerRow;
            GridViewRow bottomPagerRow = GridView1.BottomPagerRow;

            DropDownList topJumpToPage = (DropDownList)topPagerRow.FindControl("DropDownList_JumpToPage");
            DropDownList bottomJumpToPage = (DropDownList)bottomPagerRow.FindControl("DropDownList_JumpToPage");

            if (topJumpToPage != null)
            {
                for (int i = 0; i < GridView1.PageCount; i++)
                {
                    ListItem item = new ListItem("Page " + (i + 1));
                    topJumpToPage.Items.Add(item);
                    bottomJumpToPage.Items.Add(item);
                }
            }

            topJumpToPage.SelectedIndex = GridView1.PageIndex;
            bottomJumpToPage.SelectedIndex = GridView1.PageIndex;
        }


        protected void DropDownList_JumpToPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow topPagerRow = GridView1.TopPagerRow;
            GridViewRow bottomPagerRow = GridView1.BottomPagerRow;

            DropDownList topJumpToPage = (DropDownList)topPagerRow.FindControl("DropDownList_JumpToPage");
            DropDownList bottomJumpToPage = (DropDownList)bottomPagerRow.FindControl("DropDownList_JumpToPage");

            if ((DropDownList)sender == bottomJumpToPage)
            {
                GridView1.PageIndex = bottomJumpToPage.SelectedIndex;
            }
            else
            {
                GridView1.PageIndex = topJumpToPage.SelectedIndex;

            }


            GridView1.DataSource = s.Stock_Inventory.Where(x => x.deleted == "N").ToList<Stock_Inventory>();
            GridView1.DataBind();

        }
    }
}