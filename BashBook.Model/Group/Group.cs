using System.Collections.Generic;

namespace BashBook.Model.Group
{
    public class GroupModel
    {
        public int GroupId { get; set; }
        public int ParentGroupId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
    }

    public class GroupPreviewModel
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Message { get; set; }
        public int UnReadPostCount { get; set; }
    }

    public class ManageGroupModel
    {
        public int GroupId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Message { get; set; }
        public List<UserRoleModel> Users { get; set; }
        public List<int> Groups { get; set; }
        public int UserId { get; set; }
    }

    public class UserRoleModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
