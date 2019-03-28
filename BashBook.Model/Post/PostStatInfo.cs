namespace BashBook.Model.Post
{
    public class PostStatInfoModel
    {
        public int PostStatInfoId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public bool IsLiked { get; set; }
        public bool IsRead { get; set; }
    }

    public class PostUserModel
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}
