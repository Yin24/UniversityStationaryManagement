using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.Employee
{
    public partial class ViewCat : System.Web.UI.Page
    {
        SSISEntities entities = new SSISEntities();
        List<ViewCatalogueForShow> list = new List<ViewCatalogueForShow>();
        protected void Page_Load(object sender, EventArgs e)
        {
            //GridView1.DataSource = s.Categories.Where(x => x.deleted != "Y").ToList<Category>();
            //GridView1.DataBind();
            if (!this.IsPostBack)
            {
                BindGrid();
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGrid();
            if (e.NewPageIndex < 0)
                GridView1.PageIndex = 0;
            else
                GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
        private void BindGrid()
        {
            //Changes for paging next button click
            string searchWord = TextBox1.Text;
            if (!string.IsNullOrEmpty(searchWord))
            {
                list = new List<ViewCatalogueForShow>();
                var catList = entities.Categories.Where(x => x.deleted == "N" && x.cat_name.Contains(searchWord)).Select(x => x.cat_id).ToList();
                var result2 = (from t1 in entities.Categories
                               join t2 in entities.Stock_Inventory
                               on t1.cat_id equals t2.cat_id
                               where t1.deleted.Equals("N")
                               && t2.deleted.Equals("N")
                               && (catList.Contains(t2.cat_id))
                               || t2.item_description.Contains(searchWord)
                               orderby t1.cat_name
                               select new { t1.cat_name, t2.item_description }).ToList();

                for (int i = 0; i < result2.Count(); i++)
                {
                    ViewCatalogueForShow vec = new ViewCatalogueForShow();
                    vec.categoryName = result2[i].cat_name;
                    vec.Description = result2[i].item_description;
                    list.Add(vec);
                }

                GridView1.DataSource = list;
                
            }else
            {
                List<Stock_Inventory> SIList = entities.Stock_Inventory.Where(x => x.deleted.Equals("N")).ToList();
                list = new List<ViewCatalogueForShow>();
                foreach (Stock_Inventory si in SIList)
                {
                    ViewCatalogueForShow vec = new ViewCatalogueForShow();

                    vec.Description = si.item_description;

                    vec.categoryName = (from t1 in entities.Categories
                                        where t1.deleted.Equals("N")
                                         && (t1.cat_id == si.cat_id)
                                        select new { t1.cat_name }).ToList().First().cat_name;
                    list.Add(vec);
                }
                GridView1.DataSource = list;
                
            }
            GridView1.DataBind();

        }

        protected void Search_Click(object sender, EventArgs e)
        {
            this.BindGrid();
            
            
        }

        class ViewCatalogueForShow
        {
            
            public string categoryName { get; set; }
            public string Description { get; set; }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
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
            this.BindGrid();
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
            this.BindGrid();
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


    }
}