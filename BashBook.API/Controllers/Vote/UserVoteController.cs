using System.Collections.Generic;
using System.Web.Http;
using BashBook.BAL.Vote;
using BashBook.Model.Poll;

namespace BashBook.API.Controllers.Vote
{
    [Authorize]
    [RoutePrefix("api/UserVote")]
    public class UserVoteController : BaseController
    {
        readonly UserVoteOperation _userVote = new UserVoteOperation();

        [HttpPost]
        [Route("Add")]
        public bool Add(UserVoteModel model)
        {
            int userId = GetUserId();
            model.UserId = userId;
            return _userVote.Add(model);
        }

        [HttpGet]
        [Route("Result/{pollId}")]
        public VoteResultModel Result(int pollId)
        {
            return _userVote.Result(pollId);
        }

        [HttpGet]
        [Route("UsersInfo/{pollId}")]
        public List<VoteUserInfoModel> UsersInfo(int pollId)
        {
            return _userVote.UsersInfo(pollId);
        }

        [HttpPost]
        [Route("Edit")]
        public bool Edit(UserVoteModel model)
        {
            return _userVote.Edit(model);
        }

        [HttpDelete]
        [Route("Delete/{userVoteId}")]
        public bool Delete(int userVoteId)
        {
            return _userVote.Delete(userVoteId);
        }
    }
}
