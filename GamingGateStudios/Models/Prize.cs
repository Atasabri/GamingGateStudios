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
    
    public partial class Prize
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prize()
        {
            this.Winners = new HashSet<Winner>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Name_AR { get; set; }
        public int Type { get; set; }
        public Nullable<int> Third_Partner { get; set; }
        public Nullable<double> Price { get; set; }
        public int Points { get; set; }
    
        public virtual Third_Partner Third_Partner1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Winner> Winners { get; set; }
    }
}