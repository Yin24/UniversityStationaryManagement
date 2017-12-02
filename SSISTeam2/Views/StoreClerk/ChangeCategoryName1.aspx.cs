using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2
{
    public partial class ChangeCategoryName1 : System.Web.UI.Page
    {
        SSISEntities s = new SSISEntities();

        protected void Page_Load(object sender, EventArgs e)

        {
            if (!this.IsPostBack)
            {
                BindGrid();

            }
        }

        private void BindGrid()

        {
            
            GridView1.DataSource = s.Categories.Where(x => x.deleted != "Y").ToList();
            GridView1.DataBind();
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
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)

        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGrid();
            GridViewRow gvr = GridView1.Rows[e.NewEditIndex];
            LinkButton linkEdit = (LinkButton)gvr.FindControl("Edit");
            LinkButton linkDelete = (LinkButton)gvr.FindControl("Delete");

        }
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int cat_id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string cat_name = (row.FindControl("TextBox2") as TextBox).Text;


            using (SSISEntities entities = new SSISEntities())
            {
                Category category = entities.Categories.Where(p => p.cat_id == cat_id).First<Category>();
                //SSISEntities.cat_id = cat_id;
                category.cat_name = cat_name;
                entities.SaveChanges();
            }
            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int cat_id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            using (SSISEntities entities = new SSISEntities())
            {
                Category category = entities.Categories.Where(p => p.cat_id == cat_id).First<Category>();
                //check if there are any items under this cat 
                int checkcatid= category.cat_id;
                List<Stock_Inventory> lsi = entities.Stock_Inventory.Where(x => x.cat_id == checkcatid).ToList();
                if (lsi.Count == 0)
                {
                    category.deleted = "Y";
                    entities.SaveChanges();
                    Message.Text = "";
                }
                else
                {
                    Message.Text = "You cannot delete a category that has items within it.";
                }
                
            }
            this.BindGrid();
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox3.Text != null)
            {

                
                GridView1.DataSource = SearchCatagories(TextBox3.Text);
                GridView1.DataBind();
            }
            Message.Text = "";
        }
        public List<Category> SearchCatagories(string name)
        {
            List<Category> lc = s.Categories.Where(s => s.cat_name.Contains(name)).ToList();
            return lc;
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateNewCategory.aspx");
        }
    }
}