using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BashBook.DAL.Vote;
using BashBook.Model.Poll;
using System.Transactions;
using System.Web.Script.Serialization;
using BashBook.Model;
using BashBook.Model.Lookup;

namespace BashBook.BAL.Vote
{
    public class PollOperation : BaseBusinessAccessLayer
    {
        readonly PollRepository _poll = new PollRepository();
        readonly OptionRepository _option = new OptionRepository();
        readonly EntityPollRepository _entityPoll = new EntityPollRepository();


        public List<PollModel> GetAll(int userId)
        {
            return _poll.GetAll(userId);
        }

        public PollWithOptionsModel GetWithOptions(int pollId)
        {
            return new PollWithOptionsModel()
            {
                Poll = _poll.GetById(pollId),
                Options = _option.GetAll(pollId)

            };
        }

        public PollModel GetAllActive()
        {
            return _poll.GetAllActive();
        }

        public List<GroupVoteRequestModel> GetGroupVoteRequestList(int userId)
        {
            return _poll.GetGroupVoteRequestList(userId);
        }

        public int Add(PollModel model)
        {
            model.Image = Cdn.Base64ToImageUrl(model.Image);

            model.IsActive = true;
            model.IsVotingCompleted = false;

            return _poll.Add(model);
        }

        public int AddWithOptions(PollWithOptionsModel model)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    // Validations
                    if (model.Options.Count == 0)
                    {
                        throw new InvalidOperationException("Poll do not have any options");
                    }

                    foreach (var option in model.Options)
                    {
                        if (string.IsNullOrEmpty(option.Text) && model.Poll.OptionTypeId != (int)Lookups.PollOptionTypes.Image)
                        {
                            throw new InvalidOperationException("Poll option text is empty");
                        }
                    }

                    model.Poll.Image = Cdn.Base64ToImageUrl(model.Poll.Image);
                    model.Poll.IsActive = true;
                    model.Poll.IsVotingCompleted = false;
                    int pollId = _poll.Add(model.Poll);

                    foreach (var option in model.Options)
                    {
                        if (!string.IsNullOrEmpty(option.Text))
                        {
                            option.Image = Cdn.Base64ToImageUrl(option.Image);
                            option.PollId = pollId;
                            _option.Add(option);
                        }
                    }

                    foreach (var group in model.Groups)
                    {
                        _entityPoll.Add(new EntityPollModel()
                        {
                            EntityId = group,
                            PollId = pollId,
                            EntityTypeId = (int)Lookups.EntityTypes.Group
                        });
                    }

                    scope.Complete();

                    return pollId;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string json = js.Serialize(model);
                    Log.Error("BL-Poll - AddWithOptions" + json, ex);
                    throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
                }
            }
        }

        public List<QuickPollViewModel> GetQuickPollList(int userId, int entityTypeId, int entityId)
        {
            return _poll.GetQuickPollList(userId, entityTypeId, entityId);
        }
        public int AddQuickPoll(QuickPollModel model, int userId)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    var poll = new PollModel()
                    {
                        IsActive = true,
                        IsVotingCompleted = false,
                        UserId = userId,
                        OptionTypeId = (int)Lookups.PollOptionTypes.Text,
                        SelectionTypeId = (int)Lookups.PollSelectionTypes.Single,
                        Text = model.Text
                    };

                    int pollId = _poll.Add(poll);

                    foreach (var optionText in model.Options)
                    {
                        var option = new OptionModel()
                        {
                            Text = optionText,
                            PollId = pollId,
                        };
                        _option.Add(option);
                    }

                    // Add entity type poll Here
                    _entityPoll.Add(new EntityPollModel()
                    {
                        EntityId = model.EntityId,
                        PollId = pollId,
                        EntityTypeId = model.EntityTypeId
                    });

                    scope.Complete();

                    return pollId;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string json = js.Serialize(model);
                    Log.Error("BL-Poll - AddQuickPoll" + json, ex);
                    throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
                }
            }
        }

        public bool EditWithOptions(PollWithOptionsModel model)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    model.Poll.Image = Cdn.Base64ToImageUrl(model.Poll.Image);
                    if (_poll.IsActive(model.Poll.PollId))
                    {
                        throw new InvalidOperationException("Voting is in progress, can not be edited");
                    }
                    _poll.Edit(model.Poll);

                    var oldOptionList = _option.GetAllOptionId(model.Poll.PollId);
                    var newOptionList = model.Options.Where(x => x.OptionId > 0).Select(x => x.OptionId).ToList();
                    foreach (var option in model.Options)
                    {
                        if (option.OptionId > 0)
                        {
                            _option.Edit(option);
                        }
                        else
                        {
                            option.PollId = model.Poll.PollId;
                            option.Image = Cdn.Base64ToImageUrl(option.Image);
                            _option.Add(option);
                        }
                    }

                    foreach (var item in oldOptionList)
                    {
                        if (!newOptionList.Contains(item))
                        {
                            _option.Delete(item);
                        }
                    }


                    var oldGroupList = _entityPoll.GetEntityIdList(model.Poll.PollId, (int)Lookups.EntityTypes.Group);
                    foreach (var item in model.Groups)
                    {
                        if (!oldGroupList.Contains(item))
                        {
                            _entityPoll.Add(new EntityPollModel()
                            {
                                EntityId = item,
                                PollId = model.Poll.PollId,
                                EntityTypeId = (int)Lookups.EntityTypes.Group
                            });
                        }
                    }

                    foreach (var item in oldGroupList)
                    {
                        if (!model.Groups.Contains(item))
                        {
                            _entityPoll.Delete(model.Poll.PollId, (int)Lookups.EntityTypes.Group, item);
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
                    Log.Error("BL- Poll - EditWithOptions" + json, ex);
                    throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
                }
            }
        }

        public bool Edit(PollModel model)
        {
            if (!_poll.IsActive(model.PollId))
            {
                throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = "Voting is in progress, can not be edited" });
            }
            model.Image = Cdn.Base64ToImageUrl(model.Image);
            return _poll.Edit(model);
        }

        public bool Activate(int pollId)
        {
            return _poll.Activate(pollId);
        }

        public bool Deactivate(int pollId)
        {
            return _poll.Deactivate(pollId);
        }

        public bool Reopen(int pollId)
        {
            return _poll.Reopen(pollId);
        }

        public bool Complete(int pollId)
        {
            return _poll.Complete(pollId);
        }

        public bool Delete(int pollId)
        {
            if (!_poll.IsActive(pollId))
            {
                throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = "Voting is in progress, can not be deleted" });
            }
            return _poll.Delete(pollId);
        }
    }
}
