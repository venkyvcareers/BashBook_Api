using BashBook.BAL.Post;
using BashBook.Model.Post;
using System.Web.Http;

namespace BashBook.API.Controllers.Post
{
    [Authorize]
    [RoutePrefix("api/PostLike")]
    public class PostLikeController : BaseController
    {
        readonly PostLikeOperation _postLike = new PostLikeOperation();

        [HttpPost]
        [Route("Like")]
        public int Like(PostLikeModel model)
        {
            return _postLike.Like(model);
        }

        [HttpPost]
        [Route("Unlike")]
        public int Unlike(PostLikeModel model)
        {
            return _postLike.Unlike(model);
        }
    }
}
