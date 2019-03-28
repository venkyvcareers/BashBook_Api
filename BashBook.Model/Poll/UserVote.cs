using System.Collections.Generic;
using BashBook.Model.Group;
using BashBook.Model.User;

namespace BashBook.Model.Poll
{
    public class UserVoteModel
    {
        public int UserVoteId { get; set; }
        public int UserId { get; set; }
        public int PollId { get; set; }
        public List<int> Options { get; set; }
    }

    public class UserPollOptionModel
    {
        public int UserId { get; set; }
        public int PollId { get; set; }
        public int OptionId { get; set; }
    }

    public class UserVoteInfoModel
    {
        public int UserId { get; set; }
        public int OptionId { get; set; }
        public long VotedOn { get; set; }
    }

    public class VoteResultModel
    {
        public PollModel Poll { get; set; }
        public List<OptionResponseModel> Options { get; set; }
        public int TotalUserCount { get; set; }
    }

    public class VoteUserInfoModel
    {
        public UserPreviewModel UserInfo { get; set; }
        public long VotedOn { get; set; }
        public List<int> Options { get; set; }
    }

    public class GroupVoteRequestModel
    {
        public GroupPreviewModel Group { get; set; }
        public PollModel Poll { get; set; }
        public string OptionSelected { get; set; }
        public bool IsVoted { get; set; }
        public long? VotedOn { get; set; }
    }
}
