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
    public partial class ConfirmDisbursement : System.Web.UI.Page
    {
        private const string SESSION_DEPARTMENT_LIST = "ConfirmDisbursement_DepartmentList";
        private const string SESSION_COLLECTION_PT_LIST = "ConfirmDisbursement_CollectionPtList";
        private const string SESSION_USER_LIST = "ConfirmDisbursement_UserList";
        private const string SESSION_DISBURSING_LIST = "ConfirmDisbursement_DisbursingList";
        private const string SESSION_CURRENT_DEPT_CODE = "ConfirmDisbursement_CurrentDeptCode";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            panelNoItems.Visible = false;
            panelNormal.Visible = false;

            string forwardedDeptCode = Request.QueryString["dept"];

            using (SSISEntities context = new SSISEntities())
            {
                //List<Collection_Point> collectionPts = context.Collection_Point.Where(w => w.deleted != "Y").ToList();
                List<Department> departmentList = context.Departments.Where(w => w.deleted != "Y").ToList();
                List<Collection_Point> collectionPtList = context.Collection_Point.Where(w => w.deleted != "Y").ToList();
                List<Dept_Registry> usersList = context.Dept_Registry.Where(w => w.deleted != "Y").ToList();

                Session[SESSION_DEPARTMENT_LIST] = departmentList;
                Session[SESSION_COLLECTION_PT_LIST] = collectionPtList;
                Session[SESSION_USER_LIST] = usersList;

                string currentUser = User.Identity.Name;

                // Get all that can be disbursed given the current user
                DisbursementModelCollection disbursingList = FacadeFactory.getDisbursementService(context).getAllThatCanBeSignedOff(currentUser);

                //Session[SESSION_DISBURSING_LIST] = disbursingList;

                var itemGroups = disbursingList.SelectMany(sm =>
                    sm.Items
                    .Select(s => new { s.Key.ItemCode, s.Key.Description, Quantity = s.Value, sm.Department.dept_code, sm.RequestId, sm.Department.name })
                ).GroupBy(k => new { k.ItemCode, k.Description, DeptCode = k.dept_code }, v => v).ToList();

                List<ConfirmDisbursementViewModel> filteredDisbursingList = new List<ConfirmDisbursementViewModel>();

                foreach (var itemGroup in itemGroups)
                {

                    int itemQty = itemGroup.Select(s => s.Quantity).Where(w => w > 0).Aggregate((a, b) => a + b);
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

                    filteredDisbursingList.Add(model);
                }

                string currentDepartmentCode = departmentList.First().dept_code;

                if (forwardedDeptCode != null)
                {
                    // Forwarded from View Generated forms
                    currentDepartmentCode = forwardedDeptCode;
                }

                // Mark empty departments
                List<string> deptCodes = filteredDisbursingList.Select(s => s.DeptCode).ToList();

                foreach (var dept in departmentList)
                {
                    // If the dept does not have disbursements:
                    if (! deptCodes.Contains(dept.dept_code))
                    {
                        dept.name += " (empty)";
                    }
                }


                Session[SESSION_DISBURSING_LIST] = filteredDisbursingList;
                Session[SESSION_CURRENT_DEPT_CODE] = currentDepartmentCode;

                //filteredDisbursingList = filteredDisbursingList.Where(w => w.DeptCode == currentDepartmentCode).ToList();

                _refreshDepartmentsDropDown(departmentList);

                _refreshGrid(filteredDisbursingList);

                ddlDepartments.SelectedValue = currentDepartmentCode;

                if (departmentList.Count == 0 || disbursingList.Count == 0)
                {
                    panelNoItems.Visible = true;
                } else
                {
                    panelNormal.Visible = true;
                }

            }
        }
        private void _refreshGrid(List<ConfirmDisbursementViewModel> list)
        {
            string selectedDeptCode = Session[SESSION_CURRENT_DEPT_CODE] as string;

            var filtered = list.Where(w => w.DeptCode == selectedDeptCode).ToList();

            gvDisbursement.DataSource = filtered;
            gvDisbursement.DataBind();
            //MergeCells(gvToRetrieve);

            // Update representative name and collection point location
            List<Department> deptList = Session[SESSION_DEPARTMENT_LIST] as List<Department>;
            List<Collection_Point> collectionPtList = Session[SESSION_COLLECTION_PT_LIST] as List<Collection_Point>;
            List<Dept_Registry> usersList = Session[SESSION_USER_LIST] as List<Dept_Registry>;

            Department dep = deptList.Find(f => f.dept_code == selectedDeptCode);
            lblRepName.Text = "Representative: " + new UserModel(dep.rep_user).Fullname; //lblRepName.Text = "Representative: " + usersList.Find(f => f.username == dep.rep_user).fullname;
            lblCollectionPtLocation.Text = "Collection point: " + collectionPtList.Find(f => f.collection_pt_id == dep.collection_point).location;

            if (filtered.Count == 0)
            {
                panelNoItems.Visible = true;
                panelNormal.Visible = false;
            }
            else
            {
                panelNoItems.Visible = false;
                panelNormal.Visible = true;
            }
        }

        private void _refreshDepartmentsDropDown(List<Department> departments)
        {
            ddlDepartments.DataSource = departments;
            ddlDepartments.DataValueField = "dept_code";
            ddlDepartments.DataTextField = "name";
            ddlDepartments.DataBind();

            //ddlDepartments_SelectedIndexChanged(ddlDepartments, new EventArgs());
        }

        protected void ddlDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ConfirmDisbursementViewModel> list = Session[SESSION_DISBURSING_LIST] as List<ConfirmDisbursementViewModel>;


            // Reset all quantities
            list.ForEach(l => l.QuantityActual = l.QuantityExpected);

            _checkRowsForDiscrepancies(list);

            // Save it back
            Session[SESSION_DISBURSING_LIST] = list;

            // Get the current value
            DropDownList ddl = sender as DropDownList;

            string selectedDeptCode = ddl.SelectedValue;

            Session[SESSION_CURRENT_DEPT_CODE] = selectedDeptCode;
            // Filter list based on the selectedVal
            //var filtered = list.Where(w => w.DeptCode == selectedDeptCode).ToList();

            _refreshGrid(list);
        }

        protected void btnResetRowQty_Click(object sender, EventArgs e)
        {
            List<ConfirmDisbursementViewModel> list = Session[SESSION_DISBURSING_LIST] as List<ConfirmDisbursementViewModel>;

            Button btn = sender as Button;
            GridViewRow gvr = btn.Parent.Parent as GridViewRow;

            int index = gvr.DataItemIndex;

            string selectedDeptCode = Session[SESSION_CURRENT_DEPT_CODE] as string;
            var filteredList = list.Where(w => w.DeptCode == selectedDeptCode).ToList();
            string currentItemCode = filteredList[index].ItemCode;

            //list[index].QuantityActual = list[index].QuantityExpected;
            list.Find(f => f.DeptCode == selectedDeptCode && f.ItemCode == currentItemCode).QuantityActual = list.Find(f => f.DeptCode == selectedDeptCode && f.ItemCode == currentItemCode).QuantityExpected;

            Session[SESSION_DISBURSING_LIST] = list;

            _checkRowsForDiscrepancies(list);

            //ddlDepartments_SelectedIndexChanged(ddlDepartments, new EventArgs());
            _refreshGrid(list);
        }

        protected void tbQtyActual_TextChanged(object sender, EventArgs e)
        {
            List<ConfirmDisbursementViewModel> list = Session[SESSION_DISBURSING_LIST] as List<ConfirmDisbursementViewModel>;

            TextBox tb = sender as TextBox;
            GridViewRow gvr = tb.Parent.Parent as GridViewRow;

            int index = gvr.DataItemIndex;

            string selectedDeptCode = Session[SESSION_CURRENT_DEPT_CODE] as string;
            var filteredList = list.Where(w => w.DeptCode == selectedDeptCode).ToList();
            string currentItemCode = filteredList[index].ItemCode;

            string valStr = tb.Text;
            int value = 0;

            if (int.TryParse(valStr, out value))
            {
                // Could convert
                int expectedQty = list.Find(f => f.DeptCode == selectedDeptCode && f.ItemCode == currentItemCode).QuantityExpected;
                int updatedValue = value;

                if (value > expectedQty)
                {
                    updatedValue = expectedQty;
                }
                else if (value < 0)
                {
                    updatedValue = 0;
                }

                list.Find(f => f.DeptCode == selectedDeptCode && f.ItemCode == currentItemCode).QuantityActual = updatedValue;
            }

            Session[SESSION_DISBURSING_LIST] = list;

            _checkRowsForDiscrepancies(list);

            //ddlDepartments_SelectedIndexChanged(ddlDepartments, new EventArgs());
            _refreshGrid(list);
        }

        private void _checkRowsForDiscrepancies(List<ConfirmDisbursementViewModel> list)
        {
            bool noDiscrepancies = list.All(a => a.QuantityActual == a.QuantityExpected);

            if (noDiscrepancies)
            {
                btnSubmit.Text = "Confirm quantities";
            }
            else
            {
                btnSubmit.Text = "Confirm quantities, then file discrepancies";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Get all the models
            List<ConfirmDisbursementViewModel> list = Session[SESSION_DISBURSING_LIST] as List<ConfirmDisbursementViewModel>;

            string selectedDeptCode = Session[SESSION_CURRENT_DEPT_CODE] as string;
            list = list.Where(w => w.DeptCode == selectedDeptCode).ToList();

            List<ConfirmDisbursementViewModel> okayItems = new List<ConfirmDisbursementViewModel>();
            Dictionary<ConfirmDisbursementViewModel, int> notOkayItems = new Dictionary<ConfirmDisbursementViewModel, int>();


            // For each gvr, check if actual == expected
            foreach (ConfirmDisbursementViewModel model in list)//gvToRetrieve.Rows)
            {
                //string expected = (gvr.FindControl("lblQtyExpected") as Label).Text;
                //string actual = (gvr.FindControl("tbQtyActual") as Label).Text;

                //int expectedQty = int.Parse(expected);
                //int actualQty = int.Parse(actual);
                int expectedQty = model.QuantityExpected;
                int actualQty = model.QuantityActual;

                if (actualQty > expectedQty)
                {
                    lblWarningInfo.Text = "Items cannot have a higher actual quantity than what was to be retrieved.";
                    //string.Format("Row {0} cannot have a higher actual quantity than expected.", gvr.DataItemIndex + 1);
                }

                if (expectedQty == actualQty)
                {
                    okayItems.Add(model);
                }
                else
                {
                    int difference = expectedQty - actualQty;
                    notOkayItems.Add(model, difference);
                }
            }

            using (SSISEntities context = new SSISEntities())
            {

                // Save the Okay Items
                // Get a list of all request ids and item code (de-normalized)
                okayItems.SelectMany(sm => sm.RequestIds
                            .Select(s => new { RequestId = s, sm.ItemCode })
                        )
                        // Normalise and group by requestId
                        .GroupBy(k => k.RequestId, v => v.ItemCode)
                        .ToList()
                        // For each, get the Request object, match the items in the request, and save to DB
                        .ForEach(idAndItemCode =>
                        {
                            Request request = context.Requests.Find(idAndItemCode.Key);

                            Dictionary<string, int> itemCodeAndQty =
                                request.Request_Details
                                .Where(w => w.deleted != "Y"
                                            && idAndItemCode.Contains(w.item_code)
                                    )
                                .ToDictionary(
                                    k => k.item_code,
                                    v => v.Request_Event.First().allocated.Value
                                );

                            FacadeFactory.getRequestMovementService(context).moveFromDisbursingToDisbursed(idAndItemCode.Key, itemCodeAndQty, User.Identity.Name);
                        });

                Dictionary<
                    string,
                    Dictionary<int, int>
                    >
                    itemCodeAndIdAndQty = new Dictionary<string, Dictionary<int, int>>();


                // Go through not okay items and shift quantities around
                foreach (var item in notOkayItems)
                {
                    // Get list of all requestIds
                    List<Request> requestsByDateDesc = item.Key.RequestIds.Select(s => context.Requests.Find(s)).Where(w => w.deleted != "Y").OrderByDescending(o => o.date_time).ToList();
                    int shortfall = item.Value;

                    Dictionary<int, int> idAndQty = new Dictionary<int, int>();

                    // First is the latest made request
                    foreach (var request in requestsByDateDesc)
                    {
                        Request_Details detail = request.Request_Details.Where(w => w.deleted != "Y" && w.item_code == item.Key.ItemCode).DefaultIfEmpty(null).FirstOrDefault();
                        if (detail == null) continue;

                        int origQty = detail.Request_Event.First().allocated.Value;
                        int retrievedQty = origQty;

                        if (shortfall > 0)
                        {
                            if (shortfall > origQty)
                            {
                                shortfall -= origQty;
                                retrievedQty = 0;
                            }
                            else if (shortfall == origQty)
                            {
                                shortfall = 0;
                                retrievedQty = 0;
                            }
                            else
                            {
                                // shortfall < origQty
                                retrievedQty = origQty - shortfall;
                                shortfall = 0;
                            }
                        }

                        idAndQty.Add(request.request_id, retrievedQty);
                    }

                    itemCodeAndIdAndQty.Add(item.Key.ItemCode, idAndQty);

                    // Minus the expected quantity for each request's item origQty, while expected quantity is > 0,
                    // and don't minus if origQty is > expectedQty
                    // Once you've hit zero or cannot minus, assign the leftover into the next request
                    // Then the rest of the requests are gonna be zero
                    // Then save all these requests using the MovementService

                }

                var notOkayGroupingsByRequestId = itemCodeAndIdAndQty
                    .SelectMany(sm =>
                        sm.Value.Select(s => new { requestId = s.Key, itemCode = sm.Key, retrievedQty = s.Value })
                    )
                    .GroupBy(k => k.requestId, v => new { v.itemCode, v.retrievedQty });

                foreach (var grouping in notOkayGroupingsByRequestId)
                {
                    var items = grouping.ToDictionary(k => k.itemCode, v => v.retrievedQty);
                    FacadeFactory.getRequestMovementService(context).moveFromDisbursingToDisbursed(grouping.Key, items, User.Identity.Name);
                }

                context.SaveChanges();

            } // Disposal of context

            // If there were not okay items, record into file discrepancy session variable
            if (notOkayItems.Count > 0)
            {
                DiscrepancyDictionary discrepancyDict = Session[ConfirmRetrieval.PUBLIC_SESSION_DISCREPANCY_DICT] as DiscrepancyDictionary;

                if (discrepancyDict == null) discrepancyDict = new DiscrepancyDictionary();

                foreach (var item in notOkayItems)
                {
                    discrepancyDict.AddOrIncrease(item.Key.ItemCode, item.Value);
                }

                Session[ConfirmRetrieval.PUBLIC_SESSION_DISCREPANCY_DICT] = discrepancyDict;

                // Enable button to continue to fileDiscrepancies

                // Change to redirect to heng tiong's thing
                Response.Redirect("FileDiscrepency.aspx", false);

            }
            else
            {
                Response.Redirect(Request.Url.ToString(), false);
            }

        }
    }
}