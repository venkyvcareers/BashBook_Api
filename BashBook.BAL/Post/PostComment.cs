using System.Collections.Generic;
using BashBook.DAL.Post;
using BashBook.Model.Post;

namespace BashBook.BAL.Post
{
    public class PostCommentOperation : BaseBusinessAccessLayer
    {
        readonly PostCommentRepository _postComment = new PostCommentRepository();

        public List<PostViewCommentModel> GetAll(int postId)
        {
            return _postComment.GetAll(postId);
        }

        public int Add(PostCommentModel model)
        {
            return _postComment.Add(model);
        }
    }
}
