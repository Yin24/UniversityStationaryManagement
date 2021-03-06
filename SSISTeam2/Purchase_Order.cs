//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSISTeam2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Purchase_Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Purchase_Order()
        {
            this.Delivery_Orders = new HashSet<Delivery_Orders>();
            this.Purchase_Order_Details = new HashSet<Purchase_Order_Details>();
        }
    
        public int order_id { get; set; }
        public string clerk_user { get; set; }
        public System.DateTime order_date { get; set; }
        public System.DateTime delivery_by { get; set; }
        public string deliver_address { get; set; }
        public string supplier_id { get; set; }
        public string deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Delivery_Orders> Delivery_Orders { get; set; }
        public virtual Dept_Registry Dept_Registry { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase_Order_Details> Purchase_Order_Details { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
