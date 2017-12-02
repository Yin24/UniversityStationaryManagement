using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class ViewPendingOrder : System.Web.UI.Page
    {
        SSISEntities s = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!IsPostBack)
                {
                    var orders = s.Purchase_Order.Where(x => x.deleted != "Y" && x.clerk_user == User.Identity.Name).ToList<Purchase_Order>();
                    if (orders != null && orders.Count > 0)
                    {
                        panelNoData.Visible = false;
                        GridView1.DataSource = orders;
                        GridView1.DataBind();
                    } else
                    {
                        panelNoData.Visible = true;
                    }
                }
              
            }
          
        }

        protected void Edit_Order(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            string orderId = ((Label)gvr.FindControl("Label_OrderId")).Text;

            Session["order"] = orderId;

            Response.Redirect("~/Views/StoreClerk/EditOrder.aspx");
        }

        protected void Delete_Order(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            int orderId =Convert.ToInt32( ((Label)gvr.FindControl("Label_OrderId")).Text);

            Purchase_Order order = s.Purchase_Order.Where(x => x.order_id == orderId).First();

            order.deleted = "Y";

            s.SaveChanges();

            Response.Redirect("~/Views/StoreClerk/ViewPendingOrder.aspx");
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

            GridView1.DataSource = s.Purchase_Order.Where(x => x.deleted != "Y" && x.clerk_user == User.Identity.Name).ToList<Purchase_Order>();
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


            GridView1.DataSource = s.Purchase_Order.Where(x => x.deleted != "Y" && x.clerk_user == User.Identity.Name).ToList<Purchase_Order>();
            GridView1.DataBind();

        }
    }
}