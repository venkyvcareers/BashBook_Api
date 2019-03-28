using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BashBook.DAL.EDM;
using BashBook.Model.Cricket;
using BashBook.Model.Lookup;
using BashBook.Utility;

namespace BashBook.DAL.Cricket
{
    public class MatchRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public long GetStartTime(int matchId)
        {
            return _db.Matches.First(x => x.MatchId == matchId).StartsOn;
        }

        public MatchPreviewModel GetById(int matchId)
        {
            return (from m in _db.Matches
                    where m.MatchId == matchId
                    select new MatchPreviewModel()
                    {
                        MatchId = m.MatchId,
                        Number = m.Number,
                        StartsOn = m.StartsOn,
                        StatusId = m.StatusId,
                        Result = m.Result,
                        Ground = m.Ground.Name + ", " + m.Ground.City,
                        HomeTeam = (from t in _db.Teams
                            where t.TeamId == m.HomeTeamId
                            select new TeamModel()
                            {
                                Id = t.TeamId,
                                Image = t.Image,
                                ShortName = t.ShortName,
                                Name = t.Name
                            }).FirstOrDefault(),
                        OpponentTeam = (from t in _db.Teams
                            where t.TeamId == m.OpponentTeamId
                            select new TeamModel()
                            {
                                Id = t.TeamId,
                                Image = t.Image,
                                ShortName = t.ShortName,
                                Name = t.Name
                            }).FirstOrDefault()
                    }).FirstOrDefault();
        }

        public List<int> GetTeamList(int matchId)
        {
            var match = _db.Matches.First(x => x.MatchId == matchId);
            var result = new List<int> { match.HomeTeamId, match.OpponentTeamId };

            return result;
        }

        public bool ChangeStatus(int matchId, int statusId)
        {
            var match = _db.Matches.Find(matchId);

            if (match != null)
            {
                match.StatusId = statusId;
                _db.Entry(match).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }

            return false;
        }

        public List<MatchQuestionAnswerModel> GetMatchQuestionAnswers(int matchId)
        {
            var categories = _db.MatchQuestions.Where(x => x.MatchId == matchId).Select(y => y.PredictionCategoryId)
                .Distinct().ToList();
            var matchAnswers = (from pc in _db.PredictionCategories
                                where categories.Contains(pc.PredictionCategoryId)
                                select new MatchQuestionAnswerModel()
                                {
                                    BonusTimes = pc.BonusTimes,
                                    CategoryTypeId = pc.CategoryTypeId,
                                    LossPoints = pc.LossPoints,
                                    WinPoints = pc.WinPoints,
                                    Answers = (from mq in _db.MatchQuestions
                                               where mq.MatchId == matchId && mq.PredictionCategoryId == pc.PredictionCategoryId
                                               select new QuestionAnswerModel()
                                               {
                                                   Answer = mq.Answer ?? 0,
                                                   MatchQuestionId = mq.MatchQuestionId
                                               }).ToList()
                                }).ToList();

            return matchAnswers;
        }

