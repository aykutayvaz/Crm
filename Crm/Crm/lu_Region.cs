//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Crm
{
    using System;
    using System.Collections.Generic;
    
    public partial class lu_Region
    {
        public lu_Region()
        {
            this.dt_Service = new HashSet<dt_Service>();
        }
    
        public int RegionId { get; set; }
        public string RegionName { get; set; }
    
        public virtual ICollection<dt_Service> dt_Service { get; set; }
    }
}
