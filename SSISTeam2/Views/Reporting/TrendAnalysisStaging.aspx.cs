using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity;
using System.Data.Sql;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace SSISTeam2.Views.Reporting.Reports
{
   
    public partial class TrendAnalysisStaging : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportDocument crTA = new ReportDocument();
                crTA.Load(Server.MapPath("~/Views/Reporting/Reports/Trend_Analysis.rpt"));
                TACrystal.ReportSource = crTA;
                Session["ReportDocument"] = crTA;
            }
            else
            {
                ReportDocument doc = (ReportDocument)Session["ReportDocument"];
                TACrystal.ReportSource = doc;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //CrystalReport
           // ReportDocument crTA = new ReportDocument();
            //crTA.Load(Server.MapPath("~/Views/Reporting/Reports/Trend_Analysis.rpt"));
            //CrystalReportViewer1.ReportSource = crTA;
            //CrystalReportViewer1.RefreshReport();






            //Populate Year DDL
            //string currentYear = DateTime.Now.Year.ToString();
            //int year=Convert.ToInt32(currentYear);
            //for (int i = year; i >= 1980; i--)
            //                    {
            //    Year1.Items.Add(i.ToString());
            //    Year2.Items.Add(i.ToString());
            //    Year3.Items.Add(i.ToString());
            }


        //Populate Month DDL
        //DateTime month = Convert.ToDateTime("1/1/2000");
        //                for (int i = 0; i <= 11; i++)
        //                    {
        //    DateTime NextMont = month.AddMonths(i);
        //    ListItem list = new ListItem();
        //    list.Text = NextMont.ToString("MMMM");
        //    list.Value = NextMont.Month.ToString();
        //    Month1.Items.Add(list);
        //    Month2.Items.Add(list);
        //    Month3.Items.Add(list);
        //}


        //Commence SQL Connection

        //SqlConnection conn = new SqlConnection("data source=(local);initial catalog=SSIS;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
        //    conn.Open();
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter adapter = new SqlDataAdapter("Select cat_name from Category", conn);
        //    adapter.Fill(ds, "cat_name");
        //if (!IsPostBack)
        //{
        //    CatList.DataSource = ds.Tables[0];
        //    CatList.DataTextField = "cat_name";
        //    CatList.DataValueField = "cat_name";

        //    CatList.DataBind();

        //    }
        //}

        //METHOD FOR MANIPULATION OF ITEMS BETWEEN LIST BOXES
        //private void MoveItems(bool isAdd)
        //{
        //    if (isAdd)// Items from CatList to Selector
        //    {
        //        for (int i = CatList.Items.Count - 1; i >= 0; i--)
        //        {
        //            if (CatList.Items[i].Selected)
        //            {
        //                SelectorList.Items.Add(CatList.Items[i]);
        //                SelectorList.ClearSelection();
        //                CatList.Items.Remove(CatList.Items[i]);
        //            }
        //        }
        //    }
        //    else // Item from Selector to CatList
        //    {
        //        for (int i = SelectorList.Items.Count - 1; i >= 0; i--)
        //        {
        //            if (SelectorList.Items[i].Selected)
        //            {
        //                CatList.Items.Add(SelectorList.Items[i]);
        //                CatList.ClearSelection();
        //                SelectorList.Items.Remove(SelectorList.Items[i]);
        //            }
        //        }
        //    }
        //}

        //private void MoveAllItems(bool isAddAll)
        //{
        //    if (isAddAll)// All from CatList to Selector
        //    {
        //        for (int i = CatList.Items.Count - 1; i >= 0; i--)
        //        {
        //            SelectorList.Items.Add(CatList.Items[i]);
        //            SelectorList.ClearSelection();
        //            CatList.Items.Remove(CatList.Items[i]);
        //        }
        //    }
        //    else // All from Selector to CatList
        //    {
        //        for (int i = SelectorList.Items.Count - 1; i >= 0; i--)
        //        {
        //            CatList.Items.Add(SelectorList.Items[i]);
        //            CatList.ClearSelection();
        //            SelectorList.Items.Remove(SelectorList.Items[i]);
        //        }
        //    }
        //}

        //protected void AddOneCat_Click(object sender, EventArgs e)
        //{
        //    MoveItems(true);

        //}

        //protected void AddAllCat_Click(object sender, EventArgs e)
        //{
        //    MoveAllItems(true);
        //}

        //protected void RemoveOneCat_Click(object sender, EventArgs e)
        //{
        //    MoveItems(false);
        //}

        //protected void RemoveAllCat_Click(object sender, EventArgs e)
        //{
        //    MoveItems(false);
        //}

        //protected void SetMonthYear_OnClick(object sender, EventArgs e)
        //{


        /*
                    // Create parameter objects
                    ParameterFields CatParams = new ParameterFields();
                    ParameterField CatParam = new ParameterField();
                    ParameterDiscreteValue Catdv = new ParameterDiscreteValue();

                    // Set the ParameterFieldName to the name of the parameter
                    // created in the Field Explorer
                    CatParam.ParameterFieldName = "cat_name";

                    // Add cats
                    for (int i = SelectorList.Items.Count - 1; i >= 0; i--)
                    {
                        Catdv.Value = Convert.ToString(CatList.Items[i]);
                        CatParam.CurrentValues.Add(Catdv);

                        // Clear and reuse cat dv at end of loop
                        Catdv = new ParameterDiscreteValue();
                    }

        // Add CatParam object to collection
        CatParams.Add(CatParam);

                    // Assign to report viewer
                    //CrystalReportViewer1.ParameterFieldInfo = CatParams;*/






        //}

        //protected void cValidator_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    args.IsValid = SelectorList.Items.Count > 0;
        //}
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