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
    
    public partial class QuestionRule
    {
        public int QuestionRuleId { get; set; }
        public int QuestionId { get; set; }
        public int Priority { get; set; }
        public string Text { get; set; }
    
        public virtual Question Question { get; set; }
    }
}
