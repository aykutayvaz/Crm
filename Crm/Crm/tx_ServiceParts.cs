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
    
    public partial class tx_ServiceParts
    {
        public int ServiceId { get; set; }
        public int PartId { get; set; }
    
        public virtual dt_Part dt_Part { get; set; }
    }
}
