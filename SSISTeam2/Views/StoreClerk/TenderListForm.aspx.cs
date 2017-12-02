using SSISTeam2.Classes.EFFacades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class TenderListForm : System.Web.UI.Page
    {
        SSISEntities entities = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindGrid();
                String searchWord = TextBox1.Text;
                var result = entities.Suppliers.Where(x => x.name.Contains(searchWord)).Select(x => x.name).ToList();

            }
            List<Supplier> supList = entities.Suppliers.ToList();

        }

        //private void BindGrid()
        //{
        //    string searchWord = TextBox1.Text;
        //    if (!string.IsNullOrEmpty(searchWord)){
        //        decimal changePrice;
        //        bool isDouble = Decimal.TryParse(searchWord, out changePrice);
        //        var result = entities.Suppliers.Where(x => x.name.Contains(searchWord)).Select(x => x.supplier_id).ToList();
        //        var result2 = entities.Stock_Inventory.Where(x => x.item_description.Contains(searchWord)).Select(x => x.item_code).ToList();
        //        var result3 = from t1 in entities.Tender_List
        //                      join t2 in entities.Tender_List_Details
        //                      on t1.tender_year_id equals t2.tender_year_id
        //                      join t3 in entities.Suppliers
        //                      on t1.supplier_id equals t3.supplier_id
        //                      join t4 in entities.Stock_Inventory
        //                      on t2.item_code equals t4.item_code
        //                      where t2.deleted.Equals("Y")
        //                      && t1.deleted.Equals("N")
        //                      && t3.deleted.Equals("N")
        //                      && t4.deleted.Equals("N")
        //                      //&& (result.Contains(t1.supplier_id))
        //                      //|| (result2.Contains(t2.item_code))
        //                       && ((result.Contains(t1.supplier_id))
        //                       || (result2.Contains(t2.item_code)))
        //                      //|| (result4.Contains(t2.price))
        //                      orderby t3.name
        //                      select new { t2.tender_id, t1.tender_year_id, t3.supplier_id, t3.name, t2.item_code, t4.item_description, t2.price, t1.tender_date };
        //        GridView1.Columns[1].Visible = false;
        //        GridView1.Columns[2].Visible = false;
        //        GridView1.Columns[3].Visible = false;
        //        GridView1.Columns[4].Visible = false;
        //        GridView1.DataSource = result3.ToList();              
        //    }
        //    else
        //    {
        //        var result = from t1 in entities.Tender_List
        //                     join t2 in entities.Tender_List_Details
        //                     on t1.tender_year_id equals t2.tender_year_id
        //                     join t3 in entities.Suppliers
        //                     on t1.supplier_id equals t3.supplier_id
        //                     join t4 in entities.Stock_Inventory
        //                     on t2.item_code equals t4.item_code
        //                     where t2.deleted.Equals("N")
        //                     && t3.deleted.Equals("N")
        //                     && t4.deleted.Equals("N")
        //                     orderby t3.name
        //                     select new { t2.tender_id, t1.tender_year_id, t3.supplier_id, t3.name, t2.item_code, t4.item_description, t2.price, t1.tender_date };

        //        GridView1.Columns[1].Visible = false;
        //        GridView1.Columns[2].Visible = false;
        //        GridView1.Columns[3].Visible = false;
        //        GridView1.Columns[4].Visible = false;
        //        GridView1.DataSource = result.ToList();
        //    }

        //    GridView1.DataBind();
        ////}
        private void BindGrid()
        {
            string searchWord = TextBox1.Text;
            if (!string.IsNullOrEmpty(searchWord))
            {
                decimal changePrice;
                bool isDouble = Decimal.TryParse(searchWord, out changePrice);
                var result = entities.Suppliers.Where(x => x.name.Contains(searchWord)).Select(x => x.supplier_id).ToList();
                var result2 = entities.Stock_Inventory.Where(x => x.item_description.Contains(searchWord)).Select(x => x.item_code).ToList();
                var result3 = (from t1 in entities.Tender_List
                               join t2 in entities.Tender_List_Details
                               on t1.tender_year_id equals t2.tender_year_id
                               join t3 in entities.Suppliers
                               on t1.supplier_id equals t3.supplier_id
                               join t4 in entities.Stock_Inventory
                               on t2.item_code equals t4.item_code
                               where t2.deleted.Equals("N")
                               && t3.deleted.Equals("N")
                               && t4.deleted.Equals("N")
                                //&& result2.Contains(t2.item_code)
                                && ((result.Contains(t1.supplier_id))
                                || (result2.Contains(t2.item_code)))
                               //|| (result4.Contains(t2.price))
                               orderby t3.name
                               select new { t2.tender_id, t1.tender_year_id, t3.supplier_id, t3.name, t2.item_code, t4.item_description, t2.price, t1.tender_date }).ToList();
                GridView1.Columns[1].Visible = false;
                GridView1.Columns[2].Visible = false;
                GridView1.Columns[3].Visible = false;
                GridView1.Columns[4].Visible = false;
                GridView1.DataSource = result3.ToList();

                //if (result3 != null && result3.Count > 0)
                //{
                //    lblNoData.Visible = false;
                //    GridView1.DataSource = result3.ToList(); 

                //}
                //else
                //{
                //    lblNoData.Visible = true;
                //}

            }
            else
            {
                var result = (from t1 in entities.Tender_List
                              join t2 in entities.Tender_List_Details
                              on t1.tender_year_id equals t2.tender_year_id
                              join t3 in entities.Suppliers
                              on t1.supplier_id equals t3.supplier_id
                              join t4 in entities.Stock_Inventory
                              on t2.item_code equals t4.item_code
                              where t2.deleted.Equals("N")
                              && t3.deleted.Equals("N")
                              && t4.deleted.Equals("N")
                              orderby t3.name
                              select new { t2.tender_id, t1.tender_year_id, t3.supplier_id, t3.name, t2.item_code, t4.item_description, t2.price, t1.tender_date }).ToList();

                GridView1.Columns[1].Visible = false;
                GridView1.Columns[2].Visible = false;
                GridView1.Columns[3].Visible = false;
                GridView1.Columns[4].Visible = false;
                GridView1.DataSource = result.ToList();
                //if (result != null && result.Count > 0)
                //{
                //    lblNoData.Visible = false;
                //    GridView1.DataSource = result.ToList();

                //}
                //else
                //{
                //    lblNoData.Visible = true;
                //}


            }

            GridView1.DataBind();
        }
        protected void AddNewTender_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddNewTender.aspx");
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            this.BindGrid();
        }
        //protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    GridViewRow row = GridView1.Rows[e.RowIndex];

        //    string supplierId = (row.FindControl("DropDownList1") as DropDownList).SelectedValue;
        //    //string supplierId = (row.FindControl("Label9") as Label).Text;
        //    //string supplierName = dropDowns[0].SelectedValue.
        //    string supplierName = (row.FindControl("DropDownList1") as DropDownList).SelectedItem.Text;
        //    //Label9.Text = supplierId;
        //    //Label1.Text = supplierName;
        //    //string supplierName= (row.FindControl("TextBox1") as TextBox).Text;
        //    string itemCode = (row.FindControl("Label8") as Label).Text;
        //    string itemDescription = (row.FindControl("TextBox3") as TextBox).Text;
        //    int tenderId = Convert.ToInt32((row.FindControl("Label10") as Label).Text);
        //    decimal price = Convert.ToDecimal((row.FindControl("TextBox5") as TextBox).Text);
        //    int tenderYearId = Convert.ToInt32((row.FindControl("Label11") as Label).Text);
        //    DateTime tenderDate = Convert.ToDateTime((row.FindControl("Label6") as Label).Text);

        //    //string[] dropDownNames = { "DropDownList1" };
        //    //List<DropDownList> dropDowns = new List<DropDownList>();
        //    //dropDownNames.ToList().ForEach(name =>
        //    //{
        //    //    dropDowns.Add((DropDownList)row.FindControl(name));
        //    //});
        //    //string supplierName = dropDowns[0].SelectedValue;

        //    //using (SSISEntities s = new SSISEntities())
        //    //{
        //    //    Supplier supplierToEdit = s.Suppliers.Find(supplierId);

        //    //   supplierToEdit.
        //    //}

        //    var tender = entities.Tender_List_Details.SingleOrDefault(x => x.tender_id == tenderId);

        //    // find tender_year_id for the latest one, with the supplier_id

        //    var tenderYearItem = entities.Tender_List.Where(x => x.supplier_id == supplierId).OrderByDescending(o => o.tender_date);

        //    if (tenderYearItem.Count() > 0)
        //    {
        //        tender.tender_year_id = tenderYearItem.First().tender_year_id;
        //    }

        //    tender.price = price;

        //    //var result1 = entities.Suppliers.SingleOrDefault(x => x.supplier_id == supplierId);
        //    //result1.supplier_id = supplierId;
        //    //entities.SaveChanges();

        //    var result2 = entities.Stock_Inventory.SingleOrDefault(x => x.item_code == itemCode);
        //    result2.item_description = itemDescription;
        //    entities.SaveChanges();

        //    //var result3 = entities.Tender_List_Details.SingleOrDefault(x => x.tender_id == tenderId);
        //    //result3.price = price;
        //    //entities.SaveChanges();

        //    var result4 = entities.Tender_List.SingleOrDefault(x => x.tender_year_id == tenderYearId);
        //    result4.tender_date = tenderDate;
        //    entities.SaveChanges();

        //    FacadeFactory.GetTenderListService().RedoRankingsForTenderList();

        //    GridView1.EditIndex = -1;
        //    this.BindGrid();

        //}

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];

            // supplierId = (row.FindControl("DropDownList1") as DropDownList).SelectedValue;
            //string supplierId = (row.FindControl("Label9") as Label).Text;
            //string supplierName = dropDowns[0].SelectedValue.
           // string supplierName = (row.FindControl("DropDownList1") as DropDownList).SelectedItem.Text;
            //Label9.Text = supplierId;
            //Label1.Text = supplierName;
            //string supplierName= (row.FindControl("TextBox1") as TextBox).Text;
            string itemCode = (row.FindControl("Label8") as Label).Text;
            //string supplierId = (row.FindControl("Labe9") as Label).Text;
            //string supplierName = (row.FindControl("Labe20") as Label).Text;
            string itemDescription = (row.FindControl("TextBox3") as TextBox).Text;
            
            int tenderId = Convert.ToInt32((row.FindControl("Label10") as Label).Text);
                  
            decimal price = Convert.ToDecimal((row.FindControl("TextBox5") as TextBox).Text);

            int tenderYearId = Convert.ToInt32((row.FindControl("Label11") as Label).Text);
            DateTime tenderDate = Convert.ToDateTime((row.FindControl("Label6") as Label).Text);

            //string[] dropDownNames = { "DropDownList1" };
            //List<DropDownList> dropDowns = new List<DropDownList>();
            //dropDownNames.ToList().ForEach(name =>
            //{
            //    dropDowns.Add((DropDownList)row.FindControl(name));
            //});
            //string supplierName = dropDowns[0].SelectedValue;

            //using (SSISEntities s = new SSISEntities())
            //{
            //    Supplier supplierToEdit = s.Suppliers.Find(supplierId);

            //   supplierToEdit.
            //}

            var tender = entities.Tender_List_Details.SingleOrDefault(x => x.tender_id == tenderId);

            //// find tender_year_id for the latest one, with the supplier_id

            //var tenderYearItem = entities.Tender_List.Where(x => x.supplier_id == supplierId).OrderByDescending(o => o.tender_date);

            //if (tenderYearItem.Count() > 0)
            //{
            //    tender.tender_year_id = tenderYearItem.First().tender_year_id;
            //}

            tender.price = price;

            //var result1 = entities.Suppliers.SingleOrDefault(x => x.supplier_id == supplierId);
            //result1.supplier_id = supplierId;
            //entities.SaveChanges();

            var result2 = entities.Stock_Inventory.SingleOrDefault(x => x.item_code == itemCode);
            result2.item_description = itemDescription;
            entities.SaveChanges();

            //var result3 = entities.Tender_List_Details.SingleOrDefault(x => x.tender_id == tenderId);
            //result3.price = price;
            //entities.SaveChanges();

            //var result4 = entities.Tender_List.SingleOrDefault(x => x.tender_year_id == tenderYearId);
            //result4.tender_date = tenderDate;
            //entities.SaveChanges();

            FacadeFactory.GetTenderListService().RedoRankingsForTenderList();

            GridView1.EditIndex = -1;
            this.BindGrid();

        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];

            //int tenderYearId = Convert.ToInt32((row.FindControl("Label11") as Label).Text);
            //var result1 = entities.Tender_List.SingleOrDefault(x => x.tender_year_id == tenderYearId);
            //result1.deleted = "Y";
            //entities.SaveChanges();

            int tenderId = Convert.ToInt32((row.FindControl("Label10") as Label).Text);
            var result2 = entities.Tender_List_Details.SingleOrDefault(x => x.tender_id == tenderId);
            result2.deleted = "Y";
            entities.SaveChanges();

            GridView1.EditIndex = -1;
            this.BindGrid();

        }

        protected void DropDownList_JumpToPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow topPagerRow = GridView1.TopPagerRow;
            GridViewRow bottomPagerRow = GridView1.BottomPagerRow;

            DropDownList topJumpToPage = (DropDownList)topPagerRow.FindControl("DropDownList_JumpToPage");
            DropDownList bottomJumpToPage = (DropDownList)bottomPagerRow.FindControl("DropDownList_JumpToPage");

            if ((DropDownList)sender == bottomJumpToPage)
            {
                GridView1.PageIndex = bottomJumpToPage.SelectedIndex;
            }
            else
            {
                GridView1.PageIndex = topJumpToPage.SelectedIndex;

            }
            this.BindGrid();
        }

        protected void GridView_EditBooks_DataBound(object sender, EventArgs e)
        {
            GridViewRow topPagerRow = GridView1.TopPagerRow;
            GridViewRow bottomPagerRow = GridView1.BottomPagerRow;

            try
            {
                DropDownList topJumpToPage = (DropDownList)topPagerRow.FindControl("DropDownList_JumpToPage");
                DropDownList bottomJumpToPage = (DropDownList)bottomPagerRow.FindControl("DropDownList_JumpToPage");

                if (topJumpToPage != null)
                {
                    for (int i = 0; i < GridView1.PageCount; i++)
                    {
                        ListItem item = new ListItem("Page " + (i + 1));
                        topJumpToPage.Items.Add(item);
                        bottomJumpToPage.Items.Add(item);
                    }
                }

                topJumpToPage.SelectedIndex = GridView1.PageIndex;
                bottomJumpToPage.SelectedIndex = GridView1.PageIndex;
            }
            catch (Exception ex)
            {

                ex.StackTrace.ToString();

            }

        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                GridView1.PageIndex = 0;
            }
            else
            {
                GridView1.PageIndex = e.NewPageIndex;
            }

            this.BindGrid();
        }
        //private void BindSearchGrid()
        //{
            
        //    string searchWord = TextBox1.Text;
        //    decimal changePrice;
        //    bool isDouble = Decimal.TryParse(searchWord, out changePrice);

        //    var result = entities.Suppliers.Where(x => x.name.Contains(searchWord)).Select(x => x.supplier_id).ToList();
        //    var result2 = entities.Stock_Inventory.Where(x => x.item_description.Contains(searchWord)).Select(x => x.item_code).ToList();
        //    //var result4 = entities.Tender_List_Details.Where(x => x.price == changePrice).Select(x => x.price).ToList();
        //    var result3 = from t1 in entities.Tender_List
        //                  join t2 in entities.Tender_List_Details
        //                  on t1.tender_year_id equals t2.tender_year_id
        //                  join t3 in entities.Suppliers
        //                  on t1.supplier_id equals t3.supplier_id
        //                  join t4 in entities.Stock_Inventory
        //                  on t2.item_code equals t4.item_code
        //                  where t2.deleted.Equals("N")
        //                  && t3.deleted.Equals("N")
        //                  && t4.deleted.Equals("N")
        //                  && (result.Contains(t1.supplier_id))
        //                  || (result2.Contains(t2.item_code))
        //                  //|| (result4.Contains(t2.price))
        //                  orderby t3.name
        //                  select new { t2.tender_id, t1.tender_year_id, t3.supplier_id, t3.name, t2.item_code, t4.item_description, t2.price, t1.tender_date };
        //    GridView1.Columns[1].Visible = false;
        //    GridView1.Columns[2].Visible = false;
        //    GridView1.Columns[3].Visible = false;
        //    GridView1.Columns[4].Visible = false;
        //    GridView1.DataSource = result3.ToList();
        //    GridView1.DataBind();
        //}

        
        protected void Search_Click(object sender, EventArgs e)
        {
            
            this.BindGrid();
        }
        public class DropDownListItem
        {
            string text;
            int value;
            public string Text { get { return text; } }
            public int Value { get { return value; } }
            public DropDownListItem(string text, int value)
            {
                this.text = text;
                this.value = value;
            }
        }

        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if ((e.Row.RowState & DataControlRowState.Edit) > 0)
        //        {
        //            GridViewRow row = e.Row;
        //            //(GridView1.Rows[e.NewEditIndex].FindControl("DropDownList1") as DropDownList).Visible = true;
        //            List<Supplier> supplierList = entities.Suppliers.ToList();
        //            List<string> supplierNameList = new List<string>();
        //            foreach (Supplier s in supplierList)
        //            {
        //                supplierNameList.Add(s.name);
        //            }

        //            DropDownList ddl = row.FindControl("DropDownList1") as DropDownList;
        //            ddl.DataSource = supplierList;
        //            ddl.DataTextField = "name";
        //            ddl.DataValueField = "supplier_id";
        //            ddl.DataBind();
        //        }
        //    }
        //}
    }

}