//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BashBook.DAL.EDM
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ground
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ground()
        {
            this.Matches = new HashSet<Match>();
        }
    
        public int GroundId { get; set; }
        public int CountryId { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public Nullable<int> Capacity { get; set; }
    
        public virtual LookUpValue LookUpValue { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Match> Matches { get; set; }
    }
}
