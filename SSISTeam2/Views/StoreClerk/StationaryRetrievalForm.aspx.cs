using SSISTeam2.Classes.EFFacades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class StationaryRetrievalForm : System.Web.UI.Page
    {
        SSISEntities entities = new SSISEntities();
        
        private void MergeCells()
        {
            int totalQty = 0;
            int i = GridView1.Rows.Count - 2;
            while (i >= 0)
            {
                GridViewRow curRow = GridView1.Rows[i];
                GridViewRow preRow = GridView1.Rows[i + 1];

                //int j = 1;
                //while (j < curRow.Cells.Count)
                //{
                //    if (curRow.Cells[j].Text == preRow.Cells[j].Text)
                //    {
                //        if (preRow.Cells[j].RowSpan < 2)
                //        {
                //            curRow.Cells[j].RowSpan = 2;
                //            preRow.Cells[j].Visible = false;
                //        }
                //        else
                //        {
                //            curRow.Cells[j].RowSpan = preRow.Cells[j].RowSpan + 1;
                //            preRow.Cells[j].Visible = false;
                //        }
                //    }
                //    j++;
                //}
                //i--;
                if (curRow.Cells[1].Text == preRow.Cells[1].Text)
                {
                    if (preRow.Cells[1].RowSpan < 2)
                    {
                        curRow.Cells[1].RowSpan = 2;
                        preRow.Cells[1].Visible = false;
                        totalQty = Convert.ToInt32(preRow.Cells[2].Text) + Convert.ToInt32(curRow.Cells[2].Text);
                        curRow.Cells[2].Text = totalQty.ToString();
                        curRow.Cells[2].RowSpan = 2;
                        preRow.Cells[2].Visible = false;
                    }
                    else
                    {
                        curRow.Cells[1].RowSpan = preRow.Cells[1].RowSpan + 1;
                        preRow.Cells[1].Visible = false;
                        totalQty = totalQty + Convert.ToInt32(curRow.Cells[2].Text);
                        curRow.Cells[2].Text = totalQty.ToString();
                        curRow.Cells[2].RowSpan = preRow.Cells[2].RowSpan + 1;
                        preRow.Cells[2].Visible = false;
                    }
                }
                i--;
            }


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            var result = from t1 in entities.Departments
                         join t2 in entities.Requests
                         on t1.dept_code equals t2.dept_code
                         join t3 in entities.Request_Details
                         on t2.request_id equals t3.request_id
                         join t4 in entities.Request_Event
                         on t3.request_detail_id equals t4.request_detail_id
                         join t5 in entities.Stock_Inventory
                         on t3.item_code equals t5.item_code
                         where t1.deleted.Equals("N")
                         && t2.deleted.Equals("N")
                         && t3.deleted.Equals("N")
                         && t4.deleted.Equals("N")
                         && t5.deleted.Equals("N")
                         orderby t5.item_description
                         select new { t5.item_description, t1.dept_code, t4.quantity };

            using (SSISEntities context = new SSISEntities())
            {
                //var result2 = FacadeFactory.getAllocatedService(context).getAllAllocated()
            }


            GridView1.DataSource = result.ToList();
            GridView1.DataBind();
            MergeCells();
            



            //var result = from t1 in entities.Departments
            //             join t2 in entities.Requests
            //             on t1.dept_code equals t2.dept_code
            //             join t3 in entities.Request_Details
            //             on t2.request_id equals t3.request_id
            //             join t4 in entities.Request_Event
            //             on t3.request_detail_id equals t4.request_detail_id
            //             join t5 in entities.Stock_Inventory
            //             on t3.item_code equals t5.item_code
            //             group t4 by new { t5.item_description} into g
            //             select new {
            //                 item_description = g.Key.item_description,
            //                 quantity = g.Sum(t4=> t4.quantity)
            //             };

        }


        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string itemDescription = row.Cells[1].Text;
            string departmentCode = row.Cells[3].Text;


            //var result = from t1 in entities.Departments
            //             join t2 in entities.Requests
            //             on t1.dept_code equals t2.dept_code
            //             join t3 in entities.Request_Details
            //             on t2.request_id equals t3.request_id
            //             join t4 in entities.Request_Event
            //             on t3.request_detail_id equals t4.request_detail_id
            //             join t5 in entities.Stock_Inventory
            //             on t3.item_code equals t5.item_code
            //             where t5.item_description.Equals(itemDescription)
            //             && t2.dept_code.Equals(departmentCode)
            //             select new { t2.request_id, t3.request_detail_id, t4.request_event_id };
            var result  = (from t1 in entities.Departments
                         join t2 in entities.Requests
                         on t1.dept_code equals t2.dept_code
                         join t3 in entities.Request_Details
                         on t2.request_id equals t3.request_id
                         join t4 in entities.Request_Event
                         on t3.request_detail_id equals t4.request_detail_id
                         join t5 in entities.Stock_Inventory
                         on t3.item_code equals t5.item_code
                         where t5.item_description.Equals(itemDescription)
                         && t2.dept_code.Equals(departmentCode)
                         select new { t2.request_id, t3.request_detail_id, t4.request_event_id }).ToList();
            //GridView2.DataSource = result;
            //GridView2.DataBind();
            
                Label1.Text=result[0].request_id.ToString();
                Label2.Text = result[0].request_detail_id.ToString();
                Label3.Text = result[0].request_event_id.ToString();

            //int[] delList = new int[3];
            //ArrayList al = new ArrayList();
            //al.Add(result);
            //foreach (int l in al)
            //{
            //    Label1.Text = l.ToString();

            //}
            ////al = (ArrayList)result;
            //for (int i = 0; i < delList.Length; i++)
            //{
            //    delList[i] = (int)al[i];

            //}
            //Label1.Text = delList[0].ToString();
            //Label2.Text = delList[1].ToString();
            //Label3.Text = delList[3].ToString();

            //int[] delList = new int[3];
            //delList = result.ToArray();
            //ArrayList al = new ArrayList();
            //al.Add(result.ToList());
            //for(int i = 0; i < al.Count; i++)
            //{
            //    Label1.Text = al[i].ToString();
            //}





        }

    }
}