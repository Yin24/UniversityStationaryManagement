using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views
{
    public partial class HeadDashboard : System.Web.UI.Page
    {
        public UserModel userModel;

        SSISEntities context = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            userModel = new UserModel(User.Identity.Name);

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
                Response.Redirect("~/login.aspx?return=Views/DepartmentHead/HeadDashboard.aspx");
            }

            string username = User.Identity.Name.ToString();
            UserModel user = new UserModel(username);
            string currentDept = user.Department.dept_code;
            var requestInfo = (from o in context.Requests
                     where (currentDept == o.dept_code) && (o.current_status == RequestStatus.PENDING | o.current_status == RequestStatus.UPDATED)
                     select new { o.request_id, o.username , o.date_time, o.reason}).ToList();
            GridView1.DataSource = requestInfo;
            GridView1.DataBind();
            string pendingnum = GridView1.Rows.Count.ToString();
            lblPendingNum.Text = "You Have "+ pendingnum + " Requests(s) to View";

            string currentUser = Page.User.Identity.Name;
            string fullName = "";
            using (SSISEntities context = new SSISEntities())
            {
                fullName = context.Dept_Registry.Find(currentUser).fullname;
            }
            lblFullName.Text = "Welcome, " + fullName;

            Department dept = context.Departments.Where(x => currentDept == x.dept_code).First();
            Collection_Point colpoint = context.Collection_Point.Where(y => y.collection_pt_id == dept.collection_point).First();
            Dept_Registry dr = context.Dept_Registry.Where(x => x.username == dept.rep_user).First();
            lblrep.Text = dr.fullname;
            lblcolpoint.Text = colpoint.location + " (" + colpoint.day_of_week + " - " + colpoint.date_time.ToShortTimeString() +")";

            var deldept = (from x in context.Approval_Duties select x.dept_code).ToList();
            if (deldept.Contains(currentDept))
            {
                List<Approval_Duties> ads = context.Approval_Duties.Where(x => currentDept == x.dept_code).ToList();
                foreach (Approval_Duties ad in ads)
                {
                    DateTime start = ad.start_date;
                    DateTime end = ad.end_date;
                    if ((DateTime.Now > start && DateTime.Now < end) && (ad.deleted == "N"))
                    {
                        lbldelegation.Text = ad.username + " (" + start + " - " + end + ")";
                        break;
                    }
                }
            }

           
        }
        
        protected void btnViewAllPending_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/DepartmentHead/ViewPending.aspx");
        }

    

        protected void btnchangecoll_Click(object sender, EventArgs e)
        {
            //Yin
            SSISEntities context = new SSISEntities();
            string loginUser = Page.User.Identity.Name;
            string logindepCode = context.Dept_Registry.Where(b => b.username == loginUser).Select(c => c.dept_code).First().ToString();
            Department dept = context.Departments.Where(d => d.dept_code == logindepCode).Single();
            Session["sDept"] = dept;
            Response.Redirect("~/Views/DepartmentHead/ChangeCollectionnRep.aspx");
        }
        protected void btnShowHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/EmpRequestHistory.aspx");
        }

        protected void btndelegate_Click(object sender, EventArgs e)
        {
            Response.Redirect("DelegateAuthority.aspx");
        }
    }
}
