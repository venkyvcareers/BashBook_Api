using System.Collections.Generic;
using BashBook.BAL.User;
using BashBook.Model.User;
using System.Web.Http;
using BashBook.Model.Global;

namespace BashBook.API.Controllers.User
{
    [Authorize]
    [RoutePrefix("api/UserContact")]
    public class UserContactController : BaseController
    {
        readonly UserContactOperation _userContact = new UserContactOperation();

        [HttpGet]
        [Route("GetContactPreviewList/{userId}")]
        public List<UserPreviewModel> GetContactPreviewList(int userId)
        {
            return _userContact.GetContactPreviewList(userId);
        }

        [HttpGet]
        [Route("GetSearchList")]
        public List<UserPreviewModel> GetSearchList()
        {
            return _userContact.GetSearchList(GetUserId(), "");
        }

        [HttpGet]
        [Route("GetSearchList/{key}")]
        public List<UserPreviewModel> GetSearchList(string key)
        {
            return _userContact.GetSearchList(GetUserId(), key);
        }

        [HttpGet]
        [Route("GetFriendRequests")]
        public List<UserPreviewModel> GetFriendRequests()
        {
            return _userContact.GetRequestedList(GetUserId());
        }

        [HttpGet]
        [Route("GetFriends")]
        public List<UserPreviewModel> GetFriends()
        {
            return _userContact.GetAcceptedList(GetUserId());
        }

        [HttpGet]
        [Route("AddFriendrequest/{contactId}")]
        public int AddFriendrequest(int contactId)
        {
            return _userContact.AddFriendRequest(contactId, GetUserId());
        }

        [HttpGet]
        [Route("AcceptFriendrequest/{contactId}")]
        public bool AcceptFriendrequest(int contactId)
        {
            return _userContact.AcceptFriendRequest(contactId, GetUserId());
        }
    }
}
