using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class AddNewSupplier : System.Web.UI.Page
    {

        SSISEntities entities = new SSISEntities();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

       

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string SupplierId = tb_supplierId.Text;
            string SupplierName = tb_supplierName.Text;
            string ContactName = tb_contactName.Text;
            string ContactNum = tb_contactNum.Text;
            string FaxNum = tb_faxNum.Text;
            string Address = tb_address.Text;
            string Gst = tb_gst.Text;

            using (SSISEntities entities = new SSISEntities())
            {

                Supplier s = new Supplier();

                s.supplier_id = SupplierId;
                s.name = SupplierName;
                s.contact_name = ContactName;
                s.contact_num = ContactNum;
                s.fax_num = FaxNum;
                s.address = Address;
                s.gst_reg_num = Gst;
                s.deleted = "N";

                entities.Suppliers.Add(s);
                entities.SaveChanges();
            }

            Response.Redirect("~/Views/StoreClerk/ViewSupplierList.aspx");

        }
       
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewSupplierList.aspx");
        }
    }
}