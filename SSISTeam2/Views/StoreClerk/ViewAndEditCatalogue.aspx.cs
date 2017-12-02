using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class ViewCatalogueForm : System.Web.UI.Page
    {
        SSISEntities entities = new SSISEntities();
        List<ViewandEditCatalogueForShow> list = new List<ViewandEditCatalogueForShow>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()

        {
            //Added for next button click
            string searchWord = TextBox1.Text;
            if (!string.IsNullOrEmpty(searchWord))
            {
                list = new List<ViewandEditCatalogueForShow>();
                var catList = entities.Categories.Where(x => x.deleted == "N" && x.cat_name.Contains(searchWord)).Select(x => x.cat_id).ToList();
                var result2 = (from t1 in entities.Categories
                               join t2 in entities.Stock_Inventory
                               on t1.cat_id equals t2.cat_id
                               where t1.deleted.Equals("N")
                               && t2.deleted.Equals("N")
                               && (catList.Contains(t2.cat_id))
                               || t2.item_description.Contains(searchWord)
                               || t2.item_code.Contains(searchWord)
                               orderby t1.cat_name
                               select new { t2.item_code, t1.cat_name, t2.item_description, t2.current_qty, t2.reorder_level, t2.reorder_qty, t2.unit_of_measure }).ToList();

                for (int i = 0; i < result2.Count(); i++)
                {
                    ViewandEditCatalogueForShow vec = new ViewandEditCatalogueForShow();
                    vec.itemNumber = result2[i].item_code;
                    vec.categoryName = result2[i].cat_name;
                    vec.Description = result2[i].item_description;
                    vec.CQ = result2[i].current_qty;
                    vec.RQ = result2[i].reorder_level;
                    vec.RL = result2[i].reorder_level;
                    vec.UoM = result2[i].unit_of_measure;
                    list.Add(vec);
                }

                GridView1.DataSource = list;

            }
            else
            {
                List<Stock_Inventory> SIList = entities.Stock_Inventory.Where(x => x.deleted.Equals("N")).ToList();
                list = new List<ViewandEditCatalogueForShow>();
                foreach (Stock_Inventory si in SIList)
                {
                    ViewandEditCatalogueForShow vec = new ViewandEditCatalogueForShow();
                    vec.itemNumber = si.item_code;
                    vec.Description = si.item_description;

                    vec.categoryName = (from t1 in entities.Categories
                                        where t1.deleted.Equals("N")
                                         && (t1.cat_id == si.cat_id)
                                        select new { t1.cat_name }).ToList().First().cat_name;
                    vec.RL = si.reorder_level;
                    vec.RQ = si.reorder_qty;
                    vec.UoM = si.unit_of_measure;
                    vec.CQ = si.current_qty;
                    vec.CategoryId = si.cat_id;

                    list.Add(vec);
                }
                GridView1.Columns[1].Visible = false;
                GridView1.DataSource = list;

            }
            GridView1.DataBind();

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

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.BindGrid();
            List<Category> categoryList = entities.Categories.ToList();
            List<string> categoryNameList = new List<string>();
            foreach (Category c in categoryList)
            {
                categoryNameList.Add(c.cat_name);
            }
            (GridView1.Rows[e.NewEditIndex].FindControl("DropDownList1") as DropDownList).Visible = true;
            (GridView1.Rows[e.NewEditIndex].FindControl("DropDownList1") as DropDownList).DataSource = categoryNameList;
            (GridView1.Rows[e.NewEditIndex].FindControl("DropDownList1") as DropDownList).DataBind();
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string categoryName = (row.FindControl("DropDownList1") as DropDownList).SelectedItem.Text;
            string description = (row.FindControl("TextBox2") as TextBox).Text;
            //int curQty = Convert.ToInt32((row.FindControl("TextBox6") as TextBox).Text);
            int reorderLevel = Convert.ToInt32((row.FindControl("TextBox3") as TextBox).Text);
            int reorderQuantity = Convert.ToInt32((row.FindControl("TextBox4") as TextBox).Text);
            string unitOfMeasure = (row.FindControl("TextBox5") as TextBox).Text;
            string itemCode = (row.FindControl("Label1") as Label).Text;
            //int categoryID = Convert.ToInt32((row.FindControl("Label9") as Label).Text);
            var result1 = entities.Stock_Inventory.SingleOrDefault(x => x.item_code == itemCode);
            // result1.current_qty = curQty;
            result1.reorder_level = reorderLevel;
            result1.reorder_qty = reorderQuantity;
            result1.unit_of_measure = unitOfMeasure;
            result1.item_description = description;
            entities.SaveChanges();
            Category c = entities.Categories.Where(x => x.cat_name == categoryName).First();
            result1.cat_id = c.cat_id;
            //var result2 = entities.Categories.SingleOrDefault(x => x.cat_id == categoryID);
            //result2.cat_name = categoryName;
            //entities.SaveChanges();
            GridView1.EditIndex = -1;
            this.BindGrid();
            (row.FindControl("DropDownList1") as DropDownList).Visible = false;
        }
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];

            string itemCode = (row.FindControl("Label1") as Label).Text;

            var result1 = entities.Stock_Inventory.SingleOrDefault(x => x.item_code == itemCode);
            result1.deleted = "Y";
            entities.SaveChanges();
            GridView1.EditIndex = -1;
            this.BindGrid();

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != null)
            {
                //GridView1.DataSource = SearchCatagories(TextBox1.Text);
                //GridView1.DataBind();
            }
        }

        private object SearchCatagories(string text)
        {
            throw new NotImplementedException();
        }
        protected void Search_Click(object sender, EventArgs e)
        {

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

    class ViewandEditCatalogueForShow
    {
        public string itemNumber { get; set; }
        public string categoryName { get; set; }
        public string Description { get; set; }
        public int CQ { get; set; }
        public int RL { get; set; }
        public int RQ { get; set; }
        public string UoM { get; set; }
        public int CategoryId { get; set; }
        public string deleted { get; set; }


    }

}
