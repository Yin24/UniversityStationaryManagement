using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.Employee
{
    public partial class EmpDashboard : System.Web.UI.Page
    {
        SSISEntities context = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillPage();
            }
        }

        private void FillPage()
        {
            // need to login
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/login.aspx?return=Views/Employee/EmpDashboard.aspx");
            }

            string currentUser = Page.User.Identity.Name;
            string fullName = "";
            using (SSISEntities context = new SSISEntities())
            {
                fullName = context.Dept_Registry.Find(currentUser).fullname;
            }
            lblFullName.Text = "Welcome, " + fullName;

            string username = User.Identity.Name.ToString();
            UserModel user = new UserModel(username);
            string currentDept = user.Department.dept_code;
            var requestInfo = (from x in context.Requests
                     where username == x.username
                     select new { x.request_id, x.date_time, x.reason, x.current_status}).OrderByDescending(o => o.date_time).Take(3).ToList();
            GridView1.DataSource = requestInfo;
            GridView1.DataBind();

            var q2 = (from x in context.Requests
                     where currentDept  == x.dept_code
                     select new { x.request_id, x.username, x.Dept_Registry.fullname, x.date_time, x.reason, x.current_status }).OrderByDescending(o => o.date_time).Take(3).ToList();
            GridView2.DataSource = q2;
            GridView2.DataBind();
        }

        protected void btnMakeNewReq_Click(object sender, EventArgs e)
        {
            Response.Redirect("MakeNewRequest.aspx");
        }

        protected void btnShowHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmpRequestHistory.aspx");
        }
    }
}