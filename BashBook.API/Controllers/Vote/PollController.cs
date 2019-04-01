using System.Collections.Generic;
using System.Web.Http;
using BashBook.BAL.Vote;
using BashBook.Model.Poll;

namespace BashBook.API.Controllers.Vote
{
    [Authorize]
    [RoutePrefix("api/Poll")]
    public class PollController : BaseController
    {
        readonly PollOperation _poll = new PollOperation();

        [HttpGet]
        [Route("GetAll/{userId}")]
        public List<PollModel> GetAll(int userId)
        {
            return _poll.GetAll(userId);
        }

        [HttpGet]
        [Route("GetGroupVoteRequestList/{userId}")]
        public List<GroupVoteRequestModel> GetGroupVoteRequestList(int userId)
        {
            return _poll.GetGroupVoteRequestList(userId);
        }

        [HttpGet]
        [Route("GetWithOptions/{pollId}")]
        public PollWithOptionsModel GetWithOptions(int pollId)
        {
            return _poll.GetWithOptions(pollId);
        }

        [HttpGet]
        [Route("GetAllActive")]
        public PollModel GetAllActive()
        {
            return _poll.GetAllActive();
        }

        [HttpGet]
        [Route("GetQuickPollList/{entityTypeId}/{entityId}")]
        public List<QuickPollViewModel> GetQuickPollList(int entityTypeId, int entityId)
        {
            int userId = GetUserId();
            return _poll.GetQuickPollList(userId, entityTypeId, entityId);
        }


        [HttpPost]
        [Route("Add")]
        public int Add(PollModel model)
        {
            return _poll.Add(model);
        }

        [HttpPost]
        [Route("AddWithOptions")]
        public int AddWithOptions(PollWithOptionsModel model)
        {
            return _poll.AddWithOptions(model);
        }

        [HttpPost]
        [Route("EditWithOptions")]
        public bool EditWithOptions(PollWithOptionsModel model)
        {
            return _poll.EditWithOptions(model);
        }

        [HttpPost]
        [Route("Edit")]
        public bool Edit(PollModel model)
        {
            return _poll.Edit(model);
        }

        [HttpGet]
        [Route("Activate/{pollId}")]
        public bool Activate(int pollId)
        {
            return _poll.Activate(pollId);
        }

        [HttpGet]
        [Route("Deactivate/{pollId}")]
        public bool Deactivate(int pollId)
        {
            return _poll.Deactivate(pollId);
        }

        [HttpGet]
        [Route("Complete/{pollId}")]
        public bool Complete(int pollId)
        {
            return _poll.Complete(pollId);
        }

        [HttpGet]
        [Route("Reopen/{pollId}")]
        public bool Reopen(int pollId)
        {
            return _poll.Reopen(pollId);
        }

        [HttpDelete]
        [Route("Delete/{pollId}")]
        public bool Delete(int pollId)
        {
            return _poll.Delete(pollId);
        }
    }
}
