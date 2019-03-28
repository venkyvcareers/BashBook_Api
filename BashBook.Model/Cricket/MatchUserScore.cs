using System.Collections.Generic;
using BashBook.Model.User;

namespace BashBook.Model.Cricket
{
    public class MatchUserScoreModel
    {
        public int MatchId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
    }

    public class MatchWinnerModel
    {
        public int MatchId { get; set; }
        public int MatchNumber { get; set; }
        public List<UserPreviewScoreModel> Users { get; set; }
    }

    public class MatchWinnerViewModel
    {
        public int MatchId { get; set; }
        public int MatchNumber { get; set; }
        public UserPreviewModel User { get; set; }
        public int Score { get; set; }
    }

    public class UserPreviewScoreModel
    {
        public UserPreviewModel User { get; set; }
        public int Score { get; set; }
    }
}
