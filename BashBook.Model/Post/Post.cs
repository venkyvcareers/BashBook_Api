using BashBook.Model.User;

namespace BashBook.Model.Post
{
    public class PostModel
    {
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
        public int ContentTypeId { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }
        public int PostedBy { get; set; }
    }

    public class PostViewModel
    {
        public int PostId { get; set; }
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
        public int ContentTypeId { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }
        public int LikeCount { get; set; }
        public int ShareCount { get; set; }
        public long PostedOn { get; set; }
        public int PostedBy { get; set; }
        public UserGeneralViewModel UserInfo { get; set; }
    }

    public class GetPostModel
    {
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
        public int UserId { get; set; }
    }
}
