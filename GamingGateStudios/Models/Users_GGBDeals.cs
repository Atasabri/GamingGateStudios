//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GamingGateStudios.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users_GGBDeals
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public int GGBDeal_ID { get; set; }
        public bool Active { get; set; }
        public int Pay { get; set; }
    
        public virtual GGBDeal GGBDeal { get; set; }
        public virtual User User { get; set; }
    }
}
