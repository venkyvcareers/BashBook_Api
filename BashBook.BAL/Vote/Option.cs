using System.Collections.Generic;
using System.Net;
using BashBook.DAL.Vote;
using BashBook.Model;
using BashBook.Model.User;
using BashBook.Model.Poll;

namespace BashBook.BAL.Vote
{
    public class OptionOperation : BaseBusinessAccessLayer
    {
        readonly OptionRepository _option = new OptionRepository();
        readonly PollRepository _poll = new PollRepository();
        public List<OptionModel> GetAll(int pollId)
        {
            return _option.GetAll(pollId);
        }

        public List<OptionCountModel> GetOptionResponseCount(int pollId)
        {
            return _option.GetOptionResponseCount(pollId);
        }

        public List<UserGeneralViewModel> GetUserList(int optionId)
        {
            return _option.GetUserList(optionId);
        }

        public int Add(OptionModel model)
        {
            if (!_poll.IsActive(model.PollId))
            {
                throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = "Voting is in progress, can not be edited" });
            }
            model.Image = Cdn.Base64ToImageUrl(model.Image);
            return _option.Add(model);
        }

        public bool Edit(OptionModel model)
        {
            if (!_poll.IsActive(model.PollId))
            {
                throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = "Voting is in progress, can not be edited" });
            }
            model.Image = Cdn.Base64ToImageUrl(model.Image);
            return _option.Edit(model);
        }
    }
}
