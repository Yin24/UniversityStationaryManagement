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
    
    public partial class Adjustment_Details
    {
        public int voucher_detail_id { get; set; }
        public string item_code { get; set; }
        public int quantity_adjusted { get; set; }
        public string reason { get; set; }
        public int voucher_id { get; set; }
        public string deleted { get; set; }
    
        public virtual Inventory_Adjustment Inventory_Adjustment { get; set; }
        public virtual Stock_Inventory Stock_Inventory { get; set; }
    }
}
