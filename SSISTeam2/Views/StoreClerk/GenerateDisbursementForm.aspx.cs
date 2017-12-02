using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class GenerateDisbursementForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SSISEntities entities = new SSISEntities();
            if (!IsPostBack)
            {
                DropDownList1.DataSource = entities.Departments.OrderBy(u => u.name).Select(u => u.name).ToList();
                DropDownList1.DataBind();
                DropDownList2.DataSource = entities.Collection_Point.OrderBy(c => c.location).Select(c => c.location).ToList();
                DropDownList2.DataBind();
            }




        }
    }
}