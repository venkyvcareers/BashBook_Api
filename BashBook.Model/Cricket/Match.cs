using System.Collections.Generic;

namespace BashBook.Model.Cricket
{
    public class MatchPreviewModel
    {
        public int MatchId { get; set; }
        public int Number { get; set; }
        public int StatusId { get; set; }
        public long StartsOn { get; set; }
        public string Result { get; set; }
        public string Ground { get; set; }
        public TeamModel HomeTeam { get; set; }
        public TeamModel OpponentTeam { get; set; }
    }

    public class MatchPredictionDataModel
    {
        public int MatchId { get; set; }
        public int Number { get; set; }
        public List<TeamModel> Teams { get; set; }
        public List<PlayerModel> Players { get; set; }

    }

    public class MatchQuestionAnswerModel
    {
        public int CategoryTypeId { get; set; }
        public int WinPoints { get; set; }
        public int LossPoints { get; set; }
        public int BonusTimes { get; set; }
        public List<QuestionAnswerModel> Answers { get; set; }
    }

}
