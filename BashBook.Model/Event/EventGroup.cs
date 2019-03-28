using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashBook.Model.Event
{
    public class EventGroupModel
    {
        public int EventGroupId { get; set; }
        public int CreatedBy { get; set; }
        public int EventId { get; set; }
        public int GroupId { get; set; }
    }
}
