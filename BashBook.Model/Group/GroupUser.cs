using System.Collections.Generic;

namespace BashBook.Model.Group
{
    public class GroupUserModel
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int CreatedBy { get; set; }
    }

    public class GroupBulkEntityModel
    {
        public int GroupId { get; set; }
        public List<int> Users { get; set; }
        public List<int> Groups { get; set; }
        public int CreatedBy { get; set; }
    }

    public class UpdateRoleModel
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }

    public class GroupUserIdModel
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}

