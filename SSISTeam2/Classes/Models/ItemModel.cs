using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSISTeam2;

namespace SSISTeam2.Classes.Models
{
    public class ItemModel : IEquatable<ItemModel>
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

        public ItemModel() : this(null, "", "", "", 0, 0, 0)
        {
        }

        public ItemModel(Stock_Inventory stock)
        {
            itemCode = stock.item_code;
            description = stock.item_description;
            category = stock.Category;
            catName = category.cat_name;
            unitOfMeasure = stock.unit_of_measure;
            imagePath = stock.image_path;
            currentQuantity = stock.current_qty;
            reorderLevel = stock.reorder_level;
            reorderQuantity = stock.reorder_qty;
            //Changes for dashboard
            var stockItems = stock.Tender_List_Details.Where(w => w.deleted != "Y" && w.Tender_List.deleted != "Y" && w.Tender_List.tender_date.Year == DateTime.Now.Year);

            prices = new Dictionary<Supplier, double>();

            using (SSISEntities context = new SSISEntities())
            {
                // get tender list details associated with this item code

                IEnumerable<Tender_List_Details> details = context.Tender_List_Details
                    .Where(w => w.Tender_List.tender_date.Year == DateTime.Now.Year && w.Tender_List.deleted != "Y"
                    && w.item_code == itemCode && w.deleted != "Y")
                    // Get distinct
                    .GroupBy(d => d.Tender_List.supplier_id)
                    .Select(group => group.FirstOrDefault());

                Supplier sup = context.Suppliers.First();

                if (details.Count() == 0)
                {
                    // cannot find for whatever reason
                    prices.Add(sup, 0.0);
                } else
                {
                    // can find
                    foreach (var detail in details)
                    {
                        prices.Add(detail.Tender_List.Supplier, Convert.ToDouble(detail.price));
                    }
                }
            }


            //foreach (var stockItem in stockItems)
            //{
            //    if (! prices.ContainsKey(stockItem.Tender_List.Supplier))
            //    {
            //        prices.Add(stockItem.Tender_List.Supplier, Convert.ToDouble(stockItem.price));
            //    }
            //}

            //prices = stockItems.ToDictionary(x => x.Tender_List.Supplier, x => Convert.ToDouble(x.price));
            //prices = stock.Tender_List_Details.Where(w => w.deleted != "Y" && w.Tender_List.deleted != "Y" && w.Tender_List.tender_date.Year == DateTime.Now.Year).ToDictionary(x => x.Tender_List.Supplier, x => Convert.ToDouble(x.price));
        }

        public ItemModel(Category category,
                        string description,
                        string unitOfMeasure,
                        string imagePath,
                        //int availableQuantity,
                        int currentQuantity,
                        int reorderQuantity,
                        int reorderLevel)
        {
            if (category != null)
            {
                this.category = category;
                this.catName = category.cat_name;
            }
            this.description = description;
            this.unitOfMeasure = unitOfMeasure;
            this.imagePath = imagePath;
            //this.availableQuantity = availableQuantity;
            this.reorderQuantity = reorderQuantity;
            this.reorderLevel = reorderLevel;
        }

        private static int _getAvailableQuantity(string itemCode, int currentQuantity)
        {
            int cumulativeAvailable = currentQuantity;
            using (SSISEntities context = new SSISEntities())
            {
                // Get all RequestDetails for an item code
                List<Request_Details> details = context.Request_Details
                    .Where(w =>
                    w.item_code == itemCode
                    && w.deleted != "Y"
                    && (w.Request.current_status == RequestStatus.APPROVED
                    || w.Request.current_status == RequestStatus.PART_DISBURSED)
                    ).ToList();

                // For each of this item's details, get the stock it's occupying
                foreach (var detail in details)
                {
                    var eventItems = detail.Request_Event.Where(w => w.deleted != "Y" && w.status != EventStatus.DISBURSED);

                    // No matching
                    if (eventItems.Count() == 0)
                    {
                        continue;
                    }

                    Request_Event eventItem = eventItems.First();

                    // Just check allocated amount
                    int allocatedQty = eventItem.allocated.HasValue ? eventItem.allocated.Value : 0;

                    List<Request_Event> events = detail.Request_Event.OrderByDescending(o => o.date_time).ToList();
                    //int numAllocated = events.Where(w => w.status == EventStatus.ALLOCATED && w.deleted != "Y").Count();
                    int numAllocated = allocatedQty;

                    // For each detail, subtract its minusQty from the cumulative total
                    cumulativeAvailable -= numAllocated;
                }
            }

            return cumulativeAvailable;
        }

        public static int GetAvailableQtyFor(string itemCode, int currentQuantity)
        {
            return _getAvailableQuantity(itemCode, currentQuantity);
        }

        private int _getAvailableQuantityOld()
        {
            int cumulativeAvailable = currentQuantity;
            using (SSISEntities context = new SSISEntities())
            {
                // Get all RequestDetails for an item code
                List<Request_Details> details = context.Request_Details
                    .Where(w => 
                    w.item_code == itemCode 
                    && w.deleted != "Y"
                    && (w.Request.current_status == RequestStatus.APPROVED
                    || w.Request.current_status == RequestStatus.PART_DISBURSED)
                    ).ToList();

                // For each of this item's details, get the stock it's occupying
                foreach (var detail in details)
                {
                    int minusQty = 0;
                    List<Request_Event> events = detail.Request_Event.OrderByDescending(o => o.date_time).ToList();
                    int numAllocated = events.Where(w => w.status == EventStatus.ALLOCATED && w.deleted != "Y").Count();

                    if (numAllocated == 0)
                    { // Never been allocated, can skip this request_detail
                        continue;
                    } else if (numAllocated == 1)
                    { // Only just allocated one, this is the one to subtract from
                        minusQty = events.Where(w => w.status == EventStatus.ALLOCATED && w.deleted != "Y").First().quantity;
                    } else
                    {
                        for (int i = 0; i < events.Count; i++)
                        {
                            Request_Event ev = events[i];

                            if (ev.status == EventStatus.APPROVED || ev.deleted == "Y")
                            { // Skip all Approved and deleted
                                continue;
                            }

                            if (ev.status == EventStatus.DISBURSED || ev.status == EventStatus.DISBURSING || ev.status == EventStatus.RETRIEVED)
                            { // Find the closest Allocated
                                int closestAllocQty = 0;
                                for (int j = i; j >= 0; j--)
                                {
                                    Request_Event ev2 = events[j];

                                    if (ev2.status == EventStatus.ALLOCATED)
                                    {
                                        closestAllocQty = ev2.quantity;
                                        break;
                                    }
                                }
                                // Set minus qty
                                minusQty = ev.quantity + closestAllocQty;
                                // Break out of this detail's events list
                                break;
                            } else if (ev.status == EventStatus.RETRIEVING)
                            {
                                minusQty = ev.quantity;
                            }
                        }
                    }
                    // For each detail, subtract its minusQty from the cumulative total
                    cumulativeAvailable -= minusQty;
                }
            }

            return cumulativeAvailable;
        }

        public bool Equals(ItemModel other)
        {
            return other != null && other.itemCode == itemCode;
        }
        public override int GetHashCode()
        {
            if (itemCode == null) return 0;
            return itemCode.GetHashCode();
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
                var values = prices.Values;
                if (values.Count == 0)
                {
                    return 0.0;
                } else
                {
                    return values.Average();
                }
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

        public int AvailableQuantity
        {
            get
            {
                return _getAvailableQuantity(itemCode, currentQuantity);
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
    }
}
