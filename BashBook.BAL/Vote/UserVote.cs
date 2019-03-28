using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Transactions;
using System.Web.Script.Serialization;
using BashBook.DAL.User;
using BashBook.DAL.Vote;
using BashBook.Model;
using BashBook.Model.Poll;

namespace BashBook.BAL.Vote
{
    public class UserVoteOperation : BaseBusinessAccessLayer
    {
        readonly UserVoteRepository _userVote = new UserVoteRepository();
        readonly UserRepository _user = new UserRepository();
        readonly OptionRepository _option = new OptionRepository();
        readonly PollRepository _poll = new PollRepository();

        public bool Add(UserVoteModel model)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    if (!_poll.IsActive(model.PollId))
                    {
                        throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = "Voting not started yet." });
                    }
                    foreach (var option in model.Options)
                    {
                        var userVote = new UserPollOptionModel()
                        {
                            UserId = model.UserId,
                            OptionId = option,
                            PollId = model.PollId
                        };
                        if (_userVote.IsAlreadyVoted(userVote))
                        {
                            if (model.Options.Count == 1)
                            {
                                scope.Dispose();
                                throw new InvalidOperationException("You have already voted for this poll");
                            }
                        }
                        else
                        {
                            _userVote.Add(userVote);
                        }
                    }

                    scope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string json = js.Serialize(model);
                    Log.Error("BL-Voting - Add" + json, ex);
                    throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
                }
            }
        }

        public VoteResultModel Result(int pollId)
        {
            return _userVote.GetVoteResult(pollId);
        }

        public List<VoteUserInfoModel> UsersInfo(int pollId)
        {
            var list = _userVote.GetUsersInfo(pollId);
            var uniqueUsers = list.Select(x => x.UserId).Distinct().ToList();
            var userInfoList = _user.GetPreviewsByList(uniqueUsers);

            var result = new List<VoteUserInfoModel>();

            foreach (var user in uniqueUsers)
            {
                result.Add(new VoteUserInfoModel()
                {
                    UserInfo = userInfoList.First(x => x.UserId == user),
                    VotedOn = list.First(x => x.UserId == user).VotedOn,
                    Options = list.Where(x => x.UserId == user).Select(x => x.OptionId).ToList()
                });
            }

            return result;
        }

        public bool Edit(UserVoteModel model)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    var oldOptions = _option.GetOptionList(model.UserId, model.PollId);

                    foreach (var option in model.Options)
                    {
                        if (!oldOptions.Contains(option))
                        {
                            var userVote = new UserPollOptionModel()
                            {
                                UserId = model.UserId,
                                OptionId = option,
                                PollId = model.PollId
                            };
                            if (_userVote.IsAlreadyVoted(userVote))
                            {
                                if (model.Options.Count == 1)
                                {
                                    scope.Dispose();
                                    throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = "User already voted" });
                                }
                            }
                            else
                            {
                                _userVote.Add(userVote);
                            }
                        }
                    }

                    foreach (var option in oldOptions)
                    {
                        if (!model.Options.Contains(option))
                        {
                            _option.Delete(option);
                        }
                    }
                    scope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string json = js.Serialize(model);
                    Log.Error("BL-Voting - Edit" + json, ex);
                    throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
                }
            }

        }

        public bool Delete(int userVoteId)
        {
            return _userVote.Delete(userVoteId);
        }
    }
}
