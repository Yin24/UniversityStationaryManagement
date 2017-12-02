using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSISTeam2.Classes.Models
{
    public class MonthlyCheckModel
    {
        private string itemCode;
        private Category category;
        private string catName;
        private string description;
        private string unitOfMeasure;
        private string imagePath;
        //private int availableQuantity;
        private int currentQuantity;
        private int reorderQuantity;
        private int reorderLevel;
        private Dictionary<Supplier, double> prices;
        private int actualQuantity;
        private string reason;
        private double averagePrice;


        public MonthlyCheckModel(Stock_Inventory stock)
        {
            ItemModel item = new ItemModel(stock);

            this.category = item.Category;
            this.itemCode = item.ItemCode;
            this.catName = item.CatName;
            this.description = item.Description;
            this.unitOfMeasure = item.UnitOfMeasure;
            this.imagePath = item.ImagePath;
            this.currentQuantity = item.CurrentQuantity;
            this.reorderQuantity = item.ReorderQuantity;
            this.reorderLevel = item.ReorderLevel;
            this.prices = item.Prices;
            this.actualQuantity = item.CurrentQuantity;
            this.reason = "";
            this.averagePrice = AveragePrice;
        }

        //public MonthlyCheck(string itemCode, string description, string catName, string actualQuantity, string currentQuantity, string reason)
        //{
        //    this.itemCode = itemCode;
        //    this.description = description;
        //    this.catName = catName;
        //}

        public static List<ItemModel> ConvertToItemModel(List<MonthlyCheckModel> monthlyList)
        {
            return null;
        }
        public Dictionary<Supplier, double> Prices
        {
            get
            {
                return prices;
            }

            set
            {
                prices = value;
            }
        }
        public List<Supplier> Suppliers
        {
            get
            {
                return prices.Keys.ToList();
            }
        }

        public double AveragePrice
        {
            get
            {
                return prices.Values.Average();
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

        internal Category Category
        {
            get
            {
                return category;
            }

            set
            {
                category = value;
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

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
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

        public string ImagePath
        {
            get
            {
                return imagePath;
            }

            set
            {
                imagePath = value;
            }
        }
        public int CurrentQuantity
        {
            get
            {
                return currentQuantity;
            }

            set
            {
                currentQuantity = value;
            }
        }

        public int ReorderQuantity
        {
            get
            {
                return reorderQuantity;
            }

            set
            {
                reorderQuantity = value;
            }
        }

        public int ReorderLevel
        {
            get
            {
                return reorderLevel;
            }

            set
            {
                reorderLevel = value;
            }
        }
        public int ActualQuantity
        {
            get
            {
                return actualQuantity;
            }

            set
            {
                actualQuantity = value;
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
    }
}
