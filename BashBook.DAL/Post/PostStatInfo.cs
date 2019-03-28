using System;
using System.Data.Entity;
using BashBook.Model.Post;
using System.Linq;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;

namespace BashBook.DAL.Post
{
    public class PostStatInfoRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public bool AddReadStatus(PostUserModel model)
        {

            try
            {
                if (!_db.PostStatInfoes.Any(x => x.PostId == model.PostId && x.UserId == model.UserId))
                {
                    var newPostStat = new PostStatInfo()
                    {
                        UserId = model.UserId,
                        PostId = model.PostId,
                        IsLiked = false,
                        IsRead = true
                    };

                    _db.PostStatInfoes.Add(newPostStat);
                    _db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Post Like - Add- " + json, ex);
                throw;
            }
        }

        public bool AddLikedStatus(PostUserModel model)
        {

            try
            {
                if (!_db.PostStatInfoes.Any(x => x.PostId == model.PostId && x.UserId == model.UserId))
                {
                    var newPostStat = new PostStatInfo()
                    {
                        UserId = model.UserId,
                        PostId = model.PostId,
                        IsLiked = true,
                        IsRead = true
                    };

                    _db.PostStatInfoes.Add(newPostStat);
                    _db.SaveChanges();
                    return true;
                }

                var postStat = _db.PostStatInfoes.First(x => x.PostId == model.PostId && x.UserId == model.UserId);

                postStat.IsLiked = true;
                postStat.IsRead = true;

                _db.Entry(postStat).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Post Like - Add- " + json, ex);
                throw;
            }
        }
    }
}
