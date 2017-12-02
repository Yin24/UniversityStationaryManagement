using SSISTeam2.Classes.EFFacades;
using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2
{
    public partial class DEMO_ViewRequestDetail : System.Web.UI.Page
    {
        private const string SESSION_REQUEST_ID = "ViewRequestDetail_RequestId";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Hide buttons first
                panelApproval.Visible = false;

                string requestedStr = Request.QueryString["id"];
                int requestId;
                if (requestedStr == null) return;

                int.TryParse(requestedStr, out requestId);

                if (requestId == 0) return;

                RequestModel request;

                using (SSISEntities context = new SSISEntities())
                {
                    request = FacadeFactory.getRequestService(context).findRequestById(requestId);

                    if (request == null)
                    {
                        lblInfo.Text = "Could not find that request";
                        return;
                    }
                }

                Session[SESSION_REQUEST_ID] = requestId;

                lblRequestId.Text += requestedStr;
                lblEmployeeName.Text += request.Username;
                lblReason.Text += request.Reason;
                lblInfo.Text = "Status: " + request.Status;

                Dictionary<string, int> items = request.Items
                    .Where(w => w.Value > 0)
                    .ToDictionary(k => k.Key.Description, v => v.Value);

                if (request.Status == RequestStatus.PENDING || request.Status == RequestStatus.UPDATED)
                {
                    panelApproval.Visible = true;
                }

                gvItems.DataSource = items;
                gvItems.DataBind();
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            using (SSISEntities context = new SSISEntities())
            {
                RequestModel request = FacadeFactory.getRequestService(context).findRequestById((int) Session[SESSION_REQUEST_ID]);
                FacadeFactory.getRequestService(context).approveRequest(request, "Low Kway Boo");

                context.SaveChanges();
            }

            Response.Redirect("DEMO_ApproveRequest.aspx?id=" + (int)Session[SESSION_REQUEST_ID]);
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            using (SSISEntities context = new SSISEntities())
            {
                RequestModel request = FacadeFactory.getRequestService(context).findRequestById((int)Session[SESSION_REQUEST_ID]);
                FacadeFactory.getRequestService(context).rejectRequest(request, "Low Kway Boo");

                context.SaveChanges();
            }

            Response.Redirect("DEMO_ApproveRequest.aspx?id=" + (int)Session[SESSION_REQUEST_ID]);
        }
    }
}