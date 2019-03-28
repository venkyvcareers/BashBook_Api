using BashBook.DAL.Post;
using BashBook.Model.Post;

namespace BashBook.BAL.Post
{
    public class PostLikeOperation : BaseBusinessAccessLayer
    {
        readonly PostLikeRepository _postLike = new PostLikeRepository();

        public int Like(PostLikeModel model)
        {
            return _postLike.Like(model);
        }

        public int Unlike(PostLikeModel model)
        {
            return _postLike.Unlike(model);
        }
    }
}
