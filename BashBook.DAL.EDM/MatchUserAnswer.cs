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
    
    public partial class MatchUserAnswer
    {
        public int MatchUserAnswerId { get; set; }
        public int MatchQuestionId { get; set; }
        public int MatchId { get; set; }
        public int UserId { get; set; }
        public int Answer { get; set; }
    
        public virtual Match Match { get; set; }
        public virtual MatchQuestion MatchQuestion { get; set; }
        public virtual User User { get; set; }
    }
}
