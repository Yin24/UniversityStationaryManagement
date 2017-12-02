using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.Employee
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String name = "zeck";
            UserModel usermodel = new UserModel(name);
            String dept_code = usermodel.Department.dept_code;
            SSISEntities context = new SSISEntities();
            var result = context.Approval_Duties.Where(x => x.deleted == "N" && x.dept_code.Equals("dept_code")).Select(x => x.end_date).Max();
            Label1.Text = result.ToString();

        }
    }
}