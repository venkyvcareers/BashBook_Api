using System.Collections.Generic;
using System.Net;
using BashBook.BAL.User;
using BashBook.Model.User;
using System.Web.Http;
using BashBook.Model;
using BashBook.Model.Global;

namespace BashBook.API.Controllers.User
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : BaseController
    {
        readonly UserOperation _user = new UserOperation();

        [HttpGet]
        [Route("GetById/{userId}")]
        public UserModel GetById(int userId)
        {
            return _user.GetById(userId);
        }

        [HttpGet]
        [Route("GetContactPreviewList/{userId}")]
        public List<UserPreviewModel> GetContactPreviewList(int userId)
        {
            return _user.GetContactPreviewList(userId);
        }

        [HttpGet]
        [Route("GetSearchList/{key}")]
        public List<UserPreviewModel> GetSearchList(string key)
        {
            return _user.GetSearchList(key);
        }

        [HttpPost]
        [Route("GetUserInfo")]
        public UserPreviewModel GetUserInfo(EmailModel model)
        {
            return _user.GetUserInfo(model);
        }

        [HttpGet]
        [Route("GetActivityList/{userId}")]
        public UserActivityModel GetActivityList(int userId)
        {
            return _user.GetActivityList(userId);
        }

        [HttpPost]
        [Route("UpdateUserInfo")]
        public bool Update(UserModel model)
        {
            return _user.Update(model);
        }

        [HttpPost]
        [Route("UpdateMessage")]
        public bool UpdateMessage(StringModel model)
        {
            return _user.UpdateMessage(model);
        }

        [HttpPost]
        [Route("UpdateImage")]
        public string UpdateImage(StringModel model)
        {
            return _user.UpdateImage(model);
        }

        [HttpPost]
        [Route("Register")]
        public UserRegisterValidationModel Register(UserRegisterModel model)
        {
            return _user.Register(model);
        }

        [HttpGet]
        [Route("IsMobileExisted/{mobile}")]
        public bool IsMobileExisted(string mobile)
        {
            return _user.IsMobileExisted(mobile);
        }
    }
}
