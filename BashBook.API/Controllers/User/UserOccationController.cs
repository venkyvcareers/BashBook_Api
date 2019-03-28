using System.Collections.Generic;
using BashBook.BAL.User;
using BashBook.Model.User;
using System.Web.Http;
using BashBook.Model.Global;

namespace BashBook.API.Controllers.User
{
    [Authorize]
    [RoutePrefix("api/UserOccation")]
    public class UserOccationController : BaseController
    {
        readonly UserOccationOperation _userOccation = new UserOccationOperation();

        [HttpGet]
        [Route("GetAll/{userId}")]
        public List<UserOccationViewModel> GetAll(int userId)
        {
            return _userOccation.GetAll(userId);
        }

        [HttpPost]
        [Route("Add")]
        public int Add(UserOccationModel model)
        {
            return _userOccation.Add(model);
        }

        [HttpPost]
        [Route("Edit")]
        public bool Edit(UserOccationModel model)
        {
            return _userOccation.Edit(model);
        }

        [HttpDelete]
        [Route("Delete/{userOccationId}")]
        public bool Delete(int userOccationId)
        {
            return _userOccation.Delete(userOccationId);
        }
    }
}
