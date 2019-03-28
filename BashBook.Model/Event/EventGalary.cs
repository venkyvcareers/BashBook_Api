using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashBook.Model.Event
{
    public class EventGalaryItemModel
    {
        public int EventGalaryId { get; set; }
        public int EventId { get; set; }
        public int TypeId { get; set; }
        public string Url { get; set; }
        public int CreatedBy { get; set; }
        public long CreatedOn { get; set; }
    }

    public class EventGalaryItemsModel
    {
        public int EventId { get; set; }
        public List<GalaryItemModel> Items { get; set; }
        public int CreatedBy { get; set; }
    }

    public class GalaryItemModel
    {
        public int TypeId { get; set; }
        public string Url { get; set; }
    }


}
