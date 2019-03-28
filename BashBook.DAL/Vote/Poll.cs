using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.Model.Global;
using BashBook.Model.Poll;
using BashBook.Utility;
using BashBook.Model.Group;
using BashBook.Model.Lookup;

namespace BashBook.DAL.Vote
{
    public class PollRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public List<PollModel> GetAll(int userId)
        {
            try
            {
                var result = (from q in _db.Polls
                              where q.CreatedBy == userId
                              select new PollModel
                              {
                                  PollId = q.PollId,
                                  Image = q.Image,
                                  Text = q.Text,
                                  SelectionTypeId = q.SelectionTypeId,
                                  OptionTypeId = q.OptionTypeId,
                                  CategoryId = q.CategoryId,
                                  VisibilityId = q.VisibilityId,
                                  IsActive = q.IsActive,
                                  IsVotingCompleted = q.IsVotingCompleted,
                                  UserId = q.CreatedBy,
                                  CreatedOn = q.CreatedOn
                              }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Poll - GetAll - " + userId, ex);
                throw;
            }

        }

        public PollModel GetById(int pollId)
        {
            try
            {
                var result = (from q in _db.Polls
                              where q.PollId == pollId
                              select new PollModel
                              {
                                  PollId = q.PollId,
                                  Image = q.Image,
                                  Text = q.Text,
                                  SelectionTypeId = q.SelectionTypeId,
                                  OptionTypeId = q.OptionTypeId,
                                  CategoryId = q.CategoryId,
                                  VisibilityId = q.VisibilityId,
                                  IsActive = q.IsActive,
                                  IsVotingCompleted = q.IsVotingCompleted,
                                  UserId = q.CreatedBy,
                                  CreatedOn = q.CreatedOn
                              }).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Poll - GetById - " + pollId, ex);
                throw;
            }

        }

