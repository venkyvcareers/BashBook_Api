using System.Collections.Generic;
using System.Linq;
using BashBook.DAL.EDM;
using BashBook.Model.Cricket;

namespace BashBook.DAL.Cricket
{
    public class TournamentRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public List<TournamentModel> GetAll()
        {
            var result = (from t in _db.Tournaments
                          select new TournamentModel
                          {
                              TournamentId = t.TournamentId,
                              Name = t.Name,
                              Image = t.Image,
                          }).ToList();

            return result;
        }

        public bool AddUserQuestionAnswer(MatchQuestionUserAnswerModel model)
        {
            var answer = new TournamentUserAnswer()
            {
                UserId = model.UserId,
                TournamentQuestionId = model.MatchQuestionId,
                Answer = model.Answer,
            };

            _db.TournamentUserAnswers.Add(answer);
            _db.SaveChanges();

            return true;
        }

        public List<CategoryQuestionModel> GetAllQuestion(int tournamentId)
        {
            var result = (from pc in _db.PredictionCategories
                          from tq in _db.TournamentQuestions
                          where tq.TournamentId == tournamentId
                                && tq.PredictionCategoryId == pc.PredictionCategoryId
                          select new CategoryQuestionModel
                          {
                              Category = new CategoryModel()
                              {
                                  Name = pc.Name,
                                  PredictionCategoryId = pc.PredictionCategoryId,
                                  BonusTimes = pc.BonusTimes,
                                  CategoryTypeId = pc.CategoryTypeId,
                                  LossPoints = pc.LossPoints,
                                  WinPoints = pc.WinPoints
                              },
                              Questions = (from q in _db.Questions
                                           where q.QuestionId == tq.QuestionId
                                           select new QuestionModel()
                                           {
                                               MatchQuestionId = tq.TournamentQuestionId,
                                               Image = q.Image,
                                               QuestionId = q.QuestionId,
                                               Text = q.Text,
                                               SelectionTypeId = q.SelectionTypeId,
                                               LookupParentId = q.LookupParentId,
                                               OptionsTypeId = q.OptionsTypeId,
                                               LookupDisplayTypeId = q.LookupDisplayTypeId
                                           }).ToList()
                          }).ToList();

            return result;
        }

        public TournamentPredictionDataModel GetPredictionData(int tournamentId)
        {
            var teams = _db.TournamentTeams.Where(x => x.TournamentId == tournamentId).Select(x => x.TeamId).ToList();

            var result = new TournamentPredictionDataModel()
            {
                Teams = (from t in _db.Teams
                         where teams.Contains(t.TeamId)
                         select new TeamModel()
                         {
                             Id = t.TeamId,
                             Image = t.Image,
                             Name = t.Name
                         }).ToList(),
                Players = (from tp in _db.TeamPlayers
                           where teams.Contains(tp.TeamId)
                           select new PlayerModel()
                           {
                               Id = tp.Player.PlayerId,
                               Image = tp.Player.Image,
                               Name = tp.Player.Name
                           }).ToList()
            };

            return result;
        }

    }
}
