using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.DepartmentHead
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            string date = "8/1/2017 12:55:00 AM";

            DateTime myDate = DateTime.Parse(date);

            string cDate = myDate.ToString("dd/MM/yyyy");
            Label1.Text=cDate;

           // myDate.ToString("M/d/yyyy HH:mm:ss tt");
            //Label1.Text = myDate.ToString("M/d/yyyy HH:mm:ss tt");

            //Label1.Text = DateTime.ParseExact(myDate.ToString("M/d/yyyy HH:mm:ss tt"), "dd/mm/yyyy", //null).ToString();
            // Date date = input.parse(startdate);
        }
    }
}