using System;
using BashBook.Model.Post;
using System.Data.Entity;
using System.Linq;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.Utility;

namespace BashBook.DAL.Post
{
    public class PostLikeRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();
        public int Like(PostLikeModel model)
        {
            try
            {
                var postLike = new PostLike()
                {
                    PostId = model.PostId,
                    Status = true,
                    LikedBy = model.UserId,
                    LikedOn = UnixTimeBaseClass.UnixTimeNow
                };

                _db.PostLikes.Add(postLike);
                _db.SaveChanges();

                return postLike.PostLikeId;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Post Like - Add- " + json, ex);
                throw;
            }
        }

        public int Unlike(PostLikeModel model)
        {
            try
            {
                var postLike = _db.PostLikes.First(x => x.PostId == model.PostId && x.LikedBy == model.UserId);

                postLike.Status = false;
                postLike.LastUpdatedOn = UnixTimeBaseClass.UnixTimeNow;

                _db.Entry(postLike).State = EntityState.Modified;

                _db.SaveChanges();

                return postLike.PostLikeId;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Post Comment - Edit- " + json, ex);
                throw;
            }
        }
    }
}
