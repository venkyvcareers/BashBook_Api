using System.Collections.Generic;
using System.Web.Http;
using BashBook.BAL.Event;
using BashBook.Model.Event;

namespace BashBook.API.Controllers.Event
{
    [Authorize]
    [RoutePrefix("api/EventUser")]
    public class EventUserController : BaseController
    {
        readonly EventUserOperation _eventUser = new EventUserOperation();

        [HttpGet]
        [Route("GetAll/{eventId}")]
        public List<EventUserModel> GetAll(int eventId)
        {
            return _eventUser.GetAll(eventId);
        }

        [HttpPost]
        [Route("Add")]
        public int Add(EventUserModel model)
        {
            return _eventUser.Add(model);
        }

        [HttpPost]
        [Route("AddRange")]
        public List<int> AddRange(EventUsersModel model)
        {
            return _eventUser.AddRange(model);
        }

        [HttpDelete]
        [Route("Delete/{eventId}")]
        public bool Delete(int eventId)
        {
            return _eventUser.Delete(eventId);
        }

        [HttpDelete]
        [Route("Delete/{eventId}/{userId}")]
        public bool Delete(int eventId, int userId)
        {
            return _eventUser.Delete(eventId, userId);
        }
    }
}
