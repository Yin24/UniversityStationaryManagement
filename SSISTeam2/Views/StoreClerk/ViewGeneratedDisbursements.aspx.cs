using SSISTeam2.Classes.EFFacades;
using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class ViewGeneratedDisbursements : System.Web.UI.Page
    {
        private const string SESSION_DEPARTMENT_LIST = "ViewGeneratedDisbursementsForms_DepartmentList";
        private const string SESSION_DISBURSING_LIST = "ViewGeneratedDisbursementsForms_DisbursingList";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            using (SSISEntities context = new SSISEntities())
            {
                List<Collection_Point> collectionPts = context.Collection_Point.Where(w => w.deleted != "Y").ToList();
                List<Department> departmentList = context.Departments.Where(w => w.deleted != "Y").ToList();

                Session[SESSION_DEPARTMENT_LIST] = departmentList;

                if (collectionPts.Count == 0) return;

                // If the user is tagged to a collection point, add it into the location name
                string currentUser = User.Identity.Name;

                int currentCollectionPtId = collectionPts.First().collection_pt_id;
                // Get the department codes that are related to this value
                List<Department> departments = departmentList.Where(w => w.collection_point == currentCollectionPtId).ToList();

                // Get all that can be disbursed given the current user
                DisbursementModelCollection disbursingList = FacadeFactory.getDisbursementService(context).getAllThatCanBeSignedOff(currentUser);

                //Session[SESSION_DISBURSING_LIST] = disbursingList;

                string currentDepartmentCode = departments.First().dept_code;

                var itemGroups = disbursingList.SelectMany(sm =>
                    sm.Items
                    .Select(s => new { s.Key.ItemCode, s.Key.Description, Quantity = s.Value, sm.Department.dept_code, sm.RequestId, sm.Department.name, sm.CollectionPtId })
                ).GroupBy(k => new { k.ItemCode, k.Description, DeptCode = k.dept_code }, v => v).ToList();

                /* Filter active collection points */
                List<int> activeCollectionPoints = itemGroups.SelectMany(sm => sm.Select(s => s.CollectionPtId)).Distinct().ToList();

                foreach (var collectionPt in collectionPts)
                {
                    if (collectionPt.username == currentUser)
                    {
                        collectionPt.location += " [Assigned to you]";
                    }
                    if ( ! activeCollectionPoints.Contains(collectionPt.collection_pt_id))
                    {
                        collectionPt.location += " (Empty)";
                    }
                }

                ddlCollectionPoints.DataSource = collectionPts;
                ddlCollectionPoints.DataValueField = "collection_pt_id";
                ddlCollectionPoints.DataTextField = "location";
                ddlCollectionPoints.DataBind();

                List<ConfirmDisbursementViewModel> list = new List<ConfirmDisbursementViewModel>();

                foreach (var itemGroup in itemGroups)
                {

                    int itemQty = itemGroup.Select(s => s.Quantity).Aggregate((a, b) => a + b);
                    List<int> reqIds = itemGroup.Select(s => s.RequestId).ToList();

                    ConfirmDisbursementViewModel model = new ConfirmDisbursementViewModel();
                    model.ItemCode = itemGroup.Key.ItemCode;
                    model.ItemDescription = itemGroup.Key.Description;
                    model.DeptCode = itemGroup.Key.DeptCode;
                    //model.DeptName = deptGroup.First().name;
                    model.QuantityExpected = itemQty;
                    model.QuantityActual = itemQty;
                    model.RequestIds = reqIds;
                    //model.Include = true;

                    list.Add(model);
                }

                Session[SESSION_DISBURSING_LIST] = list;

                list = list.Where(w => w.DeptCode == currentDepartmentCode).ToList();

                //_refreshGrid(list);

                lblNoDepartments.Visible = false;

                _refrehDepartmentsDropDown(departments);
            }
        }
        private void _refreshGrid(List<ConfirmDisbursementViewModel> list)
        {
            if (list.Count > 0)
            {
                panelNoItems.Visible = false;
                if (ddlDepartments.Items.Count == 0)
                {
                  panelNormal.Visible = false;
                } else
                {
                  panelNormal.Visible = true;
                }
            }
            else
            {
                panelNoItems.Visible = true;
                panelNormal.Visible = false;
            }

            gvDisbursement.DataSource = list;
            gvDisbursement.DataBind();
            //MergeCells(gvToRetrieve);
        }

        private void _refrehDepartmentsDropDown(List<Department> departments) {
            List<ConfirmDisbursementViewModel> list = Session[SESSION_DISBURSING_LIST] as List<ConfirmDisbursementViewModel>;

            List<string> activeDeptCodes = list.Select(s => s.DeptCode).ToList();

            foreach (var dept in departments)
            {
                if ( ! activeDeptCodes.Contains(dept.dept_code) && ! dept.name.Contains("(Empty)"))
                {
                    dept.name += " (Empty)";
                }
            }

            ddlDepartments.DataSource = departments;
            ddlDepartments.DataValueField = "dept_code";
            ddlDepartments.DataTextField = "name";
            ddlDepartments.DataBind();

            ddlDepartments_SelectedIndexChanged(ddlDepartments, new EventArgs());
        }

        protected void ddlCollectionPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Department> departmentList = Session[SESSION_DEPARTMENT_LIST] as List<Department>;

            // Get the current value
            DropDownList ddl = sender as DropDownList;

            int selectedCollectionPtId = int.Parse(ddl.SelectedValue);
            // Get the department codes that are related to this value
            List<Department> departments = departmentList.Where(w => w.collection_point == selectedCollectionPtId).ToList();

            if (departments.Count == 0)
            {
                lblNoDepartments.Visible = true;
                ddlDepartments.Enabled = false;
            }
            else
            {
                lblNoDepartments.Visible = false;
                ddlDepartments.Enabled = true;
            }

            _refrehDepartmentsDropDown(departments);
        }

        protected void ddlDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ConfirmDisbursementViewModel> list = Session[SESSION_DISBURSING_LIST] as List<ConfirmDisbursementViewModel>;

            // Get the current value
            DropDownList ddl = sender as DropDownList;

            string selectedDeptCode = ddl.SelectedValue;

            // Filter list based on the selectedVal
            var filtered = list.Where(w => w.DeptCode == selectedDeptCode).ToList();

            _refreshGrid(filtered);
        }

        protected void btnGoToConfirm_Click(object sender, EventArgs e)
        {
            string currentDeptCode = ddlDepartments.SelectedValue;

            Response.Redirect("ConfirmDisbursement.aspx?dept=" + currentDeptCode);
        }
    }
    class ConfirmDisbursementViewModel : ConfirmRetrievalViewModel
    {
        string deptCode;

        public string DeptCode
        {
            get
            {
                return deptCode;
            }

            set
            {
                deptCode = value;
            }
        }
    }

}