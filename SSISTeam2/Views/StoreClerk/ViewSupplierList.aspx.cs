using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class ViewSupplierList : System.Web.UI.Page
    {
        SSISEntities s = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            GridView1.DataSource = s.Suppliers.Where(x=>x.deleted != "Y").ToList<Supplier>();
            GridView1.DataBind();
        }


        protected void Add_New_Supplier_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddNewSupplier.aspx");
        }

        protected void Edit_Supplier(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            string supplierid = ((Label)gvr.FindControl("Label_SupplierId")).Text;

            Response.Redirect("~/Views/StoreClerk/EditSupplier.aspx?supplier="+supplierid);
        }

        protected void delete_Supplier(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            string supplierid = ((Label)gvr.FindControl("Label_SupplierId")).Text;

            Supplier supplier = s.Suppliers.Where(x => x.supplier_id == supplierid).First();

            supplier.deleted = "Y";

            s.SaveChanges();

            Response.Redirect("~/Views/StoreClerk/ViewSupplierList.aspx");
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text != null)
            {


                GridView1.DataSource = SearchCatagories(TextBox2.Text);
                GridView1.DataBind();
            }
        }
        public List<Supplier> SearchCatagories(string param)
        {
            // return s.Suppliers.Where(x => x.supplier_id == id).ToList();

            return s.Suppliers.Where(x => x.name.Contains(param) || x.contact_name.Contains(param) || x.contact_num.Contains(param) || x.fax_num.Contains(param) || x.address.Contains(param) || x.gst_reg_num.Contains(param)).ToList();
        }
    }
}