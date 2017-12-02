using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSISTeam2.Classes.Models
{
    public class InventoryAdjustmentModel
    {
        private int voucherID;
        private string clerk;
        private DateTime date;
        private string status;
        private DateTime statusDate;
        private string deleted;
        private List<AdjustmentModel> adjModel;
        private double highestCost;

        public InventoryAdjustmentModel(Inventory_Adjustment inventoryAdjustment)
        {
            this.voucherID = inventoryAdjustment.voucher_id;
            this.clerk = inventoryAdjustment.clerk_user;
            this.date = inventoryAdjustment.date;
            this.status = inventoryAdjustment.status;
            this.statusDate = inventoryAdjustment.status_date;
            this.deleted = inventoryAdjustment.deleted;
            this.adjModel = GetAdjustmentModel(inventoryAdjustment.Adjustment_Details.ToList());
            this.highestCost = GetHighestCost();
        }
        public List<AdjustmentModel> GetAdjustmentModel(List<Adjustment_Details> detailList)
        {
            List<AdjustmentModel> modelList = new List<AdjustmentModel>();
            foreach (Adjustment_Details i in detailList)
            {
                AdjustmentModel model = new AdjustmentModel(i);
                modelList.Add(model);
            }
            return modelList;
        }
        public double GetHighestCost()
        {
            List<double> allCost = new List<double>();
            foreach (AdjustmentModel i in this.adjModel)
            {
                allCost.Add(i.Price);
            }

            return allCost.Max();
        }
        public List<AdjustmentModel> AdjModel
        {
            get
            {
                return adjModel;
            }
            set
            {
                adjModel = value;
            }
        }
        public string Clerk
        {
            get
            {
                return clerk;
            }
            set
            {
                clerk = value;
            }
        }
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }
        public double HighestCost
        {
            get
            {
                return highestCost;
            }
            set
            {
                highestCost = value;
            }
        }

        public int VoucherID
        {
            get
            {
                return voucherID;
            }
            set
            {
                voucherID = value;
            }
        }
    }
}