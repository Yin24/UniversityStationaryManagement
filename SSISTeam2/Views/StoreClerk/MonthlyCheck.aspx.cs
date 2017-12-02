using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using SSISTeam2.Classes.Models;

namespace SSISTeam2
{
    public partial class MonthlyCheck : System.Web.UI.Page
    {
        DateTime today;
        SSISEntities context;
        bool checkDone = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            context = new SSISEntities();

            if (!IsPostBack)
            {
                checkDone = CheckIfMonthlyDone();
                List<MonthlyCheckModel> itemList = (List<MonthlyCheckModel>) Session["Monthly"];

                if (itemList == null)
                {
                    itemList = new List<MonthlyCheckModel>();

                    List<Stock_Inventory> stockList = context.Stock_Inventory.Where(x => x.deleted == "N").ToList();
                    foreach (Stock_Inventory i in stockList)
                    {
                        MonthlyCheckModel item = new MonthlyCheckModel(i);
                        itemList.Add(item);
                    }

                    Session["Monthly"] = itemList;
                }


                MonthlyCheckGV.DataSource = itemList;
                MonthlyCheckGV.DataBind();

                UserModel user = new UserModel(HttpContext.Current.User.Identity.Name);
                UserModel depthead = user.FindDelegateOrDeptHead();
                //CheckLabel.Text = depthead.Username;


                //today = DateTime.Today;
                //DateTB.Text = today.Date.ToString("dd/MM/yyyy");

                itemList = (List<MonthlyCheckModel>)Session["Monthly"];
                //Session["Monthly"] = itemList;
                MonthlyCheckGV.DataSource = itemList;
                MonthlyCheckGV.DataBind();
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Retaining initial quantity values
            int initial = int.Parse(MonthlyCheckGV.Rows[e.NewEditIndex].Cells[1].Text);
            Session["discrepency"] = initial;
        }

        protected void nextBtn_Click(object sender, EventArgs e)
        {
            List<MonthlyCheckModel> itemList = (List<MonthlyCheckModel>)Session["Monthly"];
            List<MonthlyCheckModel> confirmList = new List<MonthlyCheckModel>();
            foreach (MonthlyCheckModel i in itemList)
            {
                if (i.ActualQuantity != i.CurrentQuantity)
                {
                    confirmList.Add(i);
                }
            }
            Session["Confirmation"] = confirmList;
            Response.Redirect("MonthlyCheckConfirmation.aspx");
        }
        //protected void MonthlyCheckGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    List<MonthlyCheckModel> itemList = (List<MonthlyCheckModel>)Session["Monthly"];
        //    MonthlyCheckGV.PageIndex = e.NewPageIndex;
        //    MonthlyCheckGV.DataSource = itemList;
        //    MonthlyCheckGV.DataBind();
        //}
        protected void MonthlyCheckGV_OnTextChange(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = (GridViewRow)(sender as Control).Parent.Parent;
            TextBox textbox = (TextBox) sender;
            Label rowIndex = (Label)gridViewRow.FindControl("rowIndex");
            int index = int.Parse(rowIndex.Text) - 1;
            List<MonthlyCheckModel> model = (List<MonthlyCheckModel>) Session["Monthly"];
            model[index].ActualQuantity = int.Parse(textbox.Text);

            Session["Monthly"] = model;
            MonthlyCheckGV.DataSource = model;
            MonthlyCheckGV.DataBind();
        }
        public bool CheckRepeatAdjustment(Adjustment_Details detail, List<Adjustment_Details> adjDetails)
        {
            List<Adjustment_Details> adjDetail = (List<Adjustment_Details>)Session["Adjustment"];
            foreach (Adjustment_Details i in adjDetails)
            {
                if (i.item_code == detail.item_code)
                {
                    i.quantity_adjusted += detail.quantity_adjusted;
                    return true;
                }
            }
            return false;
        }
        public bool CheckIfMonthlyDone()
        {
            List<Monthly_Check_Records> recordList = context.Monthly_Check_Records.Where(x => x.deleted == "N").ToList();

            DateTime recordDate = recordList.Max(x => x.date_checked);
            int recordMonth = recordDate.Month;
            int todayMonth = DateTime.Today.Month;
            if (recordMonth == todayMonth)
            {
                //MonthlyCheckGV.Enabled = false;
                //nextBtn.Enabled = false;
                CheckLabel.Text = "Monthly check has already been done this month";
                return true;
            }
            return false;
        }

        protected void MonthlyCheckGV_DataBinding(object sender, EventArgs e)
        {
            //if (checkDone)
            //{
            //    MonthlyCheckGV.Columns[4].Visible = false;
            //}
        }


        // for paganation

        protected void GridView_EditBooks_DataBound(object sender, EventArgs e)
        {
            GridViewRow topPagerRow = MonthlyCheckGV.TopPagerRow;
            GridViewRow bottomPagerRow = MonthlyCheckGV.BottomPagerRow;

            DropDownList topJumpToPage = (DropDownList)topPagerRow.FindControl("DropDownList_JumpToPage");
            DropDownList bottomJumpToPage = (DropDownList)bottomPagerRow.FindControl("DropDownList_JumpToPage");

            if (topJumpToPage != null)
            {
                for (int i = 0; i < MonthlyCheckGV.PageCount; i++)
                {
                    ListItem item = new ListItem("Page " + (i + 1));
                    topJumpToPage.Items.Add(item);
                    bottomJumpToPage.Items.Add(item);
                }
            }

            topJumpToPage.SelectedIndex = MonthlyCheckGV.PageIndex;
            bottomJumpToPage.SelectedIndex = MonthlyCheckGV.PageIndex;
        }


        protected void DropDownList_JumpToPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow topPagerRow = MonthlyCheckGV.TopPagerRow;
            GridViewRow bottomPagerRow = MonthlyCheckGV.BottomPagerRow;

            DropDownList topJumpToPage = (DropDownList)topPagerRow.FindControl("DropDownList_JumpToPage");
            DropDownList bottomJumpToPage = (DropDownList)bottomPagerRow.FindControl("DropDownList_JumpToPage");

            if ((DropDownList)sender == bottomJumpToPage)
            {
                MonthlyCheckGV.PageIndex = bottomJumpToPage.SelectedIndex;
            }
            else
            {
                MonthlyCheckGV.PageIndex = topJumpToPage.SelectedIndex;

            }

            MonthlyCheckGV.DataSource = (List<MonthlyCheckModel>)Session["Monthly"]; ;

            MonthlyCheckGV.DataBind();

        }

        // For paganation

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                MonthlyCheckGV.PageIndex = 0;
            }
            else
            {
                MonthlyCheckGV.PageIndex = e.NewPageIndex;
            }

            MonthlyCheckGV.DataSource= (List<MonthlyCheckModel>)Session["Monthly"]; ;

            MonthlyCheckGV.DataBind();
        }

    }
}