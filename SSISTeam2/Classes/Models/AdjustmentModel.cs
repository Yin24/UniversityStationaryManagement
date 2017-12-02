using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSISTeam2.Classes.Models
{
    public class AdjustmentModel
    {
        private int voucherDetailID;
        private ItemModel inventory;
        private int quantity;
        private string quantityAdjusted;
        private double cost;
        private string costAdjusted;
        private string reason;
        private Inventory_Adjustment inventoryAdjustment;
        private string catName;

        public AdjustmentModel(Adjustment_Details detail)
        { 
            this.voucherDetailID = detail.voucher_detail_id;
            this.inventory = GetItemModel(detail.item_code);
            this.quantity = detail.quantity_adjusted;
            this.quantityAdjusted = GetQuantityString();
            this.reason = detail.reason;
            this.inventoryAdjustment = detail.Inventory_Adjustment;
            this.cost = GetPrice();
            this.costAdjusted = string.Format("{0:C}", cost);
            this.catName = inventory.CatName;
        }
        public ItemModel GetItemModel(string itemCode)
        {
            SSISEntities context = new SSISEntities();
            Stock_Inventory iventory = context.Stock_Inventory.Where(x => x.item_code == itemCode).ToList().First();
            ItemModel model = new ItemModel(iventory);
            return model;
        }
        public double GetPrice()
        {
             double price = this.inventory.AveragePrice * Math.Abs(this.quantity);
             return price;
        }
        public string GetQuantityString()
        {
            if (this.quantity <= 0)
            {
                string qtyAdj = string.Format("Lost {0}", Math.Abs(this.quantity));
                return qtyAdj;
            } else if (this.quantity > 0)
            {
                string qtyAdj = string.Format("Found {0}", Math.Abs(this.quantity));
                return qtyAdj;
            }
            return null;
        }
        public bool Above250()
        {
            if (this.cost > 250)
            {
                return true;
            }
            else
                return false;
        }
        public int VoucherDetailID
        {
            get
            {
                return voucherDetailID;
            }
            set
            {
                value = voucherDetailID;
            }
        }
        public ItemModel Inventory
        {
            get
            {
                return inventory;
            }
            set
            {
                value = inventory;
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
        public string QuantityAdjusted
        {
            get
            {
                return quantityAdjusted;
            }
            set
            {
                quantityAdjusted = value;
            }
        }
        public double Price
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
            }
        }
        public string PriceAdjusted
        {
            get
            {
                return costAdjusted;
            }
            set
            {
                costAdjusted = value;
            }
        }
        public string Reason
        {
            get
            {
                return reason;
            }
            set
            {
                reason = value;
            }
        }
        public Inventory_Adjustment InventoryAdjustment
        {
            get
            {
                return inventoryAdjustment;
            }
            set
            {
                inventoryAdjustment = value;
            }
        }
        public string CatName
        {
            get
            {
                return catName;
            }
            set
            {
                catName = value;
            }
        }

        public string CostAdjusted
        {
            get
            {
                return costAdjusted;
            }
            set
            {
                costAdjusted = value;
            }
        }
    }
}