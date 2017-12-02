using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class EditSupplier : System.Web.UI.Page
    {
        SSISEntities context = new SSISEntities();
        string supplier_code;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string supplierId = Request.QueryString["supplier"];
                Supplier s = context.Suppliers.Where(x => x.supplier_id == supplierId).First();
                supplier_code = s.supplier_id;
                tb_supplierId.Text = s.supplier_id;
                tb_contactName.Text = s.contact_name;
                tb_contactNum.Text = s.contact_num;
                tb_faxNum.Text = s.fax_num;
                tb_gst.Text = s.gst_reg_num;
                tb_supplierName.Text = s.name;

                tb_address.Text = s.address;
            }
            

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            
            string SupplierName = tb_supplierName.Text;
            string ContactName = tb_contactName.Text;
            string ContactNum = tb_contactNum.Text;
            string FaxNum = tb_faxNum.Text;
            string Address = tb_address.Text;
            string Gst = tb_gst.Text;
            supplier_code=Request.QueryString["supplier"];

            Supplier s = context.Suppliers.Where(x=> x.supplier_id ==supplier_code).First();

               
                s.name = SupplierName;
                s.contact_name = ContactName;
                s.contact_num = ContactNum;
                s.fax_num = FaxNum;
                s.address = Address;
                s.gst_reg_num = Gst;


                context.SaveChanges();



                Response.Redirect("~/Views/StoreClerk/viewsupplierList.aspx");


            }

        protected void SubmitButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/viewsupplierList.aspx");
        }
    }

    }
