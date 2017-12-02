using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace SSISTeam2.Views.Reporting.Reports
{
    public partial class RequestVolumeRpt : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportDocument crVOL = new ReportDocument();
                crVOL.Load(Server.MapPath("~/Views/Reporting/Reports/Volume_Report.rpt"));
                VOLCrystal.ReportSource = crVOL;
                Session["ReportDocument"] = crVOL;
            }
            else
            {
                ReportDocument doc = (ReportDocument)Session["ReportDocument"];
                VOLCrystal.ReportSource = doc;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void genNewRep_OnClick(object Sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void backBtn_OnClick(object Sender, EventArgs e)
        {
            Response.Redirect("ReportsMain.aspx");
        }
    }
}