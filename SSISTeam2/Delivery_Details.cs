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
    
    public partial class Delivery_Details
    {
        public int delivery_details_id { get; set; }
        public int quantity { get; set; }
        public int delivery_id { get; set; }
        public int tender_id { get; set; }
        public string remarks { get; set; }
        public string deleted { get; set; }
    
        public virtual Delivery_Orders Delivery_Orders { get; set; }
        public virtual Tender_List_Details Tender_List_Details { get; set; }
    }
}
