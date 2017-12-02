using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using SSISTeam2.Classes.Models;
using System.Web.Security;

namespace SSISTeam2
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public UserModel userModel;

        protected void Page_Init(object sender, EventArgs e)
        {
            bool isPersistent = false;

            if (Page.User.Identity.IsAuthenticated && Page.User.Identity.Name != null && Page.User.Identity.Name != "")
            {

                //var formsAuthCookie = Response.Cookies[FormsAuthentication.FormsCookieName];
                //if (formsAuthCookie != null)
                //{
                //    var existingTicket = FormsAuthentication.Decrypt(formsAuthCookie.Value);

                //    if (existingTicket != null)
                //    {
                //        isPersistent = existingTicket.IsPersistent;
                //    }
                //}

                // Somebody is signed in
                string normalisedUserName = Page.User.Identity.Name.ToLower();
                string storedUserName = normalisedUserName;
                using (SSISEntities context = new SSISEntities())
                {
                    var users = context.Dept_Registry.Where(w => w.username.ToLower() == normalisedUserName);
                    if (users.Count() > 0)
                    {
                        var user = users.First();
                        storedUserName = user.username;
                    }
                }

                if (Page.User.Identity.Name != storedUserName)
                {
                    FormsAuthentication.SetAuthCookie(storedUserName, isPersistent);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (Request.IsAuthenticated)
            {
                string currentUser = Page.User.Identity.Name;

                string fullName = "";
                using (SSISEntities context = new SSISEntities())
                {
                    try
                    {
                        fullName = context.Dept_Registry.Find(currentUser).fullname;
                    } catch (Exception)
                    {
                        FormsAuthentication.SignOut();
                        Response.Redirect("~/login.aspx");
                    }

                    // Check if user is department rep
                    //int count = ctx.Departments.Where(d => d.rep_user == currentUser).Count();
                    //if (count > 0)
                    //{
                    //    // is a dept rep
                    //    linkBtnDepRepView.Visible = true;
                    //    //btnDepRepViewDisbursements.Visible = true;
                    //} else
                    //{
                    //    linkBtnDepRepView.Visible = false;
                    //    //btnDepRepViewDisbursements.Visible = false;
                    //}
                }

                lblFullName.Text = "Welcome, "+fullName;

                userModel = new UserModel(Page.User.Identity.Name);

            }
            

        }

        protected void Register(object sender, EventArgs e)
        {
            Response.Redirect("~/register.aspx");
        }
        protected void Login(object sender, EventArgs e)
        {
            Response.Redirect("~/login.aspx");
        }

        protected void Logout(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session["tender"] = null;
            Session["item"] = null;
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }

        protected void Default(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void LowStock(object sender, EventArgs e)
        {
            
            Response.Redirect("~/Views/StoreClerk/lowstock.aspx");
        }

        protected void MakeOrder(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/Cart.aspx");
        }

        protected void GenerateRetrieval(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/GenerateRetrieval.aspx");
        }

        protected void MakeNewRequest(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/MakeNewRequest.aspx");
        }

        protected void ViewRequestHistory(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/EmpRequestHistory.aspx");
        }

        //protected void ViewPendingReq(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/Views/Employee/EmpRequestHistory.aspx");
        //}

        protected void ApproveBtn(object sender, EventArgs e)
        {           

            Response.Redirect("~/Views/DepartmentHead/ViewPending.aspx");

            
            //Delegate  (access department-head'all pages except "DelegateAuthority.aspx")
            //try
            //{
            //    string loginUserame = Page.User.Identity.Name;
            //    UserModel loginUser = new UserModel(loginUserame);
            //    UserModel delegat = loginUser.FIndDelegateHead();

            //    if (loginUserame == delegat.Username)
            //    {

            //        Response.Redirect("~/Views/DepartmentHead/ViewPending.aspx");
            //    }

            //    else
            //    {
            //        Response.Redirect("~/Default.aspx");
            //    }

            //}
            //catch
            //{
            //    Response.Redirect("~/Default.aspx");
            //}

        }

        protected void DelegateAuth(object sender, EventArgs e)
        {
            //Yin

                Response.Redirect("~/Views/DepartmentHead/DelegateAuthority.aspx");


        }
        protected void ChangeCPRep(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/DepartmentHead/ChangeCollectionnRep.aspx");
        }

        protected void genReport(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Reporting/ReportsMain.aspx");
        }
        protected void ViewCatalogue_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/ViewCatalogue.aspx");
        }

        protected void btnDepRepViewDisbursements_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/RepViewDisbursements.aspx");
        }

        protected void btnEmpDash_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/EmpDashboard.aspx");
        }

        protected void btnClerkDash_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/Dashboard.aspx");
        }

        protected void btnNewRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/MakeNewRequest.aspx");
        }

        protected void btnEmpRequestHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/EmpRequestHistory.aspx");
        }

        protected void btnEmpViewCatalogue_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/ViewCatalogue.aspx");
        }

        protected void btnGenerateRetrieval_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/GenerateRetrieval.aspx");
        }

        protected void btnStoreCart_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/Cart.aspx");
        }

        protected void btnHeadDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/DepartmentHead/HeadDashboard.aspx");
        }

        protected void btnApproveRequests_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/DepartmentHead/ViewPending.aspx");
        }

        protected void btnGenerateReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Reporting/ReportsMain.aspx");
        }

        protected void btnMakeOrder_Mgr_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/MakeOrder.aspx");
        }

        protected void btnLowStocks_Mgr_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/lowstock.aspx");
        }

        protected void btnGenerateDisbursement_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/GenerateDisbursement.aspx");
        }

        protected void btn_ViewPendingOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/ViewPendingOrder.aspx");
        }

        protected void btnMonthlyCheck_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/MonthlyCheck.aspx");
        }

        protected void btnViewAllAdjustments_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreManager/ViewAllAdjustment.aspx");
        }

        protected void ViewDeptList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/ViewDepartmentList.aspx");
        }

        protected void btnViewTenderList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/TenderListForm.aspx");
        }

        protected void btnChnageCollectp_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/ChangeCollectionPoint.aspx");
        }

        protected void btnConfirmDisbursement_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/ConfirmDisbursement.aspx");
        }

        protected void btnViewSuppList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/ViewSupplierList.aspx");
        }

        protected void btnViewCatList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/ChangeCategoryName1.aspx");
        }

        protected void btnMaintainCatalogue_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/ViewAndEditCatalogue.aspx");
        }

        protected void btnConfirmRetrieval_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/ConfirmRetrieval.aspx");
        }

        protected void btnViewDisbursement_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/ViewGeneratedDisbursements.aspx");
        }
    }
}