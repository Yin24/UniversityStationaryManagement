using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.Employee
{
    public partial class EmpRequestDetail : System.Web.UI.Page
    {
        SSISEntities context = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            //selectreqid = Int16.Parse(Request.QueryString["key"]); 
            int requestID = Convert.ToInt32(Request.QueryString["requestid"]);
            if (!IsPostBack)
            {
                FillPage(requestID);
            }
        }
        
        private void FillPage(int reqid)
        {
            /* need to login
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/login.aspx?return=Views/StoreClerk/MakeNewRequest.aspx");
            }*/

            //UserModel currentUser = new UserModel(User.Identity.Name);

            //string username = User.Identity.Name.ToString();
            //UserModel user = new UserModel(username);
            
            Request req = context.Requests.Find(reqid);
            lblreqid.Text = req.request_id.ToString();
            lblDate.Text = req.date_time.ToString();
            lblstatus.Text = req.current_status;
            lblemployeename.Text = req.Dept_Registry.fullname;
            lblcomment.Text = req.reason;
            
            var itemInfos = (from x in context.Requests
                     where x.request_id  == reqid
                     join re in context.Request_Details on x.request_id equals re.request_id
                     join de in context.Request_Event on re.request_detail_id equals de.request_detail_id 
                     join si in context.Stock_Inventory on re.item_code equals si.item_code 
                     join cat in context.Categories on si.cat_id equals cat.cat_id
                     select new
                     {
                         cat.cat_name ,
                         si.item_description ,
                         si.unit_of_measure,
                         re.orig_quantity
                     }).ToList();

            itemInfos = itemInfos.Where(w => w.orig_quantity > 0).ToList();

            GridView2.DataSource = itemInfos;
            GridView2.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmpRequestHistory.aspx");
        }
    }
}