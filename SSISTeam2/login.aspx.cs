using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated && Request.QueryString["ReturnUrl"] != null)
                {
                    Response.Redirect("~/Views/notauthorized.aspx");
                }
                System.Web.Security.FormsAuthentication.SignOut();
                Session["tender"] = null;
                Session["item"] = null;
                Session["fullname"] = null;
                IIdentity id = User.Identity;

            }

           
            //if (id.IsAuthenticated)
            //{
                
            //    //else if (Request.QueryString["return"] != null)
            //    //{
            //    //    string redirect = Request.QueryString["return"];
            //    //    Response.Redirect("~/" + redirect);
            //    //}
            //    else
            //    {
            //        Response.Redirect("~/Default.aspx");
            //    }
            //}
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {

            //FormsAuthentication.SetAuthCookie(User.Identity.Name.ToLower(), true);
        }
    }
}