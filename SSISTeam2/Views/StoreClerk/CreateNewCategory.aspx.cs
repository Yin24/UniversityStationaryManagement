using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2
{
    public partial class CreateNewCategory : System.Web.UI.Page
    {
        SSISEntities s = new SSISEntities();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string cat_name = TextBox2.Text;
            //try
            //{
                using (SSISEntities entities = new SSISEntities())
                {
                    Category c = new Category();
                    //c.cat_id = entities.Categories.ToList().Last().cat_id+1;
                    c.cat_name = cat_name;
                    c.deleted = "N";
                    entities.Categories.Add(c);

                    entities.SaveChanges();
                }
            Response.Redirect("ChangeCategoryName1.aspx");




            //}
            //catch (DbEntityValidationException exp)
            //{
            //    throw exp;
            //    Response.Write(exp.EntityValidationErrors.ToString());
            //}

        }

        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangeCategoryName1.aspx");
        }
    }
}
