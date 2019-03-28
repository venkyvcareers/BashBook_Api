using System.Collections.Generic;
using BashBook.BAL.Post;
using BashBook.Model.Post;
using System.Web.Http;

namespace BashBook.API.Controllers.Post
{
    //[Authorize]
    [RoutePrefix("api/Post")]
    public class PostController : BaseController
    {
        readonly PostOperation _post = new PostOperation();

        [HttpPost]
        [Route("GetAll")]
        public List<PostViewModel> GetAll(GetPostModel model)
        {
            return _post.GetAllByEntity(model);
        }

        [HttpGet]
        [Route("GetAll/{groupId}")]
        public List<PostViewModel> GetAll(int groupId)
        {
            var userId = GetUserId(User.Identity.Name);
            return _post.GetAll(groupId, userId);
        }

        [HttpGet]
        [Route("GetAll/{groupId}/{lastPostId}")]
        public List<PostViewModel> GetAll(int groupId, int lastPostId)
        {
            var userId = GetUserId(User.Identity.Name);
            return _post.GetAll(groupId, lastPostId, userId);
        }

        [HttpGet]
        [Route("GetById/{postId}")]
        public PostViewModel GetById(int postId)
        {
            return _post.GetById(postId);
        }

        [HttpPost]
        [Route("Add")]
        public int Add(PostModel model)
        {
            return _post.Add(model);
        }

        [HttpDelete]
        [Route("Delete/{postId}")]
        public bool Delete(int postId)
        {
            return _post.Delete(postId);
        }

        [HttpDelete]
        [Route("DeleteByEntity/{entityTypeId}/{entityId}")]
        public bool DeleteByEntity(int entityTypeId, int entityId)
        {
            return _post.DeleteByEntity(entityTypeId, entityId);
        }
    }
}
