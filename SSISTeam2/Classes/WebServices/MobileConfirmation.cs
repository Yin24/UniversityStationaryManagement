using SSISTeam2.Classes.EFFacades;
using SSISTeam2.Classes.Models;
using SSISTeam2.Views.StoreClerk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSISTeam2.Classes.WebServices
{
    public class MobileConfirmation
    {
        public static List<WCFRetieve> GetAllPossibleRetrievalsForUser(string currentUser)
        {



            List<WCFRetieve> wcfList = new List<WCFRetieve>();

            using (SSISEntities context = new SSISEntities())
            {
                //Move all from allocated to retrieving automatically
                var allocated = FacadeFactory.getAllocatedService(context).getAllAllocated();
                if (allocated.Count > 0)
                {
                    foreach (var allocModel in allocated)
                    {
                        List<string> items = allocModel.Items.Select(s => s.Key.ItemCode).ToList();

                        FacadeFactory.getRequestMovementService(context).moveFromAllocatedToRetrieving(allocModel.RequestId, items, currentUser);
                    }
                }

                context.SaveChanges();

                var retrieving = FacadeFactory.getRetrievalService(context).getAllRetrievingByClerk(currentUser);

                var itemGroups = retrieving.SelectMany(sm =>
                    sm.Items
                    .Select(s => new { s.Key.ItemCode, s.Key.Description, Quantity = s.Value, sm.Department.dept_code, sm.RequestId, sm.Department.name })
                ).GroupBy(k => k.ItemCode, v => v).ToList();


                foreach (var itemGroup in itemGroups)
                {

                    int itemQty = itemGroup.Select(s => s.Quantity).Aggregate((a, b) => a + b);
                    //List<int> reqIds = itemGroup.Select(s => s.RequestId).ToList();
                    WCFRetieve wcfItem = new WCFRetieve();

                    wcfItem.ItemDes = itemGroup.First().Description;
                    wcfItem.TotalQty = itemQty.ToString();
                    wcfItem.ItemCode = itemGroup.Key;

                    wcfList.Add(wcfItem);
                }
            }

            return wcfList;
        }

        public static List<WCFDisburse> getAllPossibleSignOffsForUserForDept(string currentUser, string currentDeptCode)
        {
            List<WCFDisburse> wcfList = new List<WCFDisburse>();

            using (SSISEntities context = new SSISEntities())
            {
                DisbursementModelCollection disbursingList = FacadeFactory.getDisbursementService(context).getAllThatCanBeSignedOff(currentUser);

                var itemGroups = disbursingList.SelectMany(sm =>
                    sm.Items
                    .Select(s => new { s.Key.ItemCode, s.Key.Description, Quantity = s.Value, sm.Department.dept_code, sm.RequestId, sm.Department.name })
                ).GroupBy(k => new { k.ItemCode, k.Description, DeptCode = k.dept_code }, v => v)
                .ToList();

                foreach (var itemGroup in itemGroups)
                {
                    //chnage dept code into dept name

                    // If the dept code is not correct, just SKIP
                    if (itemGroup.Key.DeptCode != currentDeptCode) continue;

                    int itemQty = itemGroup.Select(s => s.Quantity).Aggregate((a, b) => a + b);

                    WCFDisburse wcfItem = new WCFDisburse();
                    wcfItem.ItemName = itemGroup.Key.Description;
                    wcfItem.RetrievedQty = itemQty.ToString();
                    wcfItem.ItemCode = itemGroup.Key.ItemCode;

                    wcfList.Add(wcfItem);
                }
            }

            return wcfList;
        }

        public static bool SignOffDisbursement(string currentUser, string currentDeptCode, Dictionary<string, int> itemCodeAndQuantities)
        {
            using (SSISEntities context = new SSISEntities())
            {
                // Get all the models:
                DisbursementModelCollection disbursingList = FacadeFactory.getDisbursementService(context).getAllThatCanBeSignedOff(currentUser);

                var itemGroups = disbursingList.SelectMany(sm =>
                    sm.Items
                    .Select(s => new { s.Key.ItemCode, s.Key.Description, Quantity = s.Value, sm.Department.dept_code, sm.RequestId, sm.Department.name })
                ).GroupBy(k => new { k.ItemCode, k.Description, DeptCode = k.dept_code }, v => v)
                .ToList();

                List<ConfirmDisbursementViewModel> modelDisbursingList = new List<ConfirmDisbursementViewModel>();

                // For each item, create a view model
                foreach (var itemGroup in itemGroups)
                {
                    // If the dept code is not correct, just SKIP
                    if (itemGroup.Key.DeptCode != currentDeptCode) continue;

                    int itemQty = itemGroup.Select(s => s.Quantity).Aggregate((a, b) => a + b);
                    List<int> reqIds = itemGroup.Select(s => s.RequestId).ToList();

                    int actualQty = itemCodeAndQuantities[itemGroup.Key.ItemCode];

                    ConfirmDisbursementViewModel model = new ConfirmDisbursementViewModel();
                    model.ItemCode = itemGroup.Key.ItemCode;
                    model.ItemDescription = itemGroup.Key.Description;
                    model.DeptCode = itemGroup.Key.DeptCode;
                    model.QuantityExpected = itemQty;
                    model.QuantityActual = actualQty;
                    model.RequestIds = reqIds;

                    modelDisbursingList.Add(model);
                }

                // Make sure all item codes were provided
                List<string> itemCodes = modelDisbursingList.Select(s => s.ItemCode).ToList();

                if (false == itemCodes.TrueForAll(deptCode => itemCodeAndQuantities.ContainsKey(deptCode)))
                {
                    // If provided dictionary does not have all the item codes, return false and stop
                    return false;
                }

                // Now do filtering:
                List<ConfirmDisbursementViewModel> okayItems = new List<ConfirmDisbursementViewModel>();
                Dictionary<ConfirmDisbursementViewModel, int> notOkayItems = new Dictionary<ConfirmDisbursementViewModel, int>();


                // Sort modelList into okay and not okay items 
                foreach (ConfirmDisbursementViewModel model in modelDisbursingList)
                {
                    int expectedQty = model.QuantityExpected;
                    int actualQty = model.QuantityActual;

                    if (actualQty > expectedQty)
                    {
                        throw new ArgumentOutOfRangeException("Actual quantity cannot be more than expected quantity! For item: " + model.ItemCode);
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

                            FacadeFactory.getRequestMovementService(context).moveFromDisbursingToDisbursed(idAndItemCode.Key, itemCodeAndQty, currentUser);
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
                    FacadeFactory.getRequestMovementService(context).moveFromDisbursingToDisbursed(grouping.Key, items, currentUser);
                }

                context.SaveChanges();

            } // Disposal of SSISEntities context
            return true;
        }



        public static bool ConfirmRetrievalFromWarehouse(string currentUser, Dictionary<string, int> itemCodeAndQuantities)
        {
            using (SSISEntities context = new SSISEntities())
            {
                var allocated = FacadeFactory.getRetrievalService(context).getAllRetrievingByClerk(currentUser);

                var itemGroups = allocated.SelectMany(sm =>
                    sm.Items
                    .Select(s => new { s.Key.ItemCode, s.Key.Description, Quantity = s.Value, sm.Department.dept_code, sm.RequestId, sm.Department.name })
                ).GroupBy(k => k.ItemCode, v => v).ToList();

                List<ConfirmRetrievalViewModel> list = new List<ConfirmRetrievalViewModel>();

                foreach (var itemGroup in itemGroups)
                {

                    int itemQty = itemGroup.Select(s => s.Quantity).Aggregate((a, b) => a + b);
                    List<int> reqIds = itemGroup.Select(s => s.RequestId).ToList();

                    int actualQty = itemCodeAndQuantities[itemGroup.Key];

                    ConfirmRetrievalViewModel model = new ConfirmRetrievalViewModel();
                    model.ItemCode = itemGroup.Key;
                    model.ItemDescription = itemGroup.First().Description;
                    model.QuantityExpected = itemQty;
                    model.QuantityActual = actualQty;
                    model.RequestIds = reqIds;

                    list.Add(model);
                }

                // Check if all item codes are present
                List<string> itemCodes = allocated.SelectMany(sm => sm.Items.Select(s => s.Key.ItemCode)).ToList();

                if (false == itemCodes.TrueForAll(code => itemCodeAndQuantities.ContainsKey(code)))
                {
                    // Some item codes are missing
                    return false;
                }


                // Do sorting

                List<ConfirmRetrievalViewModel> okayItems = new List<ConfirmRetrievalViewModel>();
                Dictionary<ConfirmRetrievalViewModel, int> notOkayItems = new Dictionary<ConfirmRetrievalViewModel, int>();

                // For each gvr, check if actual == expected
                foreach (ConfirmRetrievalViewModel model in list)
                {
                    int expectedQty = model.QuantityExpected;
                    int actualQty = model.QuantityActual;

                    if (actualQty > expectedQty)
                    {
                        throw new ArgumentOutOfRangeException("Actual quantity cannot be more than expected quantity! For item: " + model.ItemCode);
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

                            FacadeFactory.getRequestMovementService(context).moveFromRetrievingToRetrieved(idAndItemCode.Key, itemCodeAndQty, currentUser);
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
                    FacadeFactory.getRequestMovementService(context).moveFromRetrievingToRetrieved(grouping.Key, items, currentUser);
                }

                context.SaveChanges();

            } // Disposal of context
            return true;
        }

    }
}