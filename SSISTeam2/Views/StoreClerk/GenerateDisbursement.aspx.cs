using SSISTeam2.Classes;
using SSISTeam2.Classes.EFFacades;
using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class GenerateDisbursement : System.Web.UI.Page
    {
        private const string SESSION_DISBURSE_LIST = "GenerateDisbursement_DisburseList";
        private const string SESSION_COLLECTION_PT_LIST = "GenerateDisbursement_CollectionPtList";
        private const string SESSION_CURRENT_COLLECTION_PT = "GenerateDisbursement_CurrentCollectionPt";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                return;
            }

            panelNoItems.Visible = false;
            panelNormal.Visible = false;
            using (SSISEntities context = new SSISEntities())
            {
                List<Collection_Point> collectionPts = context.Collection_Point.Where(w => w.deleted != "Y").ToList();

                if (collectionPts.Count == 0) return;

                //var retrieved = FacadeFactory.getDisbursementService(context).getAllPossibleDisbursementsForCollectionPoint(collectionPts.First().collection_pt_id);
                var retrieved = FacadeFactory.getDisbursementService(context).getAllPossibleDisbursements();

                var itemGroups = retrieved.SelectMany(sm =>
                    sm.Items
                    .Select(s => new { s.Key.ItemCode, s.Key.Description, s.Value, sm.Department.dept_code, sm.RequestId, sm.Department.name, sm.CollectionPtId })
                ).GroupBy(k => k.ItemCode, v => v).ToList();

                /* Filter active collection points */
                List<int> activeCollectionPoints = itemGroups.SelectMany(sm => sm.Select(s => s.CollectionPtId)).Distinct().ToList();

                // If the user is tagged to a collection point, add it into the location name
                string currentUser = User.Identity.Name;

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

                Session[SESSION_COLLECTION_PT_LIST] = collectionPts;

                ddlCollectionPoint.DataSource = collectionPts;
                ddlCollectionPoint.DataValueField = "collection_pt_id";
                ddlCollectionPoint.DataTextField = "location";
                ddlCollectionPoint.DataBind();

                List<GenerateDisbursementViewModel> list = new List<GenerateDisbursementViewModel>();

                foreach (var itemGroup in itemGroups)
                {
                    var deptGroups = itemGroup.GroupBy(k => k.dept_code, v => v);
                    foreach (var deptGroup in deptGroups)
                    {
                        int deptQty = deptGroup.Select(s => s.Value).Aggregate((a, b) => a + b);
                        List<int> reqIds = deptGroup.Select(s => s.RequestId).ToList();

                        GenerateDisbursementViewModel model = new GenerateDisbursementViewModel();
                        model.ItemCode = itemGroup.Key;
                        model.ItemDescription = itemGroup.First().Description;
                        model.DeptCode = deptGroup.Key;
                        model.DeptName = deptGroup.First().name;
                        model.Quantity = deptQty;
                        model.RequestIds = reqIds;
                        model.Include = true;
                        model.CollectionPtId = deptGroup.First().CollectionPtId;

                        list.Add(model);
                    }
                }

                //lblDebug.Text = list.Count.ToString();

                Session[SESSION_COLLECTION_PT_LIST] = list;
                int currentCollectionPtId = collectionPts.First().collection_pt_id;
                Session[SESSION_CURRENT_COLLECTION_PT] = currentCollectionPtId;

                _refreshGrid(list);

                if (retrieved.Count == 0)
                {
                    panelNoItems.Visible = true;
                    return;
                }

                panelNormal.Visible = true;
            }
        }

        protected void chkbxInclude_CheckedChanged(object sender, EventArgs e)
        {
            List<GenerateDisbursementViewModel> list = Session[SESSION_COLLECTION_PT_LIST] as List<GenerateDisbursementViewModel>;
            CheckBox chkBox = sender as CheckBox;
            GridViewRow gvr = chkBox.Parent.Parent as GridViewRow;

            int currentCollectionPtId = (int)Session[SESSION_CURRENT_COLLECTION_PT];

            int selectedIndex = gvr.DataItemIndex;

            var filtered = list;
            if (currentCollectionPtId > 0)
            {
                filtered = list.Where(w => w.CollectionPtId == currentCollectionPtId).ToList();
            }



            // Flip the include boolean
            filtered[selectedIndex].Include = !filtered[selectedIndex].Include;

            Session[SESSION_COLLECTION_PT_LIST] = list;

            _refreshGrid(list);
        }

        private void _refreshGrid(List<GenerateDisbursementViewModel> list)
        {
            int currentCollectionPtId = (int) Session[SESSION_CURRENT_COLLECTION_PT];

            var filtered = list;
            if (currentCollectionPtId > 0)
            {
                filtered = list.Where(w => w.CollectionPtId == currentCollectionPtId).ToList();
            }

            panelNoItems.Visible = false;
            panelNormal.Visible = false;

            if (filtered.Count > 0)
            {
                // Items to show
                panelNormal.Visible = true;
            } else
            {
                // Nothing to show
                panelNoItems.Visible = true;
            }

            gvToRetrieve.DataSource = filtered;
            gvToRetrieve.DataBind();
            MergeCells(gvToRetrieve);
        }

        private void MergeCells(GridView gv)
        {
            int totalQty = 0;
            int i = gv.Rows.Count - 2;
            while (i >= 0)
            {
                GridViewRow curRow = gv.Rows[i];
                GridViewRow preRow = gv.Rows[i + 1];

                string curRowDescription = (curRow.FindControl("lblDescription") as Label).Text;
                string preRowDescription = (preRow.FindControl("lblDescription") as Label).Text;
                Label curRowTotalQtyLabel = curRow.FindControl("lblTotalQty") as Label;
                Label preRowTotalQtyLabel = preRow.FindControl("lblTotalQty") as Label;

                string curRowTotalQty = curRowTotalQtyLabel.Text;
                string preRowTotalQty = preRowTotalQtyLabel.Text;

                if (curRowDescription == preRowDescription)
                {
                    if (preRow.Cells[1].RowSpan < 2)
                    {
                        curRow.Cells[1].RowSpan = 2;
                        preRow.Cells[1].Visible = false;
                        totalQty = Convert.ToInt32(preRowTotalQty) + Convert.ToInt32(curRowTotalQty);
                        curRowTotalQtyLabel.Text = totalQty.ToString();
                        curRow.Cells[2].RowSpan = 2;
                        preRow.Cells[2].Visible = false;
                    }
                    else
                    {
                        curRow.Cells[1].RowSpan = preRow.Cells[1].RowSpan + 1;
                        preRow.Cells[1].Visible = false;
                        totalQty = totalQty + Convert.ToInt32(curRowTotalQty);
                        curRowTotalQtyLabel.Text = totalQty.ToString();
                        curRow.Cells[2].RowSpan = preRow.Cells[2].RowSpan + 1;
                        preRow.Cells[2].Visible = false;
                    }
                }
                i--;
            }


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Get current collection point
            int collectionPtId = Convert.ToInt32(ddlCollectionPoint.SelectedValue);

            // Get all the models
            List<GenerateDisbursementViewModel> list = Session[SESSION_COLLECTION_PT_LIST] as List<GenerateDisbursementViewModel>;
            // Convert to ids and items to retrieve
            var listByRequestIds = list
                .SelectMany(sm => sm.RequestIds
                .Select(s => new { RequestId = s, sm.ItemCode, sm.Include, sm.CollectionPtId }))
                .Where(w => w.Include && w.CollectionPtId == collectionPtId)
                .GroupBy(k => k.RequestId, v => v.ItemCode);

            using (SSISEntities context = new SSISEntities())
            {
                foreach (var request in listByRequestIds)
                {
                    FacadeFactory.getRequestMovementService(context).moveFromRetrievedToDisbursing(request.Key, request.ToList(), User.Identity.Name);
                }

                context.SaveChanges();
            }

            _sendEmail(User.Identity.Name, list.Where(w => w.CollectionPtId == collectionPtId).Select(s => s.DeptCode).Distinct().ToList());

            Response.Redirect(Request.Url.ToString(), false);
        }

        protected void ddlCollectionPoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<GenerateDisbursementViewModel> list = Session[SESSION_COLLECTION_PT_LIST] as List<GenerateDisbursementViewModel>;

            DropDownList ddl = sender as DropDownList;

            int newCollectionPtId = int.Parse(ddl.SelectedValue);

            Session[SESSION_CURRENT_COLLECTION_PT] = newCollectionPtId;

            _refreshGrid(list);
        }

        protected void btnSubmitAll_Click(object sender, EventArgs e)
        {
            // Get all the models
            List<GenerateDisbursementViewModel> list = Session[SESSION_COLLECTION_PT_LIST] as List<GenerateDisbursementViewModel>;
            // Convert to ids and items to retrieve
            var listByRequestIds = list
                .SelectMany(sm => sm.RequestIds
                .Select(s => new { RequestId = s, sm.ItemCode, sm.Include, sm.CollectionPtId }))
                .Where(w => w.Include)
                .GroupBy(k => k.RequestId, v => v.ItemCode);

            using (SSISEntities context = new SSISEntities())
            {
                foreach (var request in listByRequestIds)
                {
                    FacadeFactory.getRequestMovementService(context).moveFromRetrievedToDisbursing(request.Key, request.ToList(), User.Identity.Name);
                }

                context.SaveChanges();
            }

            _sendEmail(User.Identity.Name, list.Select(s => s.DeptCode).Distinct().ToList());

            Response.Redirect(Request.Url.ToString(), false);
        }

        private void _sendEmail(string currentUser, List<string> deptCodes)
        {
            UserModel currentUserModel = new UserModel(currentUser);

            List<UserModel> repUsers = new List<UserModel>();

            using (SSISEntities context = new SSISEntities())
            {
                foreach (var deptCode in deptCodes)
                {
                    string repUser = context.Departments.Find(deptCode).rep_user;
                    UserModel repUserModel = new UserModel(repUser);

                    repUsers.Add(repUserModel);
                }

                /* Email logic */
                string fromEmail = currentUserModel.Email;
                string fromName = currentUserModel.Fullname;
                //UserModel deptHead = currentUserModel.FindDelegateOrDeptHead();

                foreach (var repUserModel in repUsers)
                {
                    string toEmail = repUserModel.Email;
                    string toName = repUserModel.Fullname;

                    string subject = string.Format("New disbursement for collection");
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Dear " + toName + ",");
                    sb.AppendLine("<br />");
                    sb.AppendLine("<br />");
                    sb.AppendLine(string.Format("{0} has scheduled some items for collection.", fromName));
                    sb.AppendLine("<br />");
                    Collection_Point collectionPt = repUserModel.Department.Collection_Point1;
                    sb.AppendLine(string.Format("The collecton point is: {0}, on {1} at {2}.", collectionPt.location, collectionPt.day_of_week, collectionPt.date_time.ToShortTimeString()));
                    sb.AppendLine("<br />");
                    sb.AppendLine(string.Format("Please <a href=\"{0}\">follow this link to login to the system</a>.", "https://rebrand.ly/ssis-login"));
                    sb.AppendLine("<br />");
                    sb.AppendLine("<br />");
                    sb.AppendLine("Thank you.");
                    sb.AppendLine("<br />");
                    sb.AppendLine("<br />");
                    sb.AppendLine("<i>This message was auto-generated by the Stationery Store Inventory System.</i>");

                    string body = sb.ToString();

                    new Emailer(fromEmail, fromName).SendEmail(toEmail, toName, subject, body);
                }
            }
            /* End of email logic */
        }
    }

    class GenerateDisbursementViewModel : RetrievalFormViewModel
    {
        private int collectionPtId;

        public int CollectionPtId
        {
            get
            {
                return collectionPtId;
            }

            set
            {
                collectionPtId = value;
            }
        }
    }
}