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
    
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            this.PostComments = new HashSet<PostComment>();
            this.PostLikes = new HashSet<PostLike>();
            this.PostShares = new HashSet<PostShare>();
            this.PostStatInfoes = new HashSet<PostStatInfo>();
        }
    
        public int PostId { get; set; }
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
        public int ContentTypeId { get; set; }
        public Nullable<int> PostStatusId { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }
        public long PostedOn { get; set; }
        public int PostedBy { get; set; }
    
        public virtual LookUpValue LookUpValue { get; set; }
        public virtual LookUpValue LookUpValue1 { get; set; }
        public virtual LookUpValue LookUpValue2 { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostComment> PostComments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostLike> PostLikes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostShare> PostShares { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostStatInfo> PostStatInfoes { get; set; }
    }
}