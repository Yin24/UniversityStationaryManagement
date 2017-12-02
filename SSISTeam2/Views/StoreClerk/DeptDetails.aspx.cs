using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class DeptDetails : System.Web.UI.Page
    {
        SSISEntities context = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            //selectreqid = Int16.Parse(Request.QueryString["key"]); 
            string deptcode = Request.QueryString["deptcode"].ToString();
            if (!IsPostBack)
            {
                FillPage(deptcode);
            }
        }

        private void FillPage(string deptcode)
        {
            Department d = context.Departments.Find(deptcode);
            Collection_Point cp = context.Collection_Point.Where(x => x.collection_pt_id == d.collection_point).First();
            lbldeptname.Text = d.name;
            lblhead.Text = new UserModel( d.head_user ).Fullname;
            lblrep.Text = new UserModel(d.rep_user).Fullname;
            lblconname.Text = new UserModel(d.contact_user).Fullname;
            lblconnum.Text = d.contact_num;
            lblfax.Text = d.fax_num ;
            lblcollpoint.Text = cp.location + " on " + cp.day_of_week + " at " + cp.date_time.ToShortTimeString();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewDepartmentList.aspx");
        }
    }
}