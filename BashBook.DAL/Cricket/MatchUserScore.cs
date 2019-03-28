using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BashBook.DAL.EDM;
using BashBook.Model.Cricket;
using BashBook.Model.Lookup;
using BashBook.Model.User;

namespace BashBook.DAL.Cricket
{
    public class MatchUserScoreRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public bool Add(MatchUserScoreModel model)
        {
            var score = _db.MatchUserScores.FirstOrDefault(x => x.MatchId == model.MatchId && x.UserId == model.UserId);
            if (score == null)
            {
                var newScore = new MatchUserScore()
                {
                    UserId = model.UserId,
                    MatchId = model.MatchId,
                    Score = model.Score
                };
                _db.MatchUserScores.Add(newScore);
                _db.SaveChanges();
            }
            else
            {
                score.Score = model.Score;
                _db.Entry(score).State = EntityState.Modified;
                _db.SaveChanges();
            }

            return true;
        }

        public List<MatchWinnerModel> GetMatchWinnerList(int tournamentId)
        {
            var matches = _db.Matches.Where(x => x.StatusId == (int)Lookups.MatchStatus.Completed).Select(y => y.MatchId).ToList();

            var result = (from m in _db.Matches
                where matches.Contains(m.MatchId)
                select new MatchWinnerModel()
                {
                    MatchId = m.MatchId,
                    MatchNumber = m.Number,
                    Users = (from mus in _db.MatchUserScores
                        where mus.MatchId == m.MatchId && mus.Score ==
                              _db.MatchUserScores.Where(x => x.MatchId == m.MatchId).Max(y => y.Score)
                        select new UserPreviewScoreModel()
                        {
                            Score = mus.Score,
                            User = (from u in _db.Users
                                where u.UserId == mus.UserId
                                select new UserPreviewModel()
                                {
                                    UserId = u.UserId,
                                    Image = u.Image,
                                    FirstName = u.FirstName,
                                    LastName = u.LastName
                                }).FirstOrDefault()
                        }).ToList()
                }).ToList();

            return result;
        }

    }
}
