using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.Model.Post;
using BashBook.Utility;

namespace BashBook.DAL.Post
{
    public class PostCommentRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();
        public List<PostViewCommentModel> GetAll(int postId)
        {
            var result = (from pc in _db.PostComments
                          where pc.PostId == postId
                          select new PostViewCommentModel
                          {
                              PostId = pc.PostId,
                              Text = pc.Text,
                              Url = pc.Url,
                              TypeId = pc.TypeId,
                              CommentedOn = pc.CommentedOn,
                              CommentedUserId = pc.CommentedBy,
                              CommentedUserName = pc.User.FirstName + " " + pc.User.LastName,
                              PostCommentId = pc.PostCommentId
                          }).ToList();
            return result;
        }

        public int Add(PostCommentModel model)
        {
            try
            {
                var postComment = new PostComment()
                {
                    PostId = model.PostId,
                    CommentedBy = model.CommentedBy,
                    Text = model.Text,
                    TypeId = model.TypeId,
                    Url = model.Url,
                    CommentedOn = UnixTimeBaseClass.UnixTimeNow,
                };

                _db.PostComments.Add(postComment);
                _db.SaveChanges();

                return postComment.PostCommentId;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Post Comment - Add- " + json, ex);
                throw;
            }
        }
    }
}
