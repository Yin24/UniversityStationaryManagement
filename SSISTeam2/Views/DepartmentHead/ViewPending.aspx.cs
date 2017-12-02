using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.DepartmentHead
{
    public partial class ViewPending : System.Web.UI.Page
    {
        SSISEntities context = new SSISEntities();
        string userName = null;
        string currentDeptCode = null;
        string id = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //???(actual)
            userName = User.Identity.Name;
            UserModel user = new UserModel(userName);
            currentDeptCode = user.Department.dept_code.ToString();

            //(testing example)
            //currentDeptCode = "REGR";

            if (!IsPostBack)
            {
                var requestInfo = (from r in context.Requests
                         where r.dept_code == currentDeptCode && (r.current_status == "Pending" || r.current_status == RequestStatus.UPDATED)
                         select new
                         {
                             r.request_id,
                             r.username,
                             r.current_status,
                             r.date_time
                         }).ToList();
                if(requestInfo.Count==0)
                {
                    Label1.Text = "No Pending request!";
                }
                    GridView1.DataSource = requestInfo;
                    GridView1.DataBind();


            }
        }

        protected void lbReqId_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            Session["deptcode"] = currentDeptCode;

            id = ((Label)gvr.FindControl("lbReqId")).Text;
            Response.Redirect("ApproveReject.aspx?key=" + id);
            
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }


        //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //Bound Field
        //    id = GridView1.SelectedRow.Cells[0].Text;

        //    //Template Field
        //    //id = (GridView1.SelectedRow.FindControl("lbReqId") as Label).Text;
        //    Response.Redirect("ApproveReject.aspx?key=" + id);
        //    //Response.Redirect("WebForm1.aspx");
        //}

    }
}