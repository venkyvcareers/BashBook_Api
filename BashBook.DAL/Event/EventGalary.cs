using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.Model.Event;
using BashBook.Utility;

namespace BashBook.DAL.Event
{
    public class EventGalaryRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();
        public List<EventGalaryItemModel> GetAll(int eventId)
        {
            try
            {
                var result = (from e in _db.EventGalaries
                              where e.EventId == eventId
                              select new EventGalaryItemModel
                              {
                                  EventGalaryId = e.EventGalaryId,
                                  EventId = e.EventId,
                                  TypeId = e.TypeId,
                                  Url = e.Url,
                                  CreatedBy = e.CreatedBy,
                                  CreatedOn = e.CreatedOn
                              }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("EventGalary - GetAll - EntityId - " + eventId, ex);
                throw;
            }

        }

        public int Add(EventGalaryItemModel model)
        {
            try
            {
                var user = new EDM.EventGalary()
                {
                    EventId = model.EventId,
                    TypeId = model.TypeId,
                    Url = model.Url,
                    CreatedBy = model.CreatedBy,
                    CreatedOn = UnixTimeBaseClass.UnixTimeNow
                };

                _db.EventGalaries.Add(user);
                _db.SaveChanges();

                return user.EventGalaryId;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("EventGalary - Add - " + json, ex);
                throw;
            }

        }

        public bool Delete(int eventGalaryId)
        {
            try
            {
                var user = _db.EventGalaries.First(x => x.EventGalaryId == eventGalaryId);

                _db.EventGalaries.Remove(user);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("EventGalary - Delete" + eventGalaryId, ex);
                throw;
            }

        }

        public bool DeleteByEvent(int eventId)
        {
            try
            {
                var items = _db.EventGalaries.Where(x => x.EventId == eventId);

                _db.EventGalaries.RemoveRange(items);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("EventGalary - DeleteByEvent" + eventId, ex);
                throw;
            }

        }
    }
}
