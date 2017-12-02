using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2
{
    public partial class ViewAndMaintainSupplier : System.Web.UI.Page
    {
        SSISEntities s = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()

        {
            GridView1.DataSource = s.Suppliers.ToList();
            GridView1.DataBind();
        }
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string supplier_id = (row.FindControl("TextBox1") as TextBox).Text;
            string name = (row.FindControl("TextBox3") as TextBox).Text;
            string contact_name = (row.FindControl("TextBox5") as TextBox).Text;
            string contact_num = (row.FindControl("TextBox6") as TextBox).Text;
            string fax_num = (row.FindControl("TextBox7") as TextBox).Text;
            string address = (row.FindControl("TextBox4") as TextBox).Text;
            string gst_reg_num = (row.FindControl("TextBox2") as TextBox).Text;

            using (SSISEntities s = new SSISEntities())
            {
                Supplier supplier= s.Suppliers.Where(p => p.supplier_id == supplier_id).First<Supplier>();
                supplier.supplier_id = supplier_id;
                supplier.name = name;
                supplier.contact_name = contact_name;
                supplier.contact_num = contact_num;
                supplier.fax_num = fax_num;
                supplier.address = address;
                supplier.gst_reg_num = gst_reg_num;

                s.SaveChanges();
            }
            GridView1.EditIndex = -1;
            this.BindGrid();
        }
     
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string supplier_id = (row.FindControl("TextBox1") as TextBox).Text;
            using (SSISEntities s = new SSISEntities())
            {
                Supplier supplier = s.Suppliers.Where(p => p.supplier_id == supplier_id).First<Supplier>();
                supplier.deleted = "Y";
                s.SaveChanges();
            }
            this.BindGrid();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(TextBox1.Text != null)
            {


                GridView1.DataSource = SearchCatagories(TextBox1.Text);
                GridView1.DataBind();
            }
        }
        public List<Supplier> SearchCatagories(string param)
        {
           // return s.Suppliers.Where(x => x.supplier_id == id).ToList();
          
            return s.Suppliers.Where(x => x.name.Contains(param) || x.contact_name.Contains(param)||x.contact_num.Contains(param)||x.fax_num.Contains(param)||x.address.Contains(param)||x.gst_reg_num.Contains(param)).ToList();
        }
        protected void OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            this.BindGrid();
        }
    }
}