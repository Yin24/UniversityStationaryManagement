using SSISTeam2.Classes.EFFacades;
using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.DepartmentHead
{
    public partial class DEMO_ViewAllPending : System.Web.UI.Page
    {
        private const string SESSION_PENDINGS = "ViewAllPending_Pendings";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/login.aspx?return=Views/DepartmentHead/DEMO_ViewAllPending.aspx");
            }

            UserModel user = new UserModel(User.Identity.Name);
            List<RequestModel> pendings;
            using (SSISEntities context = new SSISEntities())
            {
                pendings = FacadeFactory.getRequestService(context).getAllPendingRequestsOfDepartment(user.Department.dept_code).ToList();
            }
            if (pendings.Count > 0)
            {
                gvPendings.DataSource = pendings;
                gvPendings.DataBind();

                lblInfo.Text = pendings.Count + " requests.";
            } else
            {
                lblInfo.Text = "No pending requests.";
            }

            Session[SESSION_PENDINGS] = pendings;
        }

        protected void gvPendings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            List<RequestModel> pendings = (List<RequestModel>) Session[SESSION_PENDINGS];
            if (e.CommandName == "Select")
            {
                int selIndex = Convert.ToInt32(e.CommandArgument.ToString());
                int requestId = pendings[selIndex].RequestId;
                lblInfo.Text = "Selected" + requestId.ToString();

                Response.Redirect("DEMO_ApproveRequest.aspx?id=" + requestId);
            }
        }
    }
}