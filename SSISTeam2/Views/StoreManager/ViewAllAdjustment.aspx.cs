using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SSISTeam2.Classes.Models;
namespace SSISTeam2
{
    public partial class ViewAllAdjustment : System.Web.UI.Page
    {
        SSISEntities context;
        //InventoryAdjustmentModel model;
        protected void Page_Load(object sender, EventArgs e)
        {
            context = new SSISEntities();

            if (!IsPostBack)
            {
                List<Inventory_Adjustment> invAdjList = context.Inventory_Adjustment.Where(x => x.deleted == "N" & x.status == "Pending").ToList();
                List<InventoryAdjustmentModel> invModelList = new List<InventoryAdjustmentModel>();
                foreach (Inventory_Adjustment i in invAdjList)
                {
                    InventoryAdjustmentModel model = new InventoryAdjustmentModel(i);
                    //model = new InventoryAdjustmentModel(i);
                    if (User.IsInRole("Manager"))
                    {
                        foreach (AdjustmentModel j in model.AdjModel)
                        {
                            if (j.Above250())
                            {
                                invModelList.Add(model);
                                break;
                            }
                        }
                    }
                    else if (User.IsInRole("Supervisor"))
                    {
                        foreach (AdjustmentModel j in model.AdjModel)
                        {
                            if (!j.Above250())
                            {
                                invModelList.Add(model);
                                break;
                            }
                        }
                    }
                }

                Session["ViewAdj"] = invModelList;
                ViewAdjustmentGV.DataSource = invModelList;
                ViewAdjustmentGV.DataBind();

                //invModelList.Add(model);
                //GridView1.DataSource = invModelList;
                //GridView1.DataBind();
            }
            //List<Adjustment_Details> adjList = context.Adjustment_Details.ToList();
            //List<AdjustmentModel> detailList = new List<AdjustmentModel>();
            //foreach (Adjustment_Details i in adjList)
            //{
            //    AdjustmentModel model = new AdjustmentModel(i);
            //    detailList.Add(model);
            //}
            //Session["ViewAdj"] = detailList;
        }

        protected void detailBtn_Click(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label rowIndex = (Label) gridViewRow.FindControl("rowIndex");
            int index = int.Parse(rowIndex.Text) - 1;
            
            List<InventoryAdjustmentModel> invModelList = (List<InventoryAdjustmentModel>)Session["ViewAdj"];
            
            InventoryAdjustmentModel model = invModelList[index];

            
            Session["ConfirmAdj"] = model;
            Response.Redirect("AdjustmentDetail.aspx");
        }

        public List<AdjustmentModel> CheckForRole(List<AdjustmentModel> adjList)
        {
            List<AdjustmentModel> listManOrSup = new List<AdjustmentModel>();

            return listManOrSup;
        }

        protected void ViewAdjustmentGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<InventoryAdjustmentModel> itemList = (List<InventoryAdjustmentModel>)Session["ViewAdj"];
            ViewAdjustmentGV.PageIndex = e.NewPageIndex;
            ViewAdjustmentGV.DataSource = itemList;
            ViewAdjustmentGV.DataBind();
        }
    }
}