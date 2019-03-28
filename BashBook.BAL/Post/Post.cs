using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashBook.DAL.Post;
using BashBook.Model.Post;

namespace BashBook.BAL.Post
{
    public class PostOperation : BaseBusinessAccessLayer
    {
        readonly PostRepository _post = new PostRepository();

        public List<PostViewModel> GetAllByEntity(GetPostModel model)
        {
            return _post.GetAllByEntity(model);
        }

        public List<PostViewModel> GetAll(int groupId, int userId)
        {
            return _post.GetAll(groupId, userId);
        }

        public List<PostViewModel> GetAll(int groupId, int lastPostId, int userId)
        {
            return _post.GetAll(groupId, lastPostId, userId);
        }
        public PostViewModel GetById(int postId)
        {
            return _post.GetById(postId);
        }
        public int Add(PostModel model)
        {
            model.Url = Cdn.Base64ToImageUrl(model.Url);
            return _post.Add(model);
        }

        public bool Delete(int postId)
        {
            return _post.Delete(postId);
        }

        public bool DeleteByEntity(int entityTypeId, int entityId)
        {
            return _post.DeleteByEntity(entityTypeId, entityId);
        }
    }
}
