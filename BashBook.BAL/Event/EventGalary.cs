using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashBook.DAL.Event;
using BashBook.Model.Event;

namespace BashBook.BAL.Event
{
    public class EventGalaryOperation : BaseBusinessAccessLayer
    {
        readonly EventGalaryRepository _event = new EventGalaryRepository();

        public List<EventGalaryItemModel> GetAll(int eventId)
        {
            return _event.GetAll(eventId);
        }

        public int Add(EventGalaryItemModel model)
        {
            if (!string.IsNullOrEmpty(model.Url))
            {
                model.Url = Cdn.Base64ToImageUrl(model.Url);
            }

            return _event.Add(model);
        }

        public List<int> AddRange(EventGalaryItemsModel model)
        {
            var result = new List<int>();
            foreach (var item in model.Items)
            {
                if (!string.IsNullOrEmpty(item.Url))
                {
                    item.Url = Cdn.Base64ToImageUrl(item.Url);
                }
                var eventGalaryId = _event.Add(new EventGalaryItemModel()
                {
                    EventId = model.EventId,
                    Url = item.Url,
                    TypeId = item.TypeId,
                    CreatedBy = model.CreatedBy
                });

                result.Add(eventGalaryId);
            }

            return result;
        }

        public bool Delete(int eventGalaryId)
        {
            return _event.Delete(eventGalaryId);
        }

        public bool DeleteByEvent(int eventId)
        {
            return _event.DeleteByEvent(eventId);
        }
    }
}
