using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BashBook.DAL.EDM;
using BashBook.Model.Cricket;
using BashBook.Model.Lookup;

namespace BashBook.DAL.Cricket
{
    public class MatchUserAnswerRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public List<MatchUserAnswerModel> GetUserMatchAnswers(int matchId)
        {
            var result = (from mua in _db.MatchUserAnswers
                          where mua.MatchQuestion.MatchId == matchId
                          select new MatchUserAnswerModel()
                          {
                              UserId = mua.UserId,
                              MatchQuestionId = mua.MatchQuestionId,
                              Answer = mua.Answer
                          }).ToList();

            return result;
        }

        public List<MatchUserCountModel> GetUserCounts(int tournamentId)
        {
            var result = (from match in _db.Matches
                          where match.TournamentId == tournamentId && match.StatusId != (int)Lookups.MatchStatus.Inactive
                          select new MatchUserCountModel()
                          {
                              MatchId = match.MatchId,
                              Number = match.Number,
                              StatusId = match.StatusId,
                              UserCount = _db.MatchUserAnswers.Where(x => x.MatchId == match.MatchId).Select(x => x.UserId).Distinct().Count()
                          }).ToList();

            return result;
        }

        public bool Add(MatchQuestionUserAnswerModel model)
        {
            var answer = new MatchUserAnswer()
            {
                UserId = model.UserId,
                MatchId = model.MatchId,
                MatchQuestionId = model.MatchQuestionId,
                Answer = model.Answer,
            };

            _db.MatchUserAnswers.Add(answer);
            _db.SaveChanges();

            return true;
        }

        public bool Edit(MatchQuestionUserAnswerModel model)
        {
            var answer = _db.MatchUserAnswers.FirstOrDefault(x =>
                x.MatchQuestionId == model.MatchQuestionId && x.UserId == model.UserId);

            if (answer == null)
            {
                return Add(model);
            }
            else
            {
                answer.Answer = model.Answer;

                _db.Entry(answer).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
        }

    }
}
