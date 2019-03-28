using System.Collections.Generic;
using System.Web.Http;
using BashBook.BAL.Event;
using BashBook.Model.Event;
using BashBook.Model.Global;

namespace BashBook.API.Controllers.Event
{
    [Authorize]
    [RoutePrefix("api/Event")]
    public class EventController : BaseController
    {
        readonly EventOperation _event = new EventOperation();

        [HttpGet]
        [Route("SelectAllByEntity/{entityTypeId}/{entityId}")]
        public List<EventModel> SelectAllByEntity(int entityTypeId, int entityId)
        {
            return _event.GetAllByEntity(entityTypeId, entityId);
        }

        [HttpGet]
        [Route("SelectById/{postId}")]
        public EventModel SelectById(int postId)
        {
            return _event.GetById(postId);
        }

        [HttpPost]
        [Route("Add")]
        public int Add(ManageEventModel model)
        {
            return _event.Add(model);
        }

        [HttpPost]
        [Route("Edit")]
        public bool Edit(ManageEventModel model)
        {
            return _event.Edit(model);
        }

        [HttpDelete]
        [Route("Delete/{postId}")]
        public bool Delete(int postId)
        {
            return _event.Delete(postId);
        }

        [HttpDelete]
        [Route("DeleteByEntity/{entityTypeId}/{entityId}")]
        public bool DeleteByEntity(int entityTypeId, int entityId)
        {
            return _event.DeleteByEntity(entityTypeId, entityId);
        }

        [HttpPost]
        [Route("UpdateImage")]
        public bool UpdateImage(StringModel model)
        {
            return _event.UpdateImage(model);
        }

        [HttpPost]
        [Route("UpdateMessage")]
        public bool UpdateMessage(StringModel model)
        {
            return _event.UpdateMessage(model);
        }

        [HttpPost]
        [Route("UpdateTitle")]
        public bool UpdateTitle(StringModel model)
        {
            return _event.UpdateTitle(model);
        }
    }
}
