namespace BashBook.Model.Post
{
    public class PostCommentModel
    {
        public int PostId { get; set; }
        public int TypeId { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public int CommentedBy { get; set; }
    }

    public class PostViewCommentModel
    {
        public int PostCommentId { get; set; }
        public int PostId { get; set; }
        public int TypeId { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public int CommentedUserId { get; set; }
        public string CommentedUserName { get; set; }
        public long CommentedOn { get; set; }
    }
}
