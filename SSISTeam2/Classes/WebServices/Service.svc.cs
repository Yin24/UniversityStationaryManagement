using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Security;

using SSISTeam2.Classes.Models;
using System.IO;
using static SSISTeam2.Classes.WebServices.Work;


namespace SSISTeam2.Classes.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        SSISEntities context = new SSISEntities();

        Work work = new Work();
        WCF_User user;

        List<string> IService.GetCatName()
        {
            return new Work().GetCatName();
        }

        List<int> IService.GetDeliveryOrderId()
        {

            return null;
        }

        List<Delivery_Details> IService.GetDeliveryOrdersDetails(string id)
        {
            return null;
        }

        List<WCF_Item> IService.GetItemDetail(string name)
        {

            List<WCF_Item> itemList = new List<WCF_Item>();

            List<Stock_Inventory> stock = new Work().GetItemDetail(name);

            foreach (Stock_Inventory s in stock)
            {
                WCF_Item item = new WCF_Item(s.item_description, s.item_code, s.unit_of_measure, s.cat_id);
                itemList.Add(item);
            }

            return itemList;

        }



        List<WCF_Request> IService.GetAllRequest(string dept)
        {
            List<WCF_Request> reqList = new List<WCF_Request>();
            List<Request> req = new Work().GetAllRequest(dept);

            foreach (Request r in req)
            {
                string date = string.Format("{0:dd/MM/yyy}", r.date_time);
                WCF_Request request = new WCF_Request(r.username, r.request_id, date, r.reason,r.current_status);
                reqList.Add(request);
            }

            return reqList;


        }

        //WCF_User IService.login(string name, string pass)
        //{
        //    WCF_User user;
        //    bool validate = Membership.ValidateUser(name, pass);

        //    if (validate)
        //    {

        //        string[] role = Roles.GetRolesForUser(name);
        //        Dept_Registry dept = new Work().login(name);
        //        user = new WCF_User(dept.dept_code, name, role[0]);
        //        return user;
        //    }
        //    else { return user = new WCF_User(null, "failed", null); }
        //}

        //WCF_User IService.login(string name, string pass)
        //{
        //    //WCF_User user;
        //    //String flag;String updflag;
        //      String updflag = "N";
        //    bool validate = Membership.ValidateUser(name, pass);

        //    if (validate)
        //    {
        //       // updflag = new Work().CheckApprovalDutiesStatus();
        //        //if (updflag.Equals("T"))
        //       // {
        //            UserModel usermodel = new UserModel(name);
        //            UserModel depthead = usermodel.FindDelegateOrDeptHead();
        //            if (usermodel.Role == "DeptHead")
        //            {
        //                user = new WCF_User(usermodel.Department.dept_code, usermodel.Username, usermodel.Role, "N");
        //            }
        //            else
        //            {
        //                if (usermodel.Username == depthead.Username)
        //                {
        //                    user = new WCF_User(depthead.Department.dept_code, depthead.Username, depthead.Role, "Y");
        //                }
        //                else
        //                {
        //                    user = new WCF_User(usermodel.Department.dept_code, usermodel.Username, usermodel.Role, "N");
        //                }
        //            }
        //       // }


        //        //string[] role = Roles.GetRolesForUser(name);
        //        //Dept_Registry dept = new Work().login(name);
        //        //user = new WCF_User(dept.dept_code, name, role[0]);
        //        //Comment delegate role
        //        //updflag = new Work().CheckApprovalDutiesStatus();
        //        //   if (updflag.Equals("T"))
        //        //   {
        //        //     flag = new Work().GetDepHeadRole(name);
        //        //     Dept_Registry dept = new Work().login(name);
        //        //     try
        //        //        {
        //        //            if (flag.Equals("Y"))
        //        //            {
        //        //                role[0] = "DeptHead";
        //        //                user = new WCF_User(dept.dept_code, name, role[0], flag);
        //        //            }
        //        //    }
        //        //    catch
        //        //        {
        //        //            flag = "N";

        //        //            user = new WCF_User(dept.dept_code, name, role[0], flag);
        //        //        }
        //        //    }

        //        return user;
        //    }

        WCF_User IService.login(string name, string pass)
        {
            //WCF_User user;
            String updflag;
            bool validate = Membership.ValidateUser(name, pass);

            if (validate)
            {
                
                UserModel usermodel = new UserModel(name);
                updflag = new Work().CheckApprovalDutiesStatus(usermodel.Department.dept_code);
               if (updflag.Equals("T"))
               {
                    //UserModel usermodel = new UserModel(name);
                    UserModel depthead = usermodel.FindDelegateOrDeptHead();
                    if (usermodel.Role == "DeptHead")
                    {
                        user = new WCF_User(usermodel.Department.dept_code, usermodel.Username, usermodel.Role, "N",usermodel.MobileNum);
                    }
                    else
                    {
                        if (usermodel.Username == depthead.Username)
                        {
                            user = new WCF_User(depthead.Department.dept_code, depthead.Username, depthead.Role, "Y", usermodel.MobileNum);
                        }
                        else
                        {
                            user = new WCF_User(usermodel.Department.dept_code, usermodel.Username, usermodel.Role, "N", usermodel.MobileNum);
                        }
                    }
                }
                    
                return user;
            }

            //    string[] role = Roles.GetRolesForUser(name);
            //    updflag = new Work().CheckApprovalDutiesStatus();
            //       if (updflag.Equals("T"))
            //       {
            //         flag = new Work().GetDepHeadRole(name);
            //         Dept_Registry dept = new Work().login(name);
            //         try
            //            {
            //                if (flag.Equals("Y"))
            //                {
            //                    role[0] = "DeptHead";
            //                    user = new WCF_User(dept.dept_code, name, role[0], flag);
            //                }
            //        }
            //        catch
            //            {
            //                flag = "N";

            //                user = new WCF_User(dept.dept_code, name, role[0], flag);
            //            }
            //        }
            //    return user;
            //}
            else { return user = new WCF_User(null, "failed", null, null,null); }
        }

        // Heng Tiong's MonthlyCheck implementation

        public List<WCF_MonthlyCheck> GetIMonthlyCheckModel()
        {
            List<MonthlyCheckModel> modelList = new Work().GetAllMonthlyCheck();

            List<WCF_MonthlyCheck> WCFList = new List<WCF_MonthlyCheck>();

            foreach (MonthlyCheckModel i in modelList)
            {

                string currentQuantity = i.CurrentQuantity.ToString();
                string actualQuantity = i.ActualQuantity.ToString();
                WCF_MonthlyCheck wcf = new WCF_MonthlyCheck(i.ItemCode, i.Description, i.CatName, currentQuantity, actualQuantity, i.Reason);

                WCFList.Add(wcf);
            }

            return WCFList;
        }

        public List<string> GetMonthlyCheckName()
        {
            List<MonthlyCheckModel> modelList = new Work().GetAllMonthlyCheck();
            List<string> strings = new List<string>();

            foreach (MonthlyCheckModel i in modelList)
            {

                string names = i.Description;
                strings.Add(names);
            }

            return strings;
        }

        public void UpdateMonthlyCheck(List<WCF_MonthlyCheck> monthlyChecks, string username)
        {
            //List<WCF_MonthlyCheck> confirmList = new List<WCF_MonthlyCheck>();

            //WCF_MonthlyCheck model = monthlyCheckList[0];

            //Adjustment_Details detail = new Adjustment_Details();
            //detail.item_code = model.ItemCode;
            //int adjusted = int.Parse(model.ActualQuantity) - int.Parse(model.CurrentQuantity);
            //detail.quantity_adjusted = adjusted;
            //detail.reason = model.Reason;
            //detail.deleted = "N";

            //Inventory_Adjustment inventory = new Inventory_Adjustment();
            //inventory.deleted = "N";
            //inventory.clerk_user = username;
            //inventory.status = "Pending";
            //inventory.date = DateTime.Today;
            //inventory.status_date = DateTime.Today;

            //inventory.Adjustment_Details.Add(detail);

            //SSISEntities context = new SSISEntities();
            //context.Adjustment_Details.Add(detail);
            //context.Inventory_Adjustment.Add(inventory);
            //context.SaveChanges();


            //foreach (WCF_MonthlyCheck i in monthlyCheckList)
            //{
            //    int actualQty = int.Parse(i.actualQuantity);
            //    int currentQty = int.Parse(i.currentQuantity);
            //    if (actualQty != currentQty)
            //    {
            //        confirmList.Add(i);
            //        discrepencyFound = true;
            //    }


            //}
            //this is the problem
            bool discrepencyFound = work.UpdateMonthlyCheck(monthlyChecks, username);
            //this is the problem
            work.UpdateMonthlyCheckRecord(username, discrepencyFound);
        }

        public void UpdateFileDiscrepancies(List<WCF_FileDiscrepancy> fileDiscrepancies, string username)
        {
            //for each itemDescription, get itemCode, get itemModel, get average price
            //i need itemCode, quantity adjusted, reason, cost of adjustment
            work.UpdateFileDiscrepancies(fileDiscrepancies, username);
        }

        public string[] GetDelgateEmployeeName(string deptcode)
        {

            return work.ListEmployeeName(deptcode).ToArray<String>();
        }

        public void Create(WCF_AppDuties dr)
        {

            string fullName = dr.username;
            string usrName = work.GetUserName(fullName);
            Approval_Duties appduties = new Approval_Duties
            {
                username = usrName,
                start_date = Convert.ToDateTime(dr.StartDate),
                end_date = Convert.ToDateTime(dr.EndDate),
                dept_code = dr.DeptCode,
                created_date = Convert.ToDateTime(dr.CreatedDate),
                deleted = dr.Deleted,
                reason = dr.Reason

            };


            work.CreateAppDuties(appduties);

        }

        public List<WCF_RequestDetail> GetRequestDetail(string id)
        {
            List<WCF_RequestDetail> rd = new List<WCF_RequestDetail>();

            List<Request_Details> rd_List = new Work().GetRequestDetail(id);
            foreach (Request_Details r in rd_List)
            {
                int quantity = Convert.ToInt32(r.orig_quantity);
                WCF_RequestDetail req = new WCF_RequestDetail(r.Stock_Inventory.item_description, quantity,r.request_detail_id);

                if (quantity == 0) continue;

                rd.Add(req);
            }

            return rd;
        }

        public WCF_AppDuties CheckAppDuties(string deptcode)
        {
            try
            {
                Approval_Duties c = work.ListAppDuties(deptcode);
                return new  WCF_AppDuties(c.username, c.start_date.ToString(), c.end_date.ToString(), c.dept_code, c.created_date.ToString(), c.deleted, c.reason);
            }
            catch
            {
                return null;
            }



            //return Work.ListAppDuties(deptcode).ToArray<String>();
        }

        public string Update(WCF_AppDuties c)
        {
            System.Diagnostics.Debug.WriteLine("Testingnnnnnnnn");
            /*Approval_Duties ap = new Approval_Duties
            {
                username = c.UserName,
                dept_code = c.DeptCode,
                deleted = "N"

            };*/
            work.UpdateDuty(c.DeptCode);
            return c.DeptCode;

        }

        public void Approve(string id)
        {
            new Work().Approve(id);
        }

        public void Reject(string id)
        {
            new Work().Reject(id);
        }

        //By Yin
 
        public List<WCFRetieve> GetEachItemQty(string user)
        {
            return work.wgetEachItemQty(user);
        }
        //Update Retrieve Form
        public void UpdateRetrieveQty(List<WCFRetieve> retrieveList, string loginUserName)
        {
            int ii = 0;
            string itemCode = null;
            string[] itemCodeAry = new string[retrieveList.Count];
            int[] qtyAry = new int[retrieveList.Count];
            Dictionary<string, int> dicList = new Dictionary<string, int>();

            foreach (WCFRetieve eachObj in retrieveList)
            {

                string itemName = eachObj.ItemDes;
                //itemCode = changeItemNametoCode(itemName);
                itemCode = eachObj.ItemCode;
                itemCodeAry[ii] = itemCode;

                int quantity = Int16.Parse(eachObj.RetrieveQty);
                qtyAry[ii] = quantity;

                //Add to dictionary
                dicList.Add(itemCodeAry[ii], qtyAry[ii]);

                ii++;
            }

            //Pass data to Mobile confirmation

            MobileConfirmation.ConfirmRetrievalFromWarehouse(loginUserName, dicList);
        }

        //chnage into Item Name to item COode
        public string changeItemNametoCode(string itemName)
        {

            Stock_Inventory st = context.Stock_Inventory.SingleOrDefault(x => x.item_description == itemName);
            string itemCode = st.item_code;
            return itemCode;

        }

        //Testing
        //public void UpdateRetrieveQty(List<WCFRetieve> retrieveList, string loginUserName)
        //{
        //    //int ii = 0;
        //    //Stock_Inventory st = null;
        //    //string itemCode = null;
        //    //string[] itemCodeAry = new string[retrieveList.Count];
        //    ////    //change item into item code
        //    //foreach (WCFRetieve eachObj in retrieveList)
        //    //{
        //    //    //Item
        //    //    string itemDescription = eachObj.ItemDes;
        //    //    st = context.Stock_Inventory.SingleOrDefault(x => x.item_description == itemDescription);
        //    //    itemCode = st.item_code;
        //    //    itemCodeAry[ii] = itemCode;
        //    //    ii++;
        //    //}

        //    Request rq = context.Requests.SingleOrDefault(x => x.request_id==38);
        //    rq.rejected_reason = "rrrr";
        //    rq.deleted = "R";
        //    context.Requests.Add(rq);
        //    context.SaveChanges();
        //}

        public List<String> GetDisbCollectP()
        {
            return work.wgetCollectP();

        }

        public List<string> GetDisbCollectDept(string cpid)
        {
            return work.wgetCollectDept(cpid);
        }

        public WCFDepartment GetDeptNamebycode(string deptName)
        {
            Department dept1 = context.Departments.Where(x => x.name == deptName).First();
            Dept_Registry dr = context.Dept_Registry.Where(x => x.username == dept1.rep_user).First();
            WCFDepartment wd = new WCFDepartment(dept1.dept_code, dr.fullname, dept1.contact_num);          
            return wd;
        }

        public List<WCFDisburse> GetDeptDetail(string user, string deptname)
        {

            return MobileConfirmation.getAllPossibleSignOffsForUserForDept(user, deptname);
        }

        //Update Disburse Form
        public void UpdateDisburseQty(string loginUserName, string deptcode, List<WCFDisburse> disburseList)
        {
            int ii = 0;
            string itemCode = null;
            string[] itemCodeAry = new string[disburseList.Count];
            int[] qtyAry = new int[disburseList.Count];
            Dictionary<string, int> dicList = new Dictionary<string, int>();

            foreach (WCFDisburse eachObj in disburseList)
            {

                string itemName = eachObj.ItemName;
                //itemCode = changeItemNametoCode(itemName);
                itemCode = eachObj.ItemCode;
                itemCodeAry[ii] = itemCode;

                int quantity = Int16.Parse(eachObj.DisbursedQty);
                qtyAry[ii] = quantity;

                //Add to dictionary
                dicList.Add(itemCodeAry[ii], qtyAry[ii]);

                ii++;
            }

            //Pass data to Mobile confirmation
            MobileConfirmation.SignOffDisbursement(loginUserName, deptcode, dicList);
        }

        //Testing
        //public void UpdateDisburseQty(string loginUserName, string deptcode, List<WCFDisburse> disburseList)
        //{
        //    int ii = 0;
        //    //Stock_Inventory st = null;
        //    //string itemCode = null;
        //    //string[] itemCodeAry = new string[disburseList.Count];

        //    //foreach (WCFDisburse eachObj in disburseList)
        //    //{
        //    //    //Item
        //    //    string itemDescription = eachObj.ItemName;
        //    //    //st = context.Stock_Inventory.SingleOrDefault(x => x.item_description == itemDescription);
        //    //    //itemCode = st.item_code;
        //    //    itemCode = eachObj.ItemCode;
        //    //    itemCodeAry[ii] = itemCode;
        //    //    ii++;
        //    //}

        //    Request rq = context.Requests.SingleOrDefault(x => x.reason == "testd");
        //    rq.rejected_reason = "zzzz";
        //    rq.deleted = "D";
        //    rq.current_status = deptcode;
        //    rq.username = loginUserName;
        //    context.Requests.Add(rq);
        //    context.SaveChanges();
        //}


        // Htein Lin Aung Apply new Request
        public void ApplyNewRequest(WCF_NewReqeust r)
        {
            Request req = new Request();
            req.username = r.Name;
            req.dept_code = r.DeptCode;
            req.reason = r.Reason;
            req.current_status = r.Status;
            req.date_time = Convert.ToDateTime(r.Date);
            req.deleted = "N";
            req.rejected = "N";

            new Work().ApplyNewRequest(req);
        }

        public List<WCFInventoryAdjustmentModel> GetAllAdjustmentList(string role)
        {

            List<WCFInventoryAdjustmentModel> rd = new List<WCFInventoryAdjustmentModel>();
            List<InventoryAdjustmentModel> invModelList = new List<InventoryAdjustmentModel>();

            List<Inventory_Adjustment> invAdjList = work.GetAdjustmentList();
            foreach (Inventory_Adjustment i in invAdjList)
            {
                InventoryAdjustmentModel s = new InventoryAdjustmentModel(i);
                if (role.Equals("DeptHead"))
                {
                    foreach (AdjustmentModel j in s.AdjModel)
                    {
                        if (j.Above250())
                        {
                            invModelList.Add(s);
                            break;
                        }
                    }
                }
                else if (role.Equals("Supervisor"))
                {
                    foreach (AdjustmentModel j in s.AdjModel)
                    {
                        if (!j.Above250())
                        {
                            invModelList.Add(s);
                            break;
                        }
                    }
                }

            }

            foreach (InventoryAdjustmentModel s in invModelList)
            {
                WCFInventoryAdjustmentModel item = new WCFInventoryAdjustmentModel(s.VoucherID.ToString(), s.Clerk, s.Status, s.Date.ToString(), s.HighestCost.ToString());
                rd.Add(item);
            }

            return rd;

        }

        public List<WCFInventoryAdjustmentDetailModel> AdjustmentDetailList(string id)
        {
            List<WCFInventoryAdjustmentDetailModel> rd = new List<WCFInventoryAdjustmentDetailModel>();
            List<Adjustment_Details> adjList = work.GetViewAdjustmentDetailList(id);

            List<AdjustmentModel> modelList = new List<AdjustmentModel>();
            foreach (Adjustment_Details i in adjList)
            {
                AdjustmentModel model = new AdjustmentModel(i);
                modelList.Add(model);
            }
            foreach (AdjustmentModel s in modelList)
            {
                WCFInventoryAdjustmentDetailModel item = new WCFInventoryAdjustmentDetailModel(s.CatName, s.QuantityAdjusted.ToString(), s.CostAdjusted.ToString(), s.Reason);
                rd.Add(item);
            }
            return rd;
        }

        public void UpdateInventoryAdj(String voucherId)
        {
            System.Diagnostics.Debug.WriteLine("Testingnnnnnnnn");
            work.updateAdjustment(voucherId);


        }

        public void CreateRequestDetail(WCFItemTotalQty req)
        {
            Request r = new Work().GetRequest();


            Request_Details rdetail = new Request_Details();
            rdetail.deleted = "N";
            rdetail.request_id = r.request_id;
            Stock_Inventory item = new Work().GetStockInventory(req.ItemDes);
            rdetail.item_code = item.item_code;
            rdetail.orig_quantity = Convert.ToInt32(req.TotalQty);

            new Work().CreateRequestDetail(rdetail);

            Request_Details newreq = new Work().GetLastRequestDetail();
            Request_Event revent = new Request_Event();
            revent.request_detail_id = newreq.request_detail_id;
            revent.status = RequestStatus.PENDING;
            revent.quantity = Convert.ToInt32(newreq.orig_quantity);
            revent.date_time = Convert.ToDateTime(r.date_time);
            revent.deleted = "N";
            revent.username = r.username;

            new Work().CreateRequestEvent(revent);
        }

        public void DeleteInventoryAdj(String voucherId)
        {
            System.Diagnostics.Debug.WriteLine("Testingnnnnnnnn");
            work.deleteAdjustment(voucherId);


        }

        public List<WCF_Request> GetRequestByDeptCode(string dept)
        {
            List<WCF_Request> reqList = new List<WCF_Request>();
            List<Request> req = new Work().GetAllRequestByDeptCode(dept);

            foreach (Request r in req)
            {
                string date = string.Format("{0:dd/MM/yyy}", r.date_time);
                WCF_Request request = new WCF_Request(r.username, r.request_id, date, r.reason, r.current_status);
                reqList.Add(request);
            }

            return reqList;
        }

        public List<WCF_Request> GetRequestByUserName(string dept, string user)
        {


            List<WCF_Request> reqList = new List<WCF_Request>();
            List<Request> req = new Work().GetAllRequestByUserName(dept,user);

            foreach (Request r in req)
            {
                string date = string.Format("{0:dd/MM/yyy}", r.date_time);
                WCF_Request request = new WCF_Request(r.username, r.request_id, date, r.reason, r.current_status);
                reqList.Add(request);
            }

            return reqList;
        }

        public void UpdateRequestDetail(string id, string qty)
        {
            new Work().UpdateRequestDetail(id, qty);
        }

        public void DeleteRequestDetail(string id)
        {
            new Work().DeleteRequestDetail(id);
        }

        public List<WCF_Partner> GetWorkingPartner(string name, string deptName)
        {
            List<WCF_Partner> pList = new List<WCF_Partner>();
           List<Dept_Registry> drList= new Work().GetWorkingPartner( name,  deptName);
            foreach (Dept_Registry dr in drList)
            {
                UserModel usermodel = new UserModel(dr.username);
                WCF_Partner partner = new WCF_Partner(dr.fullname, dr.mobile_no,usermodel.Role,usermodel.Email);
                pList.Add(partner);
            }
            return pList;
        }
    }
}