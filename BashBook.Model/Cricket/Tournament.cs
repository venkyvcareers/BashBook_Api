using System.Collections.Generic;

namespace BashBook.Model.Cricket
{
    public class TournamentModel
    {
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }

    public class UserMatchPredictionModel
    {
        public int UserId { get; set; }
        public int MatchId { get; set; }
        public List<QuestionAnswerModel> Answers { get; set; }
    }

    public class MatchQuestionUserAnswerModel
    {
        public int MatchQuestionId { get; set; }
        public int MatchId { get; set; }
        public int UserId { get; set; }
        public int Answer { get; set; }
    }

    public class QuestionAnswerModel
    {
        public int MatchQuestionId { get; set; }
        public int Answer { get; set; }
    }

    public class CategoryQuestionModel
    {
        public CategoryModel Category { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }

    public class CategoryModel
    {
        public int PredictionCategoryId { get; set; }
        public string Name { get; set; }
        public int CategoryTypeId { get; set; }
        public int WinPoints { get; set; }
        public int LossPoints { get; set; }
        public int BonusTimes { get; set; }
    }

    public class QuestionModel
    {
        public int MatchQuestionId { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public int SelectionTypeId { get; set; }
        public int OptionsTypeId { get; set; }
        public int? LookupParentId { get; set; }
        public int? LookupDisplayTypeId { get; set; }
        public int? Answer { get; set; }
    }

    public class TournamentPredictionDataModel
    {
        public List<TeamModel> Teams { get; set; }
        public List<PlayerModel> Players { get; set; }

    }
}
