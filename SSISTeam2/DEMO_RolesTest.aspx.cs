using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2
{
    public partial class DEMO_RolesTest : System.Web.UI.Page
    {
        public UserModel userModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            userModel = new UserModel(Page.User.Identity.Name);
        }
    }
}