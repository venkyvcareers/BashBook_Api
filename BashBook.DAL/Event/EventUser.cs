using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.Model.Event;
using BashBook.Model.Group;
using BashBook.Utility;

namespace BashBook.DAL.Event
{
    public class EventUserRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();
        public List<EventUserModel> GetAll(int eventId)
        {
            try
            {
                var result = (from e in _db.EventUsers
                              where e.EventId == eventId
                              select new EventUserModel
                              {
                                  EventId = e.EventId,
                                  EventUserId = e.EventUserId,
                                  UserId = e.UserId,
                                  CreatedBy = e.CreatedBy
                              }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("EventUser - GetAll - EntityId - " + eventId, ex);
                throw;
            }

        }

        public List<int> GetAllUsers(int eventId)
        {
            var result = (from gu in _db.EventUsers
                          where gu.EventId == eventId
                          select gu.UserId).ToList();
            return result;
        }

        public int Add(EventUserModel model)
        {
            try
            {
                var user = new EDM.EventUser()
                {
                    EventId = model.EventId,
                    CreatedBy = model.CreatedBy,
                    UserId = model.UserId,
                    CreatedOn = UnixTimeBaseClass.UnixTimeNow
                };

                _db.EventUsers.Add(user);
                _db.SaveChanges();

                return user.EventUserId;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("EventUser - Add - " + json, ex);
                throw;
            }

        }

        public bool UpdateRole(UpdateRoleModel model)
        {
            try
            {
                var eventUser = _db.EventUsers.First(x => x.EventId == model.GroupId && x.UserId == model.UserId);

                eventUser.RoleId = model.RoleId;

                _db.Entry(eventUser).State = EntityState.Modified;
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

        public bool Delete(int eventUserId)
        {
            try
            {
                var user = _db.EventUsers.First(x => x.EventUserId == eventUserId);

                _db.EventUsers.Remove(user);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("EventUser - Delete" + eventUserId, ex);
                throw;
            }

        }

        public bool Delete(int eventId, int userId)
        {
            try
            {
                var user = _db.EventUsers.First(x => x.EventId == eventId && x.UserId == userId);

                _db.EventUsers.Remove(user);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("EventUser - Delete - EventId - " + eventId + ", UserId - " + userId, ex);
                throw;
            }

        }
    }

}
