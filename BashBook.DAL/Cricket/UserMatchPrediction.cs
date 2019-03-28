using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.Model.Cricket;

namespace BashBook.DAL.Cricket
{
    public class UserMatchPredictionRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public bool IsPredictionAdded(int userId, int matchId)
        {
            return _db.MatchUserAnswers.Any(x => x.UserId == userId && x.MatchId == matchId);
        }

        public List<UserMatchStatusModel> GetPredictedMatchList(int tournamentId, int userId)
        {
            var matchches = _db.Matches.Where(x => x.TournamentId == tournamentId).Select(x => x.MatchId).ToList();

            var userMatches = _db.MatchUserAnswers.Where(x => matchches.Contains(x.MatchId) && x.UserId == userId)
                .Select(y=>y.MatchId).Distinct().ToList();

            var result = (from m in _db.Matches
                          where userMatches.Contains(m.MatchId)
                          select new UserMatchStatusModel()
                          {
                              MatchId = m.MatchId,
                              StartsOn = m.StartsOn,
                              StatusId = m.StatusId
                          }).ToList();

            foreach (var match in result)
            {
                match.UserId = userId;
                var userScore = _db.MatchUserScores.FirstOrDefault(x => x.UserId == userId && x.MatchId == match.MatchId);
                if (userScore != null)
                {
                    match.Score = userScore.Score;
                }
                else
                {
                    match.Score = 0;
                }
                
            }

            // Score = _db.MatchUserScores.Any(x => x.UserId == userId && x.MatchId == m.MatchId) ? _db.MatchUserScores.First(x => x.UserId == userId && x.MatchId == m.MatchId).Score : 0,
            return result;
        }
        public int Add(UserMatchAnswerModel model)
        {
            try
            {
                var userMatchAnswer = new EDM.MatchUserAnswer()
                {
                    MatchId = model.MatchId,
                    MatchQuestionId = model.MatchQuestionId,
                    UserId = model.UserId,
                    Answer = model.Answer
                };

                _db.MatchUserAnswers.Add(userMatchAnswer);
                _db.SaveChanges();

                return userMatchAnswer.MatchUserAnswerId;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("UserMatchPrediction - Add- " + json, ex);
                throw;
            }
        }

        public List<UserScoreViewModel> GetLeaderBoardByMatch(int matchId)
        {
            var result = (from mus in _db.MatchUserScores
                          from u in _db.Users
                          where mus.MatchId == matchId && u.UserId == mus.UserId
                          select new UserScoreViewModel()
                          {
                              UserId = mus.UserId,
                              Score = mus.Score,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              Image = u.Image,
                          }).OrderByDescending(x=>x.Score).ToList();

            return result;
        }

        public List<UserScoreViewModel> GetLeaderBoardByTournament(int tournamentId)
        {
            var matches = _db.Matches.Where(x => x.TournamentId == tournamentId).Select(x => x.MatchId).ToList();

            var users = _db.MatchUserScores.Where(x => matches.Contains(x.MatchId)).Select(x => x.UserId)
                .Distinct().ToList();

            var result = (from ump in _db.Users
                          where users.Contains(ump.UserId)
                          select new UserScoreViewModel()
                          {
                              UserId = ump.UserId,
                              Score = _db.MatchUserScores.Where(x => x.UserId == ump.UserId && matches.Contains(x.MatchId)).Sum(x => x.Score),
                              FirstName = ump.FirstName,
                              LastName = ump.LastName,
                              Image = ump.Image,
                          }).OrderByDescending(x => x.Score).ToList();

            return result;
        }
    }
}
