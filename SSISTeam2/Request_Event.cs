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
    
    public partial class Request_Event
    {
        public int request_event_id { get; set; }
        public int request_detail_id { get; set; }
        public string status { get; set; }
        public int quantity { get; set; }
        public System.DateTime date_time { get; set; }
        public string deleted { get; set; }
        public string username { get; set; }
        public Nullable<int> allocated { get; set; }
        public Nullable<int> not_allocated { get; set; }
    
        public virtual Dept_Registry Dept_Registry { get; set; }
        public virtual Request_Details Request_Details { get; set; }
    }
}