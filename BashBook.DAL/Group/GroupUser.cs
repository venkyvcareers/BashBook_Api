using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.Model.Group;
using BashBook.Utility;

namespace BashBook.DAL.Group
{
    public class GroupUserRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();
        public List<int> GetAllUsers(int groupId)
        {
            var result = (from gu in _db.GroupUsers
                          where gu.GroupId == groupId
                          select gu.UserId).ToList();
            return result;
        }

        public List<UserRoleModel> GetUsersWithRole(int groupId)
        {
            var result = (from gu in _db.GroupUsers
                          where gu.GroupId == groupId
                          select new UserRoleModel()
                          {
                              UserId = gu.UserId,
                              RoleId = gu.RoleId
                          }
                    ).ToList();
            return result;
        }

        public bool IsUserExisted(int userId, int groupId)
        {
            return _db.GroupUsers.Any(x => x.UserId == userId && x.GroupId == groupId);
        }

        public bool UpdateLastSeen(GroupUserIdModel model)
        {
            try
            {
                var groupUser = _db.GroupUsers.First(x => x.GroupId == model.GroupId && x.UserId == model.UserId);

                groupUser.LastSeenOn = UnixTimeBaseClass.UnixTimeNow;

                _db.Entry(groupUser).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Group - UpdateLastSeen - " + json, ex);
                throw;
            }
        }

        public long GetLastSeen(GroupUserIdModel model)
        {
            return _db.GroupUsers.First(x => x.GroupId == model.GroupId && x.UserId == model.UserId).LastSeenOn;
        }
        public int Add(GroupUserModel model)
        {
            try
            {
                var groupUser = new GroupUser()
                {
                    GroupId = model.GroupId,
                    UserId = model.UserId,
                    RoleId = model.RoleId,
                    LastSeenOn = UnixTimeBaseClass.UnixTimeNow,
                    CreatedBy = model.CreatedBy,
                    CreatedOn = UnixTimeBaseClass.UnixTimeNow
                };

                _db.GroupUsers.Add(groupUser);
                _db.SaveChanges();

                return groupUser.GroupUserId;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Group User - Add- " + json, ex);
                throw;
            }
        }

        public bool UpdateRole(UpdateRoleModel model)
        {
            try
            {
                var groupUser = _db.GroupUsers.First(x => x.GroupId == model.GroupId && x.UserId == model.UserId);

                groupUser.RoleId = model.RoleId;
                groupUser.LastUpdatedOn = UnixTimeBaseClass.UnixTimeNow;

                _db.Entry(groupUser).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Group - UpdateRole- " + json, ex);
                throw;
            }
        }

        public bool DeleteUser(GroupUserIdModel model)
        {
            try
            {
                var groupUser = _db.GroupUsers.First(x => x.GroupId == model.GroupId && x.UserId == model.UserId);

                _db.GroupUsers.Remove(groupUser);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Group - DeleteUser- " + json, ex);
                throw;
            }
        }
    }
}
