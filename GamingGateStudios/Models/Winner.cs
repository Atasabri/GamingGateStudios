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
    
    public partial class Winner
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public int Prize_ID { get; set; }
        public int Gamer_ID { get; set; }
        public bool Received { get; set; }
    
        public virtual Gamer Gamer { get; set; }
        public virtual Prize Prize { get; set; }
        public virtual User User { get; set; }
    }
}
