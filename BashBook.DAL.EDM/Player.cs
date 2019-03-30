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
    
    public partial class Player
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Player()
        {
            this.Teams = new HashSet<Team>();
            this.TeamPlayers = new HashSet<TeamPlayer>();
        }
    
        public int PlayerId { get; set; }
        public int CountryId { get; set; }
        public int PlayerRoleId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    
        public virtual LookUpValue LookUpValue { get; set; }
        public virtual LookUpValue LookUpValue1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Team> Teams { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamPlayer> TeamPlayers { get; set; }
    }
}