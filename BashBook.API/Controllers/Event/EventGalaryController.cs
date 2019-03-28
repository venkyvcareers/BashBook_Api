using System.Collections.Generic;
using System.Web.Http;
using BashBook.BAL.Event;
using BashBook.Model.Event;

namespace BashBook.API.Controllers.Event
{
    [Authorize]
    [RoutePrefix("api/EventGalary")]
    public class EventGalaryController : BaseController
    {
        readonly EventGalaryOperation _eventUser = new EventGalaryOperation();

        [HttpGet]
        [Route("GetAll/{eventId}")]
        public List<EventGalaryItemModel> GetAll(int eventId)
        {
            return _eventUser.GetAll(eventId);
        }

        [HttpPost]
        [Route("Add")]
        public int Add(EventGalaryItemModel model)
        {
            return _eventUser.Add(model);
        }

        [HttpPost]
        [Route("AddRange")]
        public List<int> AddRange(EventGalaryItemsModel model)
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
        [Route("DeleteByEvent/{eventId}")]
        public bool DeleteByEvent(int eventId)
        {
            return _eventUser.DeleteByEvent(eventId);
        }
    }
}
