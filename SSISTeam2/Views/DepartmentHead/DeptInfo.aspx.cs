using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.DepartmentHead
{
    public partial class DeptInfo : System.Web.UI.Page
    {
        SSISEntities context = new SSISEntities();
        Department dept = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            //???
            string loginuserDHead = "Jerry";
            string logindepCode = context.Dept_Registry.Where(b => b.username == loginuserDHead).Select(c => c.dept_code).First().ToString();

            //get login a department
            dept = context.Departments.Where(d => d.dept_code == logindepCode).Single();

            LabelDeptName.Text = dept.name.ToString();
            LabelContactName.Text = dept.contact_user.ToString();
            LabelPhNo.Text = dept.contact_num.ToString();
            LabelFaxNo.Text = dept.fax_num.ToString();
            //LabelHeadName.Text = dept.head_user.ToString();
            tbCollectP.Text = dept.Collection_Point1.location.ToString();
            tbRepName.Text = dept.rep_user.ToString();

        }

        protected void BtnChangeCpRn_Click(object sender, EventArgs e)
        {
            Session["sDept"] = dept;
            Response.Redirect("ChangeCollectionnRep.aspx");
        }
    }
}