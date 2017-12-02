using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using SSISTeam2.Classes.Models;

namespace SSISTeam2
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {
            SSISEntities context = new SSISEntities();
            string username = CreateUserWizard1.UserName;
            DropDownList Department = (DropDownList)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Department");
            DropDownList Role = (DropDownList)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Role");
            TextBox Fullname = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Fullname");
            TextBox MobileNum = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("MobileNo");

            string department = Department.SelectedValue;
            string role = Role.SelectedValue;
            string fullname = Fullname.Text;
            string mobilenum = MobileNum.Text;

            Dept_Registry user = new Dept_Registry();
            user.username = username;
            user.fullname = fullname;
            user.dept_code = department;
            user.deleted = "N";
            user.mobile_no = mobilenum;
            context.Dept_Registry.Add(user);
            context.SaveChanges();

            Roles.AddUserToRole(username, role);
            Label1.Text = role;

            Response.Redirect("notifysuccess.aspx");
        }
    }
}