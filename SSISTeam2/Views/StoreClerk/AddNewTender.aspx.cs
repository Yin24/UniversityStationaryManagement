using SSISTeam2.Classes.EFFacades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class AddNewTender : System.Web.UI.Page
    {
        SSISEntities entities = new SSISEntities();
        String itemCode; string selectStartDate = null; string currentDate, supplierID;
        Boolean flag=false;String chkflag = "F"; DateTime tenderDate;int resultCount;int tenderYearId;

        bool tenderListFlag;int tenderListDetailCount;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                DropDownList1.DataSource = entities.Suppliers.OrderBy(u => u.supplier_id).Select(u => u.name).ToList();
                DropDownList1.DataBind();

                var query = (from t1 in entities.Stock_Inventory
                             orderby t1.item_description
                             select new { t1.item_description, t1.item_code }).ToList();

                DropDownList2.DataSource = query;
                DropDownList2.DataTextField = "item_description";
                DropDownList2.DataBind();     

            }

        }
        protected void InsertNewTender_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                decimal price; int rank;

                String supplierName = DropDownList1.SelectedItem.ToString();
                String itemDescription = DropDownList2.SelectedItem.ToString();
                DateTime tenderDate = DateTime.Parse(TextBox1.Text);
                bool isDouble = Decimal.TryParse(TextBox2.Text, out price);

                String currentDate = DateTime.Now.ToString("yyyy-MM-dd");

                if (supplierName.Equals("Select---"))
                {
                    lblerror2.Text = "PLeae choose Supplier Name!";
                   

                }
                else if (itemDescription.Equals("Select---"))
                {
                    lblerror2.Text = " ";
                    lblerror3.Text = "PLeae choose one item description!";

                }
                else if ((TextBox1.Text).CompareTo(currentDate) == -1)
                {
                    lblerror2.Text = " ";
                    lblerror3.Text = " ";
                    lblerror1.Text = "Cannot choose previous date!";

                }
                else
                {
                    lblerror1.Text = "";
                    lblerror2.Text = "";
                    lblerror3.Text = "";
                    Label1.Text = "";

                    if ((!supplierName.Equals("Select---")) && (!itemDescription.Equals("Select---")))
                    {
                        //For getting supplier Id from supplier name
                        var result1 = entities.Suppliers.Where(x => x.name == supplierName).Select(x => x.supplier_id);
                        supplierID = result1.First();

                        //For getting item code from item description
                        var result2 = entities.Stock_Inventory.Where(x => x.item_description == itemDescription).Select(x => x.item_code);
                        itemCode = result2.First();

                        using (SSISEntities entities = new SSISEntities())
                        {
                            //For checking tender year
                            string[] words = tenderDate.ToString().Split('/');
                            String tyear = words[2];
                            String ctyear = tyear.Substring(0, 4);

                            bool tenderListFlag = checkTenderList(ctyear);
                            int tenderListDetailCount = checkTenderListDetail(ctyear);

                            if (tenderListFlag == false)
                            {
                                if (tenderListDetailCount == 0)
                                {
                                    Tender_List tender = new Tender_List
                                    {
                                        supplier_id = supplierID,
                                        tender_date = tenderDate,
                                        deleted = "N"
                                    };
                                    Tender_List_Details tenderDetail = new Tender_List_Details
                                    {
                                        tender_year_id = tender.tender_year_id,
                                        price = price,
                                        rank = 0,
                                        item_code = itemCode,
                                        deleted = "N"
                                    };
                                    entities.Tender_List.Add(tender);
                                    entities.Tender_List_Details.Add(tenderDetail);
                                    entities.SaveChanges();
                                }
                                else
                                {
                                    chkflag = "T";

                                }
                            }
                            else
                            {
                                if (tenderListDetailCount == 0)
                                {
                                    var query = entities.Tender_List.Where(x => x.tender_date.Year == DateTime.Today.Year &&
                                    x.supplier_id == supplierID && x.deleted == "N").Select(x => x.tender_year_id).Distinct();
                                    tenderYearId = query.First();

                                    Tender_List_Details tenderDetail = new Tender_List_Details
                                    {
                                        tender_year_id = tenderYearId,
                                        price = price,
                                        rank = 0,
                                        item_code = itemCode,
                                        deleted = "N"
                                    };

                                    entities.Tender_List_Details.Add(tenderDetail);
                                    entities.SaveChanges();
                                }
                                else
                                {
                                    chkflag = "T";

                                }
                            }

                            if (chkflag == "T")
                            {
                                Label1.Text = "Your Data is already exist.Please choose another one!";
                            }
                            else
                            {
                                Response.Redirect("TenderListForm.aspx");
                            }


                        }
                    }
                        }
                    }
                }
           // }
       // }

        //Check tender List
        public bool checkTenderList(string year)
        {
            flag = false;
            try
            {
                var list = entities.Tender_List.Where(x => x.supplier_id == supplierID && x.deleted == "N").Select(x => x.tender_date.Year).Distinct().ToList();

                for (int j = 0; j < list.Count(); j++)
                {
                    int value = list[j];
                    if (value == Convert.ToInt32(year))
                    {
                        flag = true;
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
            
        }

        public int checkTenderListDetail(string year)
        {
            int cyear = Convert.ToInt32(year);
            resultCount = 0;
            try
            {
                var result = (from t1 in entities.Tender_List
                              join t2 in entities.Tender_List_Details
                              on t1.tender_year_id equals t2.tender_year_id
                              where t1.supplier_id.Equals(supplierID)
                              && t2.item_code.Equals(itemCode)
                              && t1.deleted.Equals("N")
                              && t2.deleted.Equals("N")
                              && t1.tender_date.Year == cyear
                              select new { t1.tender_date.Year }).Distinct().Count();
                resultCount = result;
            }
            catch
            {
                resultCount = 0;
            }
        
            return resultCount;

        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TenderListForm.aspx");
        }
    }
}