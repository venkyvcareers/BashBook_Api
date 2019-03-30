using System.Collections.Generic;

namespace BashBook.Model.Event
{
    public class EventUserModel
    {
        public int EventUserId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int CreatedBy { get; set; }
    }

    public class EventUsersModel
    {
        public int EventId { get; set; }
        public List<int> UserList { get; set; }
        public int CreatedBy { get; set; }
    }

}
