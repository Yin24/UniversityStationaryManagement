using SSISTeam2.Classes;
using SSISTeam2.Classes.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.DepartmentHead
{
    public partial class ApproveReject : System.Web.UI.Page
    {
        SSISEntities context = new SSISEntities();
        int selectReqId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["key"] == null)
            {
                lblInfo.Text = "No request to show.";
                return;
            }

            selectReqId = Int16.Parse(Request.QueryString["key"]);
            Label1.Text = selectReqId.ToString();

            string currentDeptcode = Session["deptcode"].ToString();

            if (!IsPostBack)
            {
                //get current selected Request details
                var itemAndQuantities = (from r in context.Requests
                         join de in context.Request_Details on r.request_id equals de.request_id
                         join st in context.Stock_Inventory on de.item_code equals st.item_code
                         join v in context.Request_Event on de.request_detail_id equals v.request_detail_id
                         where de.request_id==selectReqId
                         select new
                         {    
                            st.item_description,
                            de.orig_quantity,
                            st.unit_of_measure
                            
                         })
                         .Where(w => w.orig_quantity > 0)
                         .ToList();
                GridView1.DataSource = itemAndQuantities;
                GridView1.DataBind();


                lbRqDate.Text = context.Requests.Where(x => x.request_id == selectReqId).Select(y => y.date_time).First().ToString();
                //change username to fullname
                string userName = context.Requests.Where(x => x.request_id == selectReqId).Select(y => y.username).First().ToString();
                lbRqEmp.Text = context.Dept_Registry.Where(x => x.username == userName).Select(x => x.fullname).First().ToString();
                lblRequestReason.Text = context.Requests.Where(x => x.request_id == selectReqId).Select(y => y.reason).First().ToString();

                //get last approved Request details,but not receive from store
                var lastApproveReq = context.Requests.Where(x => x.current_status == "Approved" && x.dept_code == currentDeptcode).ToList();
                if (lastApproveReq.Count == 0)
                {
                    Table2.Visible = false;
                    lbLastApp.Text = "There is no requested form to the Store!";
                    return;
                }
                else
                {
                    int lastApproveReqId = lastApproveReq.Max(y => y.request_id);

                    var w = (from r in context.Requests
                             join de in context.Request_Details on r.request_id equals de.request_id
                             join st in context.Stock_Inventory on de.item_code equals st.item_code
                             join v in context.Request_Event on de.request_detail_id equals v.request_detail_id
                             where de.request_id == lastApproveReqId
                             select new
                             {
                                 st.item_description,
                                 de.orig_quantity,
                                 st.unit_of_measure
                             })
                             .Where(ww => ww.orig_quantity > 0)
                             .ToList();

                    lbLastReqID.Text = lastApproveReqId.ToString();
                    var req = context.Requests.Where(x => x.request_id == lastApproveReqId);
                    lbLastReqDate.Text = req.First().date_time.ToString();
                    lbLastReqEmp.Text = req.First().username;

                    GridView2.DataSource = w;
                    GridView2.DataBind();
                }


            }
        }

        //Approve
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            //change status in "Request"
            var req = context.Requests.SingleOrDefault(x => x.request_id == selectReqId);
            req.current_status = "Approved";
            req.rejected_reason = tbReason.Text;

            //change status in "Request Event"
            var requestDetailIds = from r in context.Requests
                            join de in context.Request_Details on selectReqId equals de.request_id
                            select de.request_detail_id;
            List<int> reqDetails = requestDetailIds.ToList();
            int[] detAry =reqDetails.ToArray();
            for (int i=0; i<detAry.Length; i++)
            {
                changeAppStatusEvent(detAry[i]);
            }          
  
            lbAppRej.Text = "Successfully Approved!";

            _sendEmail(true, User.Identity.Name, req);
            context.SaveChanges();
            Response.Redirect("ViewPending.aspx");
        }

        public void changeAppStatusEvent(int detailId)
        {
            var requestEvent = from de in context.Request_Details
                     join ev in context.Request_Event on detailId equals ev.request_detail_id
                     select ev;

            List<Request_Event> eve = requestEvent.ToList();
            foreach(Request_Event e in eve)
            {
                e.status = "Approved";
            }
            //ent.SaveChanges();
            
        }

        //Reject
        protected void btnReject_Click(object sender, EventArgs e)
        {
            //change status in "Request"
            var req = context.Requests.SingleOrDefault(x => x.request_id == selectReqId);
            req.current_status = "Rejected";
            req.rejected = "Y";
            req.rejected_reason = tbReason.Text;

            //change status in "Request Event"
            var requestDetailIds = from r in context.Requests
                    join de in context.Request_Details on selectReqId equals de.request_id
                    select de.request_detail_id;
            List<int> reqDetails = requestDetailIds.ToList();
            int[] detAry = reqDetails.ToArray();
            for (int i = 0; i < detAry.Length; i++)
            {
                changeRejStatusEvent(detAry[i]);
            }
            context.SaveChanges();
            //
            lbAppRej.Text = "Successfully Rejected!";

            _sendEmail(false, User.Identity.Name, req);
        }
        public void changeRejStatusEvent(int detailId)
        {
            var requestEvents = from de in context.Request_Details
                    join ev in context.Request_Event on detailId equals ev.request_detail_id
                    select ev;

            List<Request_Event> eve = requestEvents.ToList();
            foreach (Request_Event e in eve)
            {
                e.status = "Rejected";
            }
            context.SaveChanges();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewPending.aspx");
        }


        private void _sendEmail(bool approved, string username, Request req)
        {
            string reqReason = req.reason;
            string decision = approved ? "approved" : "rejected";
            bool hasReason = tbReason.Text == "" ? false : true;

            /* Email logic */
            UserModel currentUserModel = new UserModel(username);

            string fromEmail = currentUserModel.Email;
            string fromName = currentUserModel.Fullname;
            UserModel employee = new UserModel(req.username);
            string toEmail = employee.Email;
            string toName = employee.Fullname;

            string subject = string.Format("Request {0}", decision);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Dear " + toName + ",");
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");
            sb.AppendLine(string.Format("Your department head has {0} your request.", decision));
            if (hasReason)
            {
                sb.AppendLine("<br />");
                sb.AppendLine(string.Format("The reason given was: {0}", tbReason.Text));
            }
            sb.AppendLine("<br />");
            sb.AppendLine(string.Format("The request's id is {0}, with the reason: {1}", req.request_id, req.reason));
            sb.AppendLine("<br />");
            sb.AppendLine(string.Format("Please <a href=\"{0}\">follow this link to view your requests</a>.", "https://rebrand.ly/ssis-deptrequests"));
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");
            sb.AppendLine("Thank you.");
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");
            sb.AppendLine("<i>This message was auto-generated by the Stationery Store Inventory System.</i>");

            string body = sb.ToString();

            new Emailer(fromEmail, fromName).SendEmail(toEmail, toName, subject, body);
            /* End of email logic */
        }

    }
}
