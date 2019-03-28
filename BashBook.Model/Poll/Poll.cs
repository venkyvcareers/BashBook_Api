using System.Collections.Generic;
using BashBook.Model.Global;

namespace BashBook.Model.Poll
{
    public class PollModel
    {
        public int PollId { get; set; }
        public int SelectionTypeId { get; set; }
        public int OptionTypeId { get; set; }
        public int? CategoryId { get; set; }
        public int? VisibilityId { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public bool IsVotingCompleted { get; set; }
        public int UserId { get; set; }
        public long CreatedOn { get; set; }
    }

    public class QuickPollModel
    {
        public int PollId { get; set; }
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
    }

    public class QuickPollViewModel
    {
        public int PollId { get; set; }
        public string Text { get; set; }
        public List<StringModel> Options { get; set; }
    }

    public class PollViewModel
    {
        public int PollId { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public long CreatedOn { get; set; }
    }

    public class PollWithOptionsModel
    {
        public PollModel Poll { get; set; }
        public List<OptionModel> Options { get; set; }
        public List<int> Groups { get; set; }
    }
    
}
