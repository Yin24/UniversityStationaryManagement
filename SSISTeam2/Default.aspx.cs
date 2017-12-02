using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.Home
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (! User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/login.aspx");
            }

            string currentUser = User.Identity.Name;

            UserModel userModel;
            //try
            //{
                userModel = new UserModel(currentUser);

                if (userModel.isDelegateHead() || userModel.isDeptHead() || userModel.isStoreManager())
                {
                    Response.Redirect("~/Views/DepartmentHead/HeadDashboard.aspx");
                }
                else if (userModel.isStoreClerk() || userModel.isStoreSupervisor())
                {
                    Response.Redirect("~/Views/StoreClerk/Dashboard.aspx");
                }
                else if (userModel.isEmployee())
                {
                    Response.Redirect("~/Views/Employee/EmpDashboard.aspx");
                }
                else
                {

                    if (User.IsInRole("DeptHead") || User.IsInRole("Manager"))
                    {
                        Response.Redirect("~/Views/DepartmentHead/HeadDashboard.aspx");
                    }
                    else if (User.IsInRole("Clerk") || User.IsInRole("Supervisor"))
                    {
                        Response.Redirect("~/Views/StoreClerk/Dashboard.aspx");
                    }
                    else if (User.IsInRole("Employee"))
                    {
                        Response.Redirect("~/Views/Employee/EmpDashboard.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/login.aspx");
                    }
                }
            //} catch (Exception)
            //{
            //    Response.Redirect("~/login.aspx");
            //}

           


            SSISEntities context = new SSISEntities();
            Category cat = context.Categories.Where(x => x.cat_id == 1).ToList().First();
            Label1.Text = cat.cat_name;
        }
    }
}