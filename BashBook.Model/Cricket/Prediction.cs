namespace BashBook.Model.Cricket
{
    public class PredictionModel
    {
        public int MatchId { get; set; }
        public int UserId { get; set; }
        public int PredictionId { get; set; }
        public int? WinningTeamId { get; set; }
        public int? TossWinnerSelectionId { get; set; }
        public int? WinningSideTypeId { get; set; }
        public int? MostSixesTeamId { get; set; }
        public int? MostRunsInPowerPlayTeamId { get; set; }
        public int? FisrtInningsTotalRangeId { get; set; }
        public bool? HasSixInFirstOvers { get; set; }
        public bool? DoesMatchGoneToLastOver { get; set; }
        public int? ManOfTheMatchPlayerId { get; set; }
        public int? MostRunScorerPlayerId { get; set; }
        public int? MostWicketTakeerPlayerId { get; set; }
    }

    public class UserMatchStatusModel
    {
        public int MatchId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public int StatusId { get; set; }
        public long StartsOn { get; set; }
    }

    public class UserMatchAnswerModel
    {
        public int MatchQuestionId { get; set; }
        public int MatchId { get; set; }
        public int UserId { get; set; }
        public int Answer { get; set; }
    }

    public class MatchPredictionModel
    {
        public int MatchId { get; set; }
        public int PredictionId { get; set; }
    }

    public class UserScoreViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public int Score { get; set; }
    }
}
