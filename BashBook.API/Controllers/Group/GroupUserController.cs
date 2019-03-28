using System.Collections.Generic;
using BashBook.BAL.Post;
using BashBook.Model.Post;
using System.Web.Http;
using BashBook.BAL.Group;
using BashBook.Model.Group;

namespace BashBook.API.Controllers.Group
{
    [Authorize]
    [RoutePrefix("api/GroupUser")]
    public class GroupUserController : BaseController
    {
        readonly GroupUserOperation _groupUser = new GroupUserOperation();

        [HttpPost]
        [Route("Add")]
        public int Add(GroupUserModel model)
        {
            return _groupUser.Add(model);
        }

        [HttpPost]
        [Route("UpdateRole")]
        public bool UpdateRole(UpdateRoleModel model)
        {
            return _groupUser.UpdateRole(model);
        }

        [HttpPost]
        [Route("UpdateLastSeen")]
        public bool UpdateLastSeen(GroupUserIdModel model)
        {
            return _groupUser.UpdateLastSeen(model);
        }

        [HttpPost]
        [Route("DeleteUser")]
        public bool DeleteUser(GroupUserIdModel model)
        {
            return _groupUser.DeleteUser(model);
        }
    }
}
