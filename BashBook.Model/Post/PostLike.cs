namespace BashBook.Model.Post
{
    public class PostLikeModel
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public long LikedOn { get; set; }

    }
}
