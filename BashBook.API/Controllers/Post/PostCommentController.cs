using System.Collections.Generic;
using BashBook.BAL.Post;
using System.Web.Http;
using BashBook.Model.Post;

namespace BashBook.API.Controllers.Post
{
    [Authorize]
    [RoutePrefix("api/PostComment")]
    public class PostCommentController : BaseController
    {
        readonly PostCommentOperation _postComment = new PostCommentOperation();

        [HttpGet]
        [Route("GetAll/{postId}")]
        public List<PostViewCommentModel> GetAll(int postId)
        {
            return _postComment.GetAll(postId);
        }
        [HttpPost]
        [Route("Add")]
        public int Add(PostCommentModel model)
        {
            return _postComment.Add(model);
        }
    }
}