        public bool AddMatchQuestionAnswer(int matchQuestionId, int answer)
        {
            var matchQuestion = _db.MatchQuestions.Find(matchQuestionId);
            if (matchQuestion != null)
            {
                matchQuestion.Answer = answer;

                _db.Entry(matchQuestion).State = EntityState.Modified;
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        public List<CategoryQuestionModel> GetAllQuestion(int matchId)
        {
            var categories = _db.MatchQuestions
                .Where(x => x.MatchId == matchId)
                .Select(x => x.PredictionCategoryId).Distinct();

            var result = (from pc in _db.PredictionCategories
                          where categories.Contains(pc.PredictionCategoryId)
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
                                           from mq in _db.MatchQuestions
                                           where mq.MatchId == matchId
                                                 && mq.QuestionId == q.QuestionId
                                                 && mq.PredictionCategoryId == pc.PredictionCategoryId
                                           select new QuestionModel()
                                           {
                                               MatchQuestionId = mq.MatchQuestionId,
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

        public List<CategoryQuestionModel> GetUserAnswers(int matchId, int userId)
        {
            var categories = _db.MatchQuestions
                .Where(x => x.MatchId == matchId)
                .Select(x => x.PredictionCategoryId).Distinct();

            var result = (from pc in _db.PredictionCategories
                          where categories.Contains(pc.PredictionCategoryId)
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
                              Questions = (from mq in _db.MatchQuestions
                                           from q in _db.Questions
                                           where mq.MatchId == matchId
                                                 && mq.QuestionId == q.QuestionId
                                                 && mq.PredictionCategoryId == pc.PredictionCategoryId
                                           select new QuestionModel()
                                           {
                                               MatchQuestionId = mq.MatchQuestionId,
                                               Image = q.Image,
                                               QuestionId = q.QuestionId,
                                               Text = q.Text,
                                               SelectionTypeId = q.SelectionTypeId,
                                               LookupParentId = q.LookupParentId,
                                               OptionsTypeId = q.OptionsTypeId,
                                               LookupDisplayTypeId = q.LookupDisplayTypeId,
                                               Answer = _db.MatchUserAnswers.FirstOrDefault(x=>x.MatchQuestionId == mq.MatchQuestionId && x.UserId == userId).Answer
                                           }).ToList()
                          }).ToList();

            return result;
        }

        public List<CategoryQuestionModel> GetMatchAnswers(int matchId)
        {
            var categories = _db.MatchQuestions
                .Where(x => x.MatchId == matchId)
                .Select(x => x.PredictionCategoryId).Distinct();

            var result = (from pc in _db.PredictionCategories
                          where categories.Contains(pc.PredictionCategoryId)
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
                              Questions = (from mq in _db.MatchQuestions
                                           from q in _db.Questions
                                           where mq.MatchId == matchId
                                                 && mq.QuestionId == q.QuestionId
                                                 && mq.PredictionCategoryId == pc.PredictionCategoryId
                                           select new QuestionModel()
                                           {
                                               MatchQuestionId = mq.MatchQuestionId,
                                               Image = q.Image,
                                               QuestionId = q.QuestionId,
                                               Text = q.Text,
                                               SelectionTypeId = q.SelectionTypeId,
                                               LookupParentId = q.LookupParentId,
                                               OptionsTypeId = q.OptionsTypeId,
                                               LookupDisplayTypeId = q.LookupDisplayTypeId,
                                               Answer = mq.Answer
                                           }).Distinct().ToList()
                          }).ToList();

            return result;
        }

        public List<MatchPreviewModel> GetMatchList(int tournamentId)
        {
            var result = (from m in _db.Matches
                          where m.TournamentId == tournamentId && m.StatusId != (int)Lookups.MatchStatus.Inactive
                          select new MatchPreviewModel()
                          {
                              MatchId = m.MatchId,
                              Number = m.Number,
                              StartsOn = m.StartsOn,
                              StatusId = m.StatusId,
                              Result = m.Result,
                              HomeTeam = (from t in _db.Teams
                                          where t.TeamId == m.HomeTeamId
                                          select new TeamModel()
                                          {
                                              Id = t.TeamId,
                                              Image = t.Image,
                                              ShortName = t.ShortName,
                                              Name = t.Name
                                          }).FirstOrDefault(),
                              OpponentTeam = (from t in _db.Teams
                                              where t.TeamId == m.OpponentTeamId
                                              select new TeamModel()
                                              {
                                                  Id = t.TeamId,
                                                  Image = t.Image,
                                                  ShortName = t.ShortName,
                                                  Name = t.Name
                                              }).FirstOrDefault()

                          }).OrderBy(x=>x.StatusId).ToList();
            return result;
        }

        public List<MatchPreviewModel> GetSliderMatchList(int tournamentId)
        {
            var result = (from m in _db.Matches
                where m.TournamentId == tournamentId && m.StatusId == (int)Lookups.MatchStatus.YetToStart
                      && m.StartsOn > UnixTimeBaseClass.UnixTimeNow + 1800
                select new MatchPreviewModel()
                {
                    MatchId = m.MatchId,
                    Number = m.Number,
                    StartsOn = m.StartsOn,
                    StatusId = m.StatusId,
                    Result = m.Result,
                    Ground = m.Ground.Name + ", " + m.Ground.City,
                    HomeTeam = (from t in _db.Teams
                        where t.TeamId == m.HomeTeamId
                        select new TeamModel()
                        {
                            Id = t.TeamId,
                            Image = t.Image,
                            Name = t.Name
                        }).FirstOrDefault(),
                    OpponentTeam = (from t in _db.Teams
                        where t.TeamId == m.OpponentTeamId
                        select new TeamModel()
                        {
                            Id = t.TeamId,
                            Image = t.Image,
                            Name = t.Name
                        }).FirstOrDefault()

                }).OrderBy(x=>x.Number).Take(3).ToList();
            return result;
        }

        public MatchPredictionDataModel GetPredictionData(int matchId)
        {
            var result = (from m in _db.Matches
                          where m.MatchId == matchId
                          select new MatchPredictionDataModel()
                          {
                              MatchId = m.MatchId,
                              Number = m.Number,
                              Teams = (from t in _db.Teams
                                       where (t.TeamId == m.HomeTeamId || t.TeamId == m.OpponentTeamId)
                                       select new TeamModel()
                                       {
                                           Id = t.TeamId,
                                           Image = t.Image,
                                           Name = t.Name
                                       }).ToList(),
                              Players = (from tp in _db.TeamPlayers
                                         where (tp.TeamId == m.HomeTeamId || tp.TeamId == m.OpponentTeamId)
                                         select new PlayerModel()
                                         {
                                             Id = tp.Player.PlayerId,
                                             Image = tp.Player.Image,
                                             Name = tp.Player.Name
                                         }).ToList()
                          }).FirstOrDefault();

            return result;
        }
    }
}
