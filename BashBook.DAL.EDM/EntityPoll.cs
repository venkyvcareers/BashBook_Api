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
    
    public partial class EntityPoll
    {
        public int EntityPollId { get; set; }
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
        public int PollId { get; set; }
    
        public virtual LookUpValue LookUpValue { get; set; }
        public virtual Poll Poll { get; set; }
    }
}
