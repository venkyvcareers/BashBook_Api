using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.Model.Group;
using BashBook.Model.User;
using BashBook.Model.Poll;
using BashBook.Utility;

namespace BashBook.DAL.Vote
{
    public class UserVoteRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public bool Add(UserPollOptionModel model)
        {
            try
            {
                var userVote = new UserVote()
                {
                    PollId = model.PollId,
                    UserId = model.UserId,
                    OptionId = model.OptionId,
                    VotedOn = UnixTimeBaseClass.UnixTimeNow
                };

                _db.UserVotes.Add(userVote);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("UserVote - Add- " + json, ex);
                throw;
            }
        }

        public bool IsAlreadyVoted(UserPollOptionModel model)
        {
            return _db.UserVotes.Any(x =>
                x.UserId == model.UserId && x.PollId == model.PollId && x.OptionId == model.OptionId);
        }

        public VoteResultModel GetVoteResult(int pollId)
        {
            var result = (from q in _db.Polls
                          where q.PollId == pollId
                          select new VoteResultModel()
                          {
                              Poll = new PollModel()
                              {
                                  PollId = q.PollId,
                                  Image = q.Image,
                                  Text = q.Text,
                                  SelectionTypeId = q.SelectionTypeId,
                                  OptionTypeId = q.OptionTypeId,
                                  IsActive = q.IsActive,
                                  UserId = q.CreatedBy,
                                  CreatedOn = q.CreatedOn
                              },
                              Options = (from o in _db.Options
                                         where o.PollId == pollId
                                         select new OptionResponseModel()
                                         {
                                             OptionId = o.OptionId,
                                             Image = o.Image,
                                             Text = o.Text,
                                             Count = _db.UserVotes.Count(x => x.PollId == pollId && x.OptionId == o.OptionId)
                                         }).ToList(),
                              TotalUserCount = _db.UserVotes.Where(x => x.PollId == pollId).Select(x => x.UserId).Distinct().Count()
                          }).FirstOrDefault();

            return result;
        }

        public List<UserVoteInfoModel> GetUsersInfo(int pollId)
        {
            var result = (from uv in _db.UserVotes
                          where uv.PollId == pollId
                          select new UserVoteInfoModel()
                          {
                              UserId = uv.UserId,
                              OptionId = uv.OptionId,
                              VotedOn = uv.VotedOn ?? 0
                          }).ToList();

            return result;
        }

        public bool Delete(int userVoteId)
        {
            try
            {
                var userVote = _db.UserVotes.First(x => x.UserVoteId == userVoteId);
                _db.UserVotes.Remove(userVote);

                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("UserVote - Delete" + userVoteId, ex);
                throw;
            }

        }
    }
}