        public List<QuickPollViewModel> GetQuickPollList(int userId, int entityTypeId, int entityId)
        {
            try
            {
                var result = (from p in _db.Polls
                              from ep in _db.EntityPolls
                              where p.CreatedBy == userId && ep.EntityTypeId == entityTypeId
                                                          && ep.EntityId == entityId && p.PollId == ep.PollId
                              select new QuickPollViewModel
                              {
                                  PollId = p.PollId,
                                  Text = p.Text,
                                  Options = (from o in _db.Options
                                             where o.PollId == p.PollId
                                             select new StringModel()
                                             {
                                                 Text = o.Text,
                                                 Id = o.OptionId
                                             }).ToList()
                              }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Poll - GetQuickPollList - " + userId, ex);
                throw;
            }
        }

        public List<GroupVoteRequestModel> GetGroupVoteRequestList(int userId)
        {
            var groupList = (from gu in _db.GroupUsers
                             where gu.UserId == userId
                             select gu.GroupId).Distinct().ToList();

            var result = (from ep in _db.EntityPolls
                          where ep.EntityTypeId == (int)Lookups.EntityTypes.Group && groupList.Contains(ep.EntityId)
                          select new GroupVoteRequestModel()
                          {
                              Group = (from g in _db.Groups
                                       where g.GroupId == ep.EntityId
                                       select new GroupPreviewModel()
                                       {
                                           Name = g.Title,
                                           GroupId = g.GroupId,
                                           Image = g.Image,
                                           Message = g.Message
                                       }).FirstOrDefault(),
                              Poll = (from q in _db.Polls
                                      where q.PollId == ep.PollId
                                      select new PollModel
                                      {
                                          PollId = q.PollId,
                                          Image = q.Image,
                                          Text = q.Text,
                                          SelectionTypeId = q.SelectionTypeId,
                                          OptionTypeId = q.OptionTypeId,
                                          CategoryId = q.CategoryId,
                                          VisibilityId = q.VisibilityId,
                                          IsActive = q.IsActive,
                                          IsVotingCompleted = q.IsVotingCompleted,
                                          UserId = q.CreatedBy,
                                          CreatedOn = q.CreatedOn
                                      }).FirstOrDefault(),
                              OptionSelected = _db.UserVotes.FirstOrDefault(x => x.UserId == userId && x.PollId == ep.PollId).Option.Text,
                              IsVoted = _db.UserVotes.Any(x => x.UserId == userId && x.PollId == ep.PollId),
                              VotedOn = _db.UserVotes.FirstOrDefault(x => x.UserId == userId && x.PollId == ep.PollId).VotedOn
                          }).ToList();
            return result;
        }

        public bool IsActive(int pollId)
        {
            try
            {
                return _db.Polls.First(x => x.PollId == pollId).IsActive;
            }
            catch (Exception ex)
            {
                Log.Error("Poll - IsActive - " + pollId, ex);
                throw;
            }

        }

        public PollModel GetAllActive()
        {
            try
            {
                var result = (from q in _db.Polls
                              where q.IsActive && !q.IsVotingCompleted
                              select new PollModel
                              {
                                  PollId = q.PollId,
                                  Image = q.Image,
                                  Text = q.Text,
                                  SelectionTypeId = q.SelectionTypeId,
                                  OptionTypeId = q.OptionTypeId,
                                  CategoryId = q.CategoryId,
                                  VisibilityId = q.VisibilityId,
                                  IsActive = q.IsActive,
                                  IsVotingCompleted = q.IsVotingCompleted,
                                  UserId = q.CreatedBy,
                                  CreatedOn = q.CreatedOn
                              }).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Poll - GetAllActive", ex);
                throw;
            }

        }

        public int Add(PollModel model)
        {
            try
            {
                var poll = new EDM.Poll()
                {
                    Text = model.Text,
                    Image = model.Image,
                    IsActive = model.IsActive,
                    IsVotingCompleted = model.IsVotingCompleted,
                    SelectionTypeId = model.SelectionTypeId,
                    OptionTypeId = model.OptionTypeId,
                    CategoryId = model.CategoryId,
                    VisibilityId = model.VisibilityId,
                    CreatedBy = model.UserId,
                    CreatedOn = UnixTimeBaseClass.UnixTimeNow
                };

                _db.Polls.Add(poll);
                _db.SaveChanges();

                return poll.PollId;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Poll - Add - " + json, ex);
                throw;
            }

        }

        public bool Edit(PollModel model)
        {
            try
            {
                var poll = _db.Polls.First(x => x.PollId == model.PollId);

                poll.Text = model.Text;
                poll.Image = model.Image;
                poll.SelectionTypeId = model.SelectionTypeId;
                poll.OptionTypeId = model.OptionTypeId;
                poll.CategoryId = model.CategoryId;
                poll.VisibilityId = model.VisibilityId;
                poll.LastUpdatedOn = UnixTimeBaseClass.UnixTimeNow;

                _db.Entry(poll).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Poll - Edit - " + json, ex);
                throw;
            }
        }

        public bool Activate(int pollId)
        {
            try
            {
                var poll = _db.Polls.First(x => x.PollId == pollId);

                if (poll.IsActive)
                {
                    return false;
                }
                poll.IsActive = true;
                _db.Entry(poll).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Poll - Activate - " + pollId, ex);
                throw;
            }
        }

        public bool Deactivate(int pollId)
        {
            try
            {
                var poll = _db.Polls.First(x => x.PollId == pollId);

                if (!poll.IsActive)
                {
                    return false;
                }
                poll.IsActive = false;
                _db.Entry(poll).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Poll - Activate - " + pollId, ex);
                throw;
            }
        }

        public bool Complete(int pollId)
        {
            try
            {
                var poll = _db.Polls.First(x => x.PollId == pollId);

                if (poll.IsVotingCompleted)
                {
                    return false;
                }
                poll.IsVotingCompleted = true;
                _db.Entry(poll).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Poll - Complete - " + pollId, ex);
                throw;
            }
        }

        public bool Reopen(int pollId)
        {
            try
            {
                var poll = _db.Polls.First(x => x.PollId == pollId);

                if (!poll.IsVotingCompleted)
                {
                    return false;
                }
                poll.IsVotingCompleted = false;
                _db.Entry(poll).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Poll - Complete - " + pollId, ex);
                throw;
            }
        }

        public bool Delete(int pollId)
        {
            try
            {
                //User Votings
                var userVotes = _db.UserVotes.Where(x => x.PollId == pollId);
                _db.UserVotes.RemoveRange(userVotes);

                //Options
                var options = _db.Options.Where(x => x.PollId == pollId);
                _db.Options.RemoveRange(options);

                //Poll
                var existedPoll = _db.Polls.First(x => x.PollId == pollId);
                _db.Polls.Remove(existedPoll);

                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Poll - Delete" + pollId, ex);
                throw;
            }

        }
    }
}
