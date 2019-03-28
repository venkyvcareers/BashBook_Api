using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.Model.Event;
using BashBook.Utility;

namespace BashBook.DAL.Event
{
    public class EventGroupRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();
        public List<EventGroupModel> GetAll(int eventId)
        {
            try
            {
                var result = (from e in _db.EventGroups
                              where e.EventId == eventId
                              select new EventGroupModel
                              {
                                  EventId = e.EventId,
                                  EventGroupId = e.EventGroupId,
                                  GroupId = e.GroupId
                              }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("EventGroup - GetAll - EntityId - " + eventId, ex);
                throw;
            }

        }

        public int Add(EventGroupModel model)
        {
            try
            {
                var user = new EDM.EventGroup()
                {
                    EventId = model.EventId,
                    GroupId = model.GroupId,
                };

                _db.EventGroups.Add(user);
                _db.SaveChanges();

                return user.EventGroupId;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("EventGroup - Add - " + json, ex);
                throw;
            }

        }

        public bool Delete(int eventGroupId)
        {
            try
            {
                var user = _db.EventGroups.First(x=>x.EventGroupId == eventGroupId);

                _db.EventGroups.Remove(user);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("EventGroup - Delete" + eventGroupId, ex);
                throw;
            }

        }

        public bool Delete(int eventId, int userId)
        {
            try
            {
                var user = _db.EventGroups.First(x => x.EventId == eventId && x.GroupId == userId);

                _db.EventGroups.Remove(user);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("EventGroup - Delete - EventId - " + eventId + ", GroupId - " + userId, ex);
                throw;
            }

        }
    }

}
