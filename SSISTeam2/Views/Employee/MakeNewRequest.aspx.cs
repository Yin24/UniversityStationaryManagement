using SSISTeam2.Classes;
using SSISTeam2.Classes.EFFacades;
using SSISTeam2.Classes.Exceptions;
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
    public partial class MakeNewRequest : System.Web.UI.Page
    {
        const string SESSION_CATEGORIES = "newRequest_Categories";
        const string SESSION_STOCKS = "newRequest_Stocks";
        const string SESSION_APPROVED_REQS = "newRequest_ApprovedRequests";
        const string SESSION_MODELS = "newRequest_SessionModels";
        const string SESSION_USER_MODEL = "newRequest_CurrentUser";
        const string SESSION_REQ_EDIT_ID = "newRequest_RequestEditId";
        const string SESSION_IS_EDITING = "newRequest_RequestIsEditing";

        const string TEMP_DEPT_CODE = "REGR";

        //bool isEditing = false;

        protected override PageStatePersister PageStatePersister
        {
            get
            {
                return new SessionPageStatePersister(Page);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { // New entry into page

                // Check that user is not a d head or dlg head or whatever
                UserModel userModel = new UserModel(User.Identity.Name);
                if (userModel.isDelegateHead() || userModel.isDeptHead() || userModel.isStoreManager())
                {
                    Response.Redirect("~/Views/notauthorized.aspx");
                }

                int requestId = 0;
                // Get any request string
                string requestToEdit = Request.QueryString["edit"];
                int.TryParse(requestToEdit, out requestId); // 0 if fails

                //if (requestId > 0)
                //{
                //    lblPageTitle.Text = "Update Request (Id " + requestId + ")";
                //    isEditing = true;
                //}

                Session[SESSION_IS_EDITING] = false;

                List<MakeNewRequestModel> models = new List<MakeNewRequestModel>();
                using (SSISEntities context = new SSISEntities())
                {
                    List<Category> cats = context.Categories.Where(w => w.deleted != "Y").ToList();
                    Session[SESSION_CATEGORIES] = cats;
                    List<Stock_Inventory> stocks = context.Stock_Inventory.Where(w => w.deleted != "Y").ToList();
                    Session[SESSION_STOCKS] = stocks;
                    RequestModelCollection requests;
                    /*
                    if (!User.Identity.IsAuthenticated)
                    {
                        Response.Redirect("~/login.aspx?return=Views/StoreClerk/MakeNewRequest.aspx");
                    }*/

                    UserModel currentUser = new UserModel(User.Identity.Name);
                    //UserModel currentUser = new UserModel("Sally");
                    try
                    {
                        string deptCode = currentUser.Department.dept_code;
                        requests = FacadeFactory.getRequestService(context).getAllApprovedRequests()
                        .fromDepartment(deptCode);
                    }
                    catch (ItemNotFoundException)
                    {
                        requests = null;
                    }

                    Session[SESSION_USER_MODEL] = currentUser;
                    Session[SESSION_APPROVED_REQS] = requests;

                    if (requestId == 0)
                    { // Making a new request
                        MakeNewRequestModel model = _makeNewModel(0);
                        models.Add(model);

                        panelCannotChange.Visible = false;
                        panelNormalBtns.Visible = true;
                        btnCancelRequest.Visible = false;
                    }
                    else
                    {
                        Request found = context.Requests.Find(requestId);
                        // Set to cannot update first
                        panelCannotChange.Visible = true;
                        panelNormalBtns.Visible = false;
                        string reason = "";
                        if (found == null)
                        {
                            reason = "That request could not be found.";
                        } else if (found.username != currentUser.Username)
                        {
                            reason = "You did not make this request.";
                        } else if (found.current_status != RequestStatus.PENDING && found.current_status != RequestStatus.UPDATED)
                        {
                            // status is neither pending nor updated
                            switch (found.current_status)
                            {
                                case RequestStatus.CANCELLED:
                                    reason = "The request was cancelled.";
                                    break;
                                case RequestStatus.REJECTED:
                                    reason = "The request was already rejected.";
                                    break;
                                default: // Approved or others
                                    reason = "The request was already approved.";
                                    break;
                            }
                        } else
                        {
                            // Request can be updated
                            panelCannotChange.Visible = false;
                            panelNormalBtns.Visible = true;
                            btnCancelRequest.Visible = true;

                            RequestModel rModel = FacadeFactory.getRequestService(context).findRequestById(requestId);

                            tbReason.Text = rModel.Reason;

                            int numIter = 0;
                            foreach (var item in rModel.Items)
                            {
                                if (item.Value == 0)
                                    continue;
                                MakeNewRequestModel model = _makeNewModel(numIter, item.Key.Category.cat_id, item.Key.ItemCode, item.Value);
                                models.Add(model);
                                numIter++;
                            }

                            lblPageTitle.Text = "Update Request (Id " + requestId + ")";

                            //isEditing = true;
                            Session[SESSION_IS_EDITING] = true;
                            Session[SESSION_REQ_EDIT_ID] = requestId;
                            btnSubmit.Text = "Save Changes";
                        }

                        lblCannotChangeInfo.Text = reason;
                    }
                    
                }

                Session[SESSION_MODELS] = models;
                _refreshGrid(models);
            } // end of isPostBack
            
        }

        protected void GridViewRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label num = (e.Row.FindControl("NumLabel") as Label);
                int numInt = Convert.ToInt32(num.Text);
                DropDownList ddlCats = (e.Row.FindControl("ddlCategories") as DropDownList);
                ListBox lbDescs = (e.Row.FindControl("lbDescriptions") as ListBox);
                ListBox lbPrevApp = (e.Row.FindControl("lbPrevApproved") as ListBox);
                TextBox tbQuantity = (e.Row.FindControl("tbQuantity") as TextBox);

                List<MakeNewRequestModel> models = _getModelsFromSession();
                MakeNewRequestModel model = models[numInt - 1];
                
                ddlCats.DataSource = model.DictCats;
                ddlCats.SelectedValue = model.CurrentCategory.ToString();
                ddlCats.DataValueField = "Key";
                ddlCats.DataTextField = "Value";
                ddlCats.DataBind();

                lbDescs.DataSource = model.DictDescriptions;
                lbDescs.SelectedValue = model.CurrentItem;
                lbDescs.DataValueField = "Key";
                lbDescs.DataTextField = "Value";
                lbDescs.DataBind();

                lbPrevApp.DataSource = model.Approved;
                lbPrevApp.DataBind();

                tbQuantity.Text = model.Quantity.ToString();
            }
        }

        protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            _checkQtyRows();

            DropDownList ddl = sender as DropDownList;
            GridViewRow gvr = ddl.Parent.Parent as GridViewRow;
            Label num = gvr.FindControl("NumLabel") as Label;

            string selected = ddl.SelectedValue;

            int numInt = Convert.ToInt32(num.Text);
            List<MakeNewRequestModel> models = _getModelsFromSession();
            MakeNewRequestModel model = models[numInt - 1];

            model.CurrentCategory = Convert.ToInt32(selected);

            model = _updateViewModelCategory(model, null);

            Session[SESSION_MODELS] = models;

            _refreshGrid(models);
        }

        protected void lbDescriptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            _checkQtyRows();

            ListBox lb = sender as ListBox;
            GridViewRow gvr = lb.Parent.Parent as GridViewRow;
            Label num = gvr.FindControl("NumLabel") as Label;

            string selected = lb.SelectedValue;

            int numInt = Convert.ToInt32(num.Text);
            List<MakeNewRequestModel> models = _getModelsFromSession();
            MakeNewRequestModel model = models[numInt - 1];

            model.CurrentItem = selected;

            model = _updateViewModelItem(model);

            Session[SESSION_MODELS] = models;

            _refreshGrid(models);
        }

        protected void tbQuantity_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            GridViewRow gvr = tb.Parent.Parent as GridViewRow;
            Label num = gvr.FindControl("NumLabel") as Label;

            string text = tb.Text;

            int numInt = Convert.ToInt32(num.Text);
            List<MakeNewRequestModel> models = _getModelsFromSession();
            MakeNewRequestModel model = models[numInt - 1];

            int qty = 0;
            if (int.TryParse(text, out qty) == false)
            { // Couldn't convert
                qty = model.Quantity;
            }

            model.Quantity = qty;

            Session[SESSION_MODELS] = models;

            _refreshGrid(models);
        }

        protected void btnNewRow_Click(object sender, EventArgs e)
        {
            _checkQtyRows();

            List<MakeNewRequestModel> models = _getModelsFromSession();
            MakeNewRequestModel newModel = _makeNewModel(models.Count);
            models.Add(newModel);

            if (models.Count == 10)
            {
                btnNewRow.Enabled = false;
            }

            Session[SESSION_MODELS] = models;

            _refreshGrid(models);
        }

        private void _checkQtyRows()
        {
            foreach (GridViewRow row in GridViewRequest.Rows)
            {
                Label rowNum = row.FindControl("NumLabel") as Label;
                TextBox tb = row.FindControl("tbQuantity") as TextBox;

                string text = tb.Text;

                int rowNumInt = Convert.ToInt32(rowNum.Text);
                List<MakeNewRequestModel> rowModels = _getModelsFromSession();
                try
                {
                    MakeNewRequestModel model = rowModels[rowNumInt - 1];
                    int qty = 0;
                    if (int.TryParse(text, out qty) == false)
                    { // Couldn't convert
                        qty = model.Quantity;
                    }

                    model.Quantity = qty;

                    Session[SESSION_MODELS] = rowModels;

                    //_refreshGrid(rowModels);
                } catch(Exception)
                {

                }

            }
        }

        protected void btnRemoveRow_Click(object sender, EventArgs e)
        {
            _checkQtyRows();

            // Original
            Button btn = sender as Button;
            GridViewRow gvr = btn.Parent.Parent as GridViewRow;
            Label num = gvr.FindControl("NumLabel") as Label;

            int numInt = Convert.ToInt32(num.Text);
            List<MakeNewRequestModel> models = _getModelsFromSession();
            //MakeNewRequestModel model = models[numInt - 1];

            if (models.Count == 10)
            {
                btnNewRow.Enabled = true;
            }

            try
            {
                models.RemoveAt(numInt - 1);

                // Reset Num numbers
                int i = 1;
                foreach(var model in models)
                {
                    model.Num = i;
                    i++;
                }

                Session[SESSION_MODELS] = models;

                _refreshGrid(models);
            } catch (Exception)
            {

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            _checkQtyRows();

            UserModel currentUserModel;

            List<MakeNewRequestModel> models = _getModelsFromSession();
            using (SSISEntities context = new SSISEntities())
            {
                currentUserModel = new UserModel(User.Identity.Name);

                Dictionary<string, int> items = new Dictionary<string, int>();
                foreach (var model in models)
                {
                    // If the user didn't add a quantity, just SKIP
                    if (model.Quantity == 0) continue;

                    if (items.ContainsKey(model.CurrentItem))
                    {
                        int qty = items[model.CurrentItem];
                        items.Remove(model.CurrentItem);

                        items.Add(model.CurrentItem, model.Quantity + qty);
                    } else
                    {
                        items.Add(model.CurrentItem, model.Quantity);
                    }
                }

                Dictionary<ItemModel, int> itemModels = new Dictionary<ItemModel, int>();
                foreach (var item in items)
                {
                    Stock_Inventory stock = context.Stock_Inventory.Find(item.Key);
                    ItemModel im = new ItemModel(stock);
                    itemModels.Add(im, item.Value);
                }

                UserModel user = (UserModel) Session[SESSION_USER_MODEL];
                RequestModel newReq = new RequestModel();
                newReq.Items = itemModels;
                newReq.Reason = tbReason.Text;
                newReq.Department = user.Department;
                newReq.Status = RequestStatus.PENDING;
                newReq.UserModel = user;

                bool isEditing = (Session[SESSION_IS_EDITING] as bool?).Value;

                if (isEditing)
                {
                    newReq.RequestId = (int)Session[SESSION_REQ_EDIT_ID];
                    FacadeFactory.getRequestService(context).updateRequestChanges(newReq);
                } else
                {
                    FacadeFactory.getRequestService(context).saveNewRequest(newReq);
                }

                context.SaveChanges();
            }

            /* Email logic */
            string fromEmail = currentUserModel.Email;
            string fromName = currentUserModel.Fullname;
            UserModel deptHead = currentUserModel.FindDelegateOrDeptHead();
            string toEmail = deptHead.Email;
            string toName = deptHead.Fullname;

            string subject = string.Format("New pending request from {0}", fromName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Dear " + toName + ",");
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");
            sb.AppendLine(string.Format("{0} has requested for some items, pending your approval.", fromName));
            sb.AppendLine("<br />");
            sb.AppendLine(string.Format("The request's reason is: {0}", tbReason.Text));
            sb.AppendLine("<br />");
            sb.AppendLine(string.Format("Please <a href=\"{0}\">follow this link to view the request</a>.", "https://rebrand.ly/ssis-mgr-viewpending"));
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");
            sb.AppendLine("Thank you.");
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");
            sb.AppendLine("<i>This message was auto-generated by the Stationery Store Inventory System.</i>");

            string body = sb.ToString();

            new Emailer(fromEmail, fromName).SendEmail(toEmail, toName, subject, body);
            /* End of email logic */

            //Response.Redirect(Request.Url.ToString(), false);
            Response.Redirect("EmpRequestHistory.aspx", false);
        }

        protected void btnCancelRequest_Click(object sender, EventArgs e)
        {
            int requestId = (int) Session[SESSION_REQ_EDIT_ID];
            string username = User.Identity.Name;
            using (SSISEntities context = new SSISEntities())
            {
                FacadeFactory.getRequestService(context).setRequestToCancelled(requestId, username);
                context.SaveChanges();
            }

            Response.Redirect("EmpRequestHistory.aspx", false);
        }

        private MakeNewRequestModel _updateViewModelItem(MakeNewRequestModel model)
        {
            List<Stock_Inventory> stocks = Session[SESSION_STOCKS] as List<Stock_Inventory>;
            RequestModelCollection requests = Session[SESSION_APPROVED_REQS] == null ? null : Session[SESSION_APPROVED_REQS] as RequestModelCollection;

            if (model.CurrentItem == null)
            {
                model.UnitOfMeasure = "";
                model.Approved = new List<string>();

                return model; // EARLY RETURN
            }

            model.UnitOfMeasure = stocks.Where(w => w.item_code == model.CurrentItem).First().unit_of_measure;

            if (requests == null)
            {
                model.Approved = new List<string>();
            }
            else
            {
                model.Approved = requests
                    .SelectMany(s =>
                    {
                        string date = s.Date.ToShortDateString();
                        IEnumerable<string> its = s.Items
                        .Where(w => w.Key.ItemCode == model.CurrentItem)
                        .Select(ss =>
                        {
                            string result = string
                                .Format("({0}) {1} - {2} ({3})",
                                    ss.Key.Category.cat_name,
                                    ss.Key.Description,
                                    ss.Value,
                                    date
                                    );
                            //string res = "";
                            //res += ss.Key.Category.cat_name;
                            //res += " " + ss.Key.Description;
                            //res += " (" + date + ")";
                            return result;
                        });

                        return its;
                    }
                    ).ToList();
            }

            return model;
        }

        private MakeNewRequestModel _updateViewModelCategory(MakeNewRequestModel model, string itemCode)
        {
            List<Stock_Inventory> stocks = Session[SESSION_STOCKS] as List<Stock_Inventory>;

            model.DictDescriptions = stocks.Where(w => w.cat_id == model.CurrentCategory).ToDictionary(k => k.item_code, v => v.item_description);

            if (itemCode == null)
            {
                var stList = stocks.Where(w => w.cat_id == model.CurrentCategory);
                if (stList.Count() > 0)
                {
                    Stock_Inventory st = stocks.Where(w => w.cat_id == model.CurrentCategory).First();
                    model.CurrentItem = st.item_code;
                } else
                {
                    model.CurrentItem = null;
                }
            } else
            {
                model.CurrentItem = itemCode;
            }

            return _updateViewModelItem(model);
        }

        private void _refreshGrid(List<MakeNewRequestModel> models)
        {
            GridViewRequest.DataSource = models;
            GridViewRequest.DataBind();

            if (GridViewRequest.Rows.Count == 1)
            {
                (GridViewRequest.Rows[0].FindControl("btnRemoveRow") as Button).Visible = false;
            }
        }

        private MakeNewRequestModel _getRowModel(Label label)
        {
            int numInt = Convert.ToInt32(label.Text);
            List<MakeNewRequestModel> models = _getModelsFromSession();
            return models[numInt - 1];
        }

        private List<MakeNewRequestModel> _getModelsFromSession()
        {
            return Session[SESSION_MODELS] as List<MakeNewRequestModel>;
        }

        private MakeNewRequestModel _makeNewModel(int count)
        {
            return _makeNewModel(count, 0, null, 0);
        }

        private MakeNewRequestModel _makeNewModel(int count, int catId, string itemCode, int qty)
        {
            List<Category> cats = Session[SESSION_CATEGORIES] as List<Category>;
            List<Stock_Inventory> stocks = Session[SESSION_STOCKS] as List<Stock_Inventory>;
            RequestModelCollection requests = Session[SESSION_APPROVED_REQS] == null ? null : Session[SESSION_APPROVED_REQS] as RequestModelCollection;

            MakeNewRequestModel model = new MakeNewRequestModel();
            model.Num = count + 1;
            model.Quantity = qty;
            model.DictCats = cats.ToDictionary(k => k.cat_id.ToString(), v => v.cat_name);

            if (catId == 0)
            {
                model.CurrentCategory = cats.First().cat_id;
            } else
            {
                model.CurrentCategory = catId;
            }

            return _updateViewModelCategory(model, itemCode);
        }

    }
    [Serializable]
    class MakeNewRequestModel
    {
        private int num, quantity, currentCategory;
        private List<string> approved;
        private Dictionary<string, string> dictCats, dictDescriptions;
        private string unitOfMeasure, currentItem;
        //private string currentCatName, currentDescription;

        public int Num
        {
            get
            {
                return num;
            }

            set
            {
                num = value;
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }

        

        public List<string> Approved
        {
            get
            {
                return approved;
            }

            set
            {
                approved = value;
            }
        }

        public string UnitOfMeasure
        {
            get
            {
                return unitOfMeasure;
            }

            set
            {
                unitOfMeasure = value;
            }
        }

        public int CurrentCategory
        {
            get
            {
                return currentCategory;
            }

            set
            {
                currentCategory = value;
            }
        }

        public string CurrentItem
        {
            get
            {
                return currentItem;
            }

            set
            {
                currentItem = value;
            }
        }

        public Dictionary<string, string> DictCats
        {
            get
            {
                return dictCats;
            }

            set
            {
                dictCats = value;
            }
        }

        public Dictionary<string, string> DictDescriptions
        {
            get
            {
                return dictDescriptions;
            }

            set
            {
                dictDescriptions = value;
            }
        }
    }
}