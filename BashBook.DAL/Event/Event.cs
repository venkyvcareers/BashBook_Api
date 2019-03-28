using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.Model.Event;
using BashBook.Utility;
using BashBook.Model.Global;
using BashBook.Model.Lookup;

namespace BashBook.DAL.Event
{
    public class EventRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public List<EventModel> GetAll(int entityTypeId, int entityId)
        {
            try
            {
                var result = (from e in _db.Events
                              where e.EntityId == entityId && e.EntityTypeId == entityTypeId
                              select new EventModel
                              {
                                  EntityId = e.EntityId,
                                  EntityTypeId = e.EntityTypeId,
                                  Title = e.Title,
                                  DateTime = e.DateTime,
                                  EventId = e.EventId,
                                  Image = e.Image,
                                  OccationId = e.OccationId,
                                  CreatedBy = e.CreatedBy,
                                  CreatedOn = e.CreatedOn
                              }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Event - SelectAllByGroupIdDeleteByEntity - EntityId - " + entityId + ", EntityTypeId - " + entityTypeId, ex);
                throw;
            }

        }

        public List<EntityPreviewModel> GetPreviewList(int userId)
        {
            var result = (from eu in _db.EventUsers
                from e in _db.Events
                where eu.UserId == userId && eu.EventId == e.EventId
                select new EntityPreviewModel
                {
                    Name = e.Title,
                    EntityId = e.EventId,
                    Image = e.Image,
                    Message = e.Title,
                    EntityTypeId = (int)Lookups.EntityTypes.Event
                }).ToList();

            return result;
        }

        public List<EventModel> GetAllByEntity(int entityTypeId, int entityId)
        {
            try
            {
                var result = (from e in _db.Events
                    where e.EntityId == entityId && e.EntityTypeId == entityTypeId
                    select new EventModel
                    {
                        EntityId = e.EntityId,
                        EntityTypeId = e.EntityTypeId,
                        Title = e.Title,
                        DateTime = e.DateTime,
                        EventId = e.EventId,
                        Image = e.Image,
                        OccationId = e.OccationId,
                        CreatedBy = e.CreatedBy,
                        CreatedOn = e.CreatedOn
                    }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Event - SelectAllByGroupIdDeleteByEntity - EntityId - " + entityId + ", EntityTypeId - " + entityTypeId, ex);
                throw;
            }

        }

        public EventModel GetById(int eventId)
        {
            try
            {
                var result = (from e in _db.Events
                              where e.EventId == eventId
                              select new EventModel
                              {
                                  EntityId = e.EntityId,
                                  EntityTypeId = e.EntityTypeId,
                                  Title = e.Title,
                                  DateTime = e.DateTime,
                                  EventId = e.EventId,
                                  Image = e.Image,
                                  OccationId = e.OccationId,
                                  CreatedBy = e.CreatedBy,
                                  CreatedOn = e.CreatedOn
                              }).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Event - SelectById" + eventId, ex);
                throw;
            }

        }

        public int Add(EventModel model)
        {
            try
            {
                var newEvent = new EDM.Event()
                {
                    Title = model.Title,
                    EntityId = model.EntityId,
                    EntityTypeId = model.EntityTypeId,
                    DateTime = model.DateTime,
                    Image = model.Image,
                    OccationId = model.OccationId,
                    CreatedBy = model.CreatedBy,
                    CreatedOn = UnixTimeBaseClass.UnixTimeNow
                };

                _db.Events.Add(newEvent);
                _db.SaveChanges();

                return newEvent.EventId;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Event - Add - " + json, ex);
                throw;
            }

        }

        public bool Edit(EventModel model)
        {
            try
            {
                var oldEvent = _db.Events.First(x => x.EventId == model.EventId);
                oldEvent.Title = model.Title;
                oldEvent.EntityId = model.EntityId;
                oldEvent.EntityTypeId = model.EntityTypeId;
                oldEvent.DateTime = model.DateTime;
                oldEvent.Image = model.Image;
                oldEvent.OccationId = model.OccationId;
                oldEvent.LastUpdatedBy = model.CreatedBy;
                oldEvent.LastUpdatedOn = UnixTimeBaseClass.UnixTimeNow;

                _db.Entry(oldEvent).State = EntityState.Modified;
                _db.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Event - Edit - " + json, ex);
                throw;
            }

        }

        public bool UpdateImage(StringModel model)
        {
            try
            {
                var evnt = _db.Events.First(x => x.EventId == model.Id);

                evnt.Image = model.Text;
                evnt.LastUpdatedOn = UnixTimeBaseClass.UnixTimeNow;

                _db.Entry(evnt).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Group - Update group pic- " + json, ex);
                throw;
            }
        }
        public bool UpdateTitle(StringModel model)
        {
            try
            {
                var evnt = _db.Events.First(x => x.EventId == model.Id);

                evnt.Title = model.Text;
                evnt.LastUpdatedOn = UnixTimeBaseClass.UnixTimeNow;

                _db.Entry(evnt).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Group - Update Name - " + json, ex);
                throw;
            }
        }
        public bool UpdateMessage(StringModel model)
        {
            try
            {
                var group = _db.Events.First(x => x.EventId == model.Id);

                group.Message = model.Text;
                group.LastUpdatedOn = UnixTimeBaseClass.UnixTimeNow;

                _db.Entry(group).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Group - Update Message - " + json, ex);
                throw;
            }
        }

        public bool Delete(int eventId)
        {
            try
            {
                var existedEvent = _db.Events.Find(eventId);

                if (existedEvent != null) _db.Events.Remove(existedEvent);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Event - Delete" + eventId, ex);
                throw;
            }

        }

        public bool DeleteByEntity(int entityTypeId, int entityId)
        {
            try
            {
                var existedEvents = _db.Events.Where(x => x.EntityId == entityId && x.EntityTypeId == entityTypeId).ToList();

                _db.Events.RemoveRange(existedEvents);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Event - DeleteByEntity - EntityId - " + entityId + ", EntityTypeId - " + entityTypeId, ex);
                throw;
            }

        }
    }
    
}
