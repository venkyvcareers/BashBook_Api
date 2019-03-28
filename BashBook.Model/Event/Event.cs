using System.Collections.Generic;
using BashBook.Model.Group;

namespace BashBook.Model.Event
{
    public class EventModel
    {
        public int EventId { get; set; }
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
        public int OccationId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Message { get; set; }
        public long DateTime { get; set; }
        public int CreatedBy { get; set; }
        public long CreatedOn { get; set; }
    }

    public class ManageEventModel
    {
        public int EventId { get; set; }
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
        public int OccationId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Message { get; set; }
        public long DateTime { get; set; }
        public List<UserRoleModel> Users { get; set; }
        public List<int> Groups { get; set; }
        public int UserId { get; set; }
    }
}
