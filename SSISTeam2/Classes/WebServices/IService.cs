using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using SSISTeam2.Classes.Models;

namespace SSISTeam2.Classes.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {

        //Operations by Htein Lin Aung 

        [OperationContract]
        [WebGet(UriTemplate = "/DeliveryOrder", ResponseFormat = WebMessageFormat.Json)]
        List<int> GetDeliveryOrderId();

        [OperationContract]
        [WebGet(UriTemplate = "/Approve/{id}", ResponseFormat = WebMessageFormat.Json)]
        void Approve(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/Reject/{id}", ResponseFormat = WebMessageFormat.Json)]
        void Reject(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/UpdateRequestDetail/{id}/{qty}", ResponseFormat =WebMessageFormat.Json)]
        void UpdateRequestDetail(string id,string qty);

        [OperationContract]
        [WebGet(UriTemplate = "/DeleteRequestDetail/{id}", ResponseFormat = WebMessageFormat.Json)]
        void DeleteRequestDetail(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/DeliveryOrder/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<Delivery_Details> GetDeliveryOrdersDetails(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/PendingRequest/{dept}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Request> GetAllRequest(string dept);

        [OperationContract]
        [WebGet(UriTemplate = "/GetRequestByDeptCode/{dept}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Request> GetRequestByDeptCode(string dept);

        [OperationContract]
        [WebGet(UriTemplate = "/GetRequestByUserName/{dept}/{user}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Request> GetRequestByUserName(string dept,string user);

        [OperationContract]
        [WebGet(UriTemplate = "/RequestDetail/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_RequestDetail> GetRequestDetail(string id);



        [OperationContract]
        [WebGet(UriTemplate = "/Category/", ResponseFormat = WebMessageFormat.Json)]
        List<string> GetCatName();

        [OperationContract]
        [WebGet(UriTemplate = "/Category/{name}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Item> GetItemDetail(string name);

        [OperationContract]
        [WebGet(UriTemplate = "/login/{name}/{pass}", ResponseFormat = WebMessageFormat.Json)]
        WCF_User login(string name, string pass);

        [OperationContract]
        [WebGet(UriTemplate = "/EmployeeList/{deptcode}", ResponseFormat = WebMessageFormat.Json)]
        string[] GetDelgateEmployeeName(string deptcode);

        [OperationContract]
        [WebInvoke(UriTemplate = "/Create", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        void Create(WCF_AppDuties Approval_Duties);

        //Service Contract by Heng Tiong

        [OperationContract]
        [WebGet(UriTemplate = "/InventoryCheck/", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_MonthlyCheck> GetIMonthlyCheckModel();

        [OperationContract]
        [WebGet(UriTemplate = "/InventoryCheckName", ResponseFormat = WebMessageFormat.Json)]
        List<string> GetMonthlyCheckName();

        [OperationContract]
        [WebInvoke(UriTemplate = "/InventoryCheck/Update/{username}", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        //void UpdateMonthlyCheck(List<WCF_MonthlyCheck> monthlyCheckList, string username);
        void UpdateMonthlyCheck(List<WCF_MonthlyCheck> monthlyChecks, string username);

        [OperationContract]
        [WebInvoke(UriTemplate = "/FileDiscrepancies/Update/{username}", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        void UpdateFileDiscrepancies(List<WCF_FileDiscrepancy> fileDiscrepancies, string username);


        [OperationContract]
        [WebGet(UriTemplate = "/CheckApprovalDuties/{deptcode}", ResponseFormat = WebMessageFormat.Json)]
        WCF_AppDuties CheckAppDuties(string deptcode);

        [OperationContract]
        [WebInvoke(UriTemplate = "/Update", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        string Update(WCF_AppDuties app);

        //By Yin
        [OperationContract]
        [WebGet(UriTemplate = "/RetriveTQty/{user}", ResponseFormat = WebMessageFormat.Json)]
        List<WCFRetieve> GetEachItemQty(string user);

        [OperationContract]
        [WebInvoke(UriTemplate = "/RetriveTQty/Update/{loginUserName}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void UpdateRetrieveQty(List<WCFRetieve> retrievedList,string loginUserName);

        [OperationContract]
        [WebGet(UriTemplate = "/DisbCollectP", ResponseFormat = WebMessageFormat.Json)]
        List<String> GetDisbCollectP();

        [OperationContract]
        [WebGet(UriTemplate = "/DisbCollectDept/{cpid}", ResponseFormat = WebMessageFormat.Json)]
        List<String> GetDisbCollectDept(string cpid);

        [OperationContract]
        [WebGet(UriTemplate = "/Department/{deptName}", ResponseFormat = WebMessageFormat.Json)]
        WCFDepartment GetDeptNamebycode(string deptName);

        [OperationContract]
        [WebGet(UriTemplate = "/DisbDeptDetail/{user}/{deptCode}", ResponseFormat = WebMessageFormat.Json)]
        List<WCFDisburse> GetDeptDetail(string user, string deptCode);

        [OperationContract]
        [WebInvoke(UriTemplate = "/DisburseTQty/Update/{loginUserName}/{deptCode}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void UpdateDisburseQty(string loginUserName, string deptCode, List<WCFDisburse> disburseList);

        [OperationContract]
        [WebGet(UriTemplate = "/ViewAllAdjustment/{role}", ResponseFormat = WebMessageFormat.Json)]
        List<WCFInventoryAdjustmentModel> GetAllAdjustmentList(string role);

        [OperationContract]
        [WebGet(UriTemplate = "/ViewAllAdjustmentDetail/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<WCFInventoryAdjustmentDetailModel> AdjustmentDetailList(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/UpdateInventoryAdjustment", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        void UpdateInventoryAdj(String voucherId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/DeleteInventoryAdjustment", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        void DeleteInventoryAdj(String voucherId);


        //Htein Lin Aung Create New Request

        [OperationContract]
        [WebInvoke(UriTemplate = "/CreateNewRequest", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        void ApplyNewRequest(WCF_NewReqeust req);

        [OperationContract]
        [WebInvoke(UriTemplate = "/CreateRequestDetail", Method = "POST",
       RequestFormat = WebMessageFormat.Json,
       ResponseFormat = WebMessageFormat.Json)]
        void CreateRequestDetail(WCFItemTotalQty req);

        [OperationContract]
        [WebGet(UriTemplate = "/GetWorkingPartner/{name}/{deptName}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Partner> GetWorkingPartner(string name, string deptName);

    }

    [DataContract]
    public class WCF_MonthlyCheck
    {
        [DataMember]
        public string itemCode;
        [DataMember]
        public string itemDescription;
        [DataMember]
        public string categoryName;
        [DataMember]
        public string currentQuantity;
        [DataMember]
        public string actualQuantity;
        [DataMember]
        public string reason;

        public WCF_MonthlyCheck(string itemCode, string itemDescription, string categoryName, string currentQuantity, string actualQuantity, string reason)
        {
            this.itemCode = itemCode;
            this.itemDescription = itemDescription;
            this.categoryName = categoryName;
            this.currentQuantity = currentQuantity;
            this.actualQuantity = actualQuantity;
            this.reason = reason;
        }

        [DataMember]
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        [DataMember]
        public string ItemDescription
        {
            get { return itemDescription; }
            set { itemDescription = value; }
        }
        [DataMember]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        [DataMember]
        public string CurrentQuantity
        {
            get { return currentQuantity; }
            set { currentQuantity = value; }
        }
        [DataMember]
        public string ActualQuantity
        {
            get { return actualQuantity; }
            set { actualQuantity = value; }
        }
        [DataMember]
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }
    }

    [DataContract]
    public class WCF_FileDiscrepancy
    {
        [DataMember]
        public string itemCode;
        [DataMember]
        public string itemName;
        [DataMember]
        public string retrievedQty;
        [DataMember]
        public string disbursedQty;
        [DataMember]
        public string adjustedQty;
        [DataMember]
        public string reason;


        public WCF_FileDiscrepancy(string itemCode, string itemName, string retrievedQty, string disbursedQty,string adjustedQty, string reason)
        {
            this.itemCode = itemCode;
            this.itemName = itemName;
            this.retrievedQty = retrievedQty;
            this.disbursedQty = disbursedQty;
            this.adjustedQty = adjustedQty;
            this.reason = reason;
        }

        [DataMember]
        string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        [DataMember]
        string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        [DataMember]
        string RetrievedQty
        {
            get { return retrievedQty; }
            set { retrievedQty = value; }
        }

        [DataMember]
        string DisbursedQty
        {
            get { return disbursedQty; }
            set { disbursedQty = value; }
        }

        [DataMember]
        string AdjustedQty
        {
            get { return adjustedQty; }
            set { adjustedQty = value; }
        }

        [DataMember]
        string Reason
        {
            get { return reason; }
            set { reason = value; }
        }
    }


    [DataContract]
    public class WCF_Item
    {
        [DataMember]
        public string itemDesc;

        [DataMember]
        public string itemCode;

        [DataMember]
        public string uom;

        [DataMember]
        public int catId;



        public WCF_Item(string itemDesc, string itemCode, string uom, int catId)
        {
            this.itemDesc = itemDesc;
            this.itemCode = itemCode;
            this.uom = uom;
            this.catId = catId;

        }

    }

    [DataContract]
    public class WCF_Request
    {
        [DataMember]
        string user;
        [DataMember]
        int req_id;
        [DataMember]
        string requestdate;
        [DataMember]
        string reason;
        [DataMember]
        string status;

        public WCF_Request(string user, int req_id, string requestdate, string reason,string status)
        {
            this.user = user;
            this.req_id = req_id;

            this.requestdate = requestdate;
            this.reason = reason;
            this.status = status;
        }

    }


    [DataContract]
    public class WCF_User
    {
        [DataMember]
        string dept_code;
        [DataMember]
        string user_name;
        [DataMember]
        string role;
        [DataMember]
        string flag;
        [DataMember]
        string mobile_num;

        public WCF_User(string dept_code, string user_name, string role, string flag, string mobile_num)
        {
            this.dept_code = dept_code;
            this.user_name = user_name;
            this.role = role;
            this.flag = flag;
            this.mobile_num = mobile_num;
        }




    }

    [DataContract]
    public class WCF_AppDuties
    {

        public string username;
         string startDate;
         string endDate;
         string deptCode;
         string createdDate;
         string deleted;
         string reason;

        public WCF_AppDuties(string username, string startDate, string endDate, string deptCode, string createdDate,  string deleted,string reason)
        {
            this.username = username;
            this.startDate = startDate;
            this.endDate = endDate;
            this.deptCode = deptCode;
            this.createdDate = createdDate;
            this.deleted = deleted;
            this.reason = reason;

        }

        [DataMember]
        public string UserName
        {
            get { return username; }
            set { username = value; }
        }

        [DataMember]
        public string StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        [DataMember]
        public string EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }


        [DataMember]
        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }


        [DataMember]
        public string CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        [DataMember]
        public string Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }


        [DataMember]
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }


    }

    [DataContract]
    public class WCF_RequestDetail
    {
        [DataMember]
        string itemdesc;
        [DataMember]
        int quantity;
        [DataMember]
        int req_detail_id;




        public WCF_RequestDetail(string itemdesc, int quantity,int req_detail_id)
        {
            this.itemdesc = itemdesc;
            this.quantity = quantity;
            this.req_detail_id = req_detail_id;
        }

        public String Itemdesc
        {
            get { return itemdesc; }
            set { itemdesc = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }






    }

    //By Yin
    [DataContract]
    public class WCFRetieve
    {
        [DataMember]
        private string itemDes;
        [DataMember]
        private string totalQty;
        [DataMember]
        private string retrieveQty;
        [DataMember]
        private string itemCode;

        public WCFRetieve() : this("", "","","")
        {

        }
        public WCFRetieve(string itemDes, string totalQty, string retrieveQty, string itemCode)
        {
            this.itemDes = itemDes;
            this.totalQty = totalQty;
            this.retrieveQty = retrieveQty;
            this.itemCode = itemCode;
        }

        public string ItemDes
        {
            get
            {
                return itemDes;
            }

            set
            {
                itemDes = value;
            }
        }

        public string TotalQty
        {
            get
            {
                return totalQty;
            }

            set
            {
                totalQty = value;
            }
        }
        public string RetrieveQty
        {
            get
            {
                return retrieveQty;
            }

            set
            {
                retrieveQty = value;
            }
        }

        public string ItemCode
        {
            get
            {
                return itemCode;
            }

            set
            {
                itemCode = value;
            }
        }


    }

    [DataContract]
    public class WCF_NewReqeust
    {
        [DataMember]
        string user;
        [DataMember]
        string dept_code;
        [DataMember]
        string reason;
        [DataMember]
        string status;
        [DataMember]
        string date_time;




        public WCF_NewReqeust(string user, string dept_code, string reason, string status, string date_time)
        {
            this.user = user;
            this.dept_code = dept_code;
            this.reason = reason;
            this.status = status;
            this.date_time = date_time;
        }

        [DataMember]
        public string Name
        {
            get { return user; }
            set { user = value; }
        }


        [DataMember]
        public string DeptCode
        {
            get { return dept_code; }
            set { dept_code = value; }
        }

        [DataMember]
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }

        [DataMember]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        [DataMember]
        public string Date
        {
            get { return date_time; }
            set { date_time = value; }
        }

        
    }



    }

[DataContract]
public class WCFDisburse
{
    [DataMember]
    string itemName;
    [DataMember]
    string retrievedQty;
    [DataMember]
    string disbursedQty;
    [DataMember]
    string itemCode;


    public WCFDisburse() : this("", "", "","")
    {

    }
    public WCFDisburse(string itemName, string retrievedQty, string disbursedQty, string itemCode)
    {
        this.itemName = itemName;
        this.retrievedQty = retrievedQty;
        this.disbursedQty = disbursedQty;
        this.itemCode = itemCode;
    }


    public string ItemName
    {
        get
        {
            return itemName;
        }

        set
        {
            itemName = value;
        }
    }

    public string RetrievedQty
    {
        get
        {
            return retrievedQty;
        }

        set
        {
            retrievedQty = value;
        }
    }


    public string DisbursedQty
    {
        get
        {
            return disbursedQty;
        }
        set
        {
            disbursedQty = value;
        }
    }
    public string ItemCode
    {
        get
        {
            return itemCode;
        }

        set
        {
            itemCode = value;
        }
    }
}
    [DataContract]
    public class WCFInventoryAdjustmentModel
    {

        public string voucherID;
        public string clerk;
        public string date;
        public string status;
        public string highestCost;


        public WCFInventoryAdjustmentModel(string voucherID, string clerk, string date, string status, string highestCost)
        {
            
            this.voucherID = voucherID;
            this.clerk = clerk;
            this.date = date;
            this.status = status;
            this.highestCost = highestCost;
            

        }
        [DataMember]
        public string VoucherID
        {
            get { return voucherID; }
            set { voucherID = value; }
        }
        [DataMember]
        public string Clerk
        {
            get { return clerk; }
            set { clerk = value; }
        }
        [DataMember]
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        [DataMember]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        [DataMember]
        public string HighestCost
        {
            get { return highestCost; }
            set { highestCost = value; }
        }      
    }

[DataContract]
public class WCFInventoryAdjustmentDetailModel
{

    public string itemdesc;
    public string qtyadjust;
    public string priceadjust;
    public string reason;


    public WCFInventoryAdjustmentDetailModel(string itemdesc, string qtyadjust, string priceadjust, string reason)
    {

        this.itemdesc = itemdesc;
        this.qtyadjust = qtyadjust;
        this.priceadjust = priceadjust;
        this.reason = reason;


    }
    [DataMember]
    public string ItemDesc
    {
        get { return itemdesc; }
        set { itemdesc = value; }
    }
    [DataMember]
    public string QtyAdjust
    {
        get { return qtyadjust; }
        set { qtyadjust = value; }
    }
    [DataMember]
    public string PriceAdjust
    {
        get { return priceadjust; }
        set { priceadjust = value; }
    }
    [DataMember]
    public string Reason
    {
        get { return reason; }
        set { reason = value; }
    }
}
    //Yin
    [DataContract]
    public class WCFDepartment
    {

        private string deptCode;
        private string repUser;
        private string contactNumber;

        public WCFDepartment() : this("", "", "")
        {

        }

    public WCFDepartment(string deptCode, string repUser, string contactNumber)
        {

            this.deptCode = deptCode;
            this.repUser = repUser;
            this.contactNumber = contactNumber;

        }
        [DataMember]
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
        [DataMember]
        public string RepUser
        {
            get
            {
                return repUser;
            }

            set
            {
                repUser = value;
            }
        }
        [DataMember]
        public string ContactNumber
        {
            get
            {
                return contactNumber;
            }

            set
            {
                contactNumber = value;
            }
        }


    }

[DataContract]
public class WCFItemTotalQty
{
    [DataMember]
    string itemDes;
    [DataMember]
    string totalQty;

    public WCFItemTotalQty() : this("", "")
    {

    }
    public WCFItemTotalQty(string itemDes, string totalQty)
    {
        this.itemDes = itemDes;
        this.totalQty = totalQty;
    }

    [DataMember]
    public string ItemDes
    {
        get
        {
            return itemDes;
        }

        set
        {
            itemDes = value;
        }
    }

    [DataMember]
    public string TotalQty
    {
        get
        {
            return totalQty;
        }

        set
        {
            totalQty = value;
        }
    }
}

[DataContract]
public class WCF_Partner
{
    [DataMember]
    private string user;
    [DataMember]
    private string ph_no;
    [DataMember]
    private string role;
    [DataMember]
    private string email;


    public WCF_Partner(string user, string ph_no,string role,string email)
    {

        this.user = user;
        this.ph_no = ph_no;
        this.role = role;
        this.email = email;


    }
}


