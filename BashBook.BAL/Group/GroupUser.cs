using System.Collections.Generic;
using System.Data;
using System.Net;
using BashBook.DAL.Group;
using BashBook.Model;
using BashBook.Model.Group;

namespace BashBook.BAL.Group
{
    public class GroupUserOperation : BaseBusinessAccessLayer
    {
        readonly GroupUserRepository _groupUser = new GroupUserRepository();

        public List<int> GetAllUsers(int groupId)
        {
            return _groupUser.GetAllUsers(groupId);
        }
        public int Add(GroupUserModel model)
        {
            if (_groupUser.IsUserExisted(model.UserId, model.GroupId))
            {
                throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = "User already added in the group" });
            }
            return _groupUser.Add(model);
        }

        public bool UpdateRole(UpdateRoleModel model)
        {
            return _groupUser.UpdateRole(model);
        }

        public bool UpdateLastSeen(GroupUserIdModel model)
        {
            return _groupUser.UpdateLastSeen(model);
        }
        public bool DeleteUser(GroupUserIdModel model)
        {
            return _groupUser.DeleteUser(model);
        }
    }
}
