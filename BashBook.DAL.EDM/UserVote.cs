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
    
    public partial class UserVote
    {
        public int UserVoteId { get; set; }
        public int UserId { get; set; }
        public int PollId { get; set; }
        public int OptionId { get; set; }
        public Nullable<long> VotedOn { get; set; }
    
        public virtual Option Option { get; set; }
        public virtual Poll Poll { get; set; }
        public virtual User User { get; set; }
    }
}