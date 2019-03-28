using System.Collections.Generic;
using BashBook.DAL.Event;
using BashBook.Model.Event;

namespace BashBook.BAL.Event
{
    public class EventUserOperation : BaseBusinessAccessLayer
    {
        readonly EventUserRepository _event = new EventUserRepository();

        public List<EventUserModel> GetAll(int eventId)
        {
            return _event.GetAll(eventId);
        }

        public int Add(EventUserModel model)
        {
            return _event.Add(model);
        }

        public List<int> AddRange(EventUsersModel model)
        {
            var result = new List<int>();
            foreach (var user in model.UserList)
            {
               var eventUserId = _event.Add(new EventUserModel()
                {
                    EventId = model.EventId,
                    UserId = user,
                    CreatedBy = model.CreatedBy
                });

                result.Add(eventUserId);
            }

            return result;
        }

        public bool Delete(int eventUserId)
        {
            return _event.Delete(eventUserId);
        }

        public bool Delete(int eventId, int userId)
        {
            return _event.Delete(eventId, userId);
        }
    }
    
}
