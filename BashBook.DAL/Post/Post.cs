using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.Model.Lookup;
using BashBook.Model.Post;
using BashBook.Model.User;
using BashBook.Utility;

namespace BashBook.DAL.Post
{
    public class PostRepository : BaseDataAccessLayer
    {
        public BashBookEntities _db = new BashBookEntities();
        public List<PostViewModel> GetAllByEntity(GetPostModel model)
        {
            try
            {
                var result = (from p in _db.Posts
                              where (
                                        p.EntityId == model.EntityId &&
                                        p.EntityTypeId == model.EntityTypeId &&
                                        p.PostedBy == model.UserId) || (p.EntityId == model.UserId &&
                                                                        p.EntityTypeId == model.EntityTypeId &&
                                                                        p.PostedBy == model.EntityId)
                              select new PostViewModel
                              {
                                  PostId = p.PostId,
                                  EntityId = p.EntityId,
                                  EntityTypeId = p.EntityTypeId,
                                  PostedBy = p.PostedBy,
                                  PostedOn = p.PostedOn,
                                  Text = p.Text,
                                  ContentTypeId = p.ContentTypeId,
                                  Url = p.Url,
                                  LikeCount = _db.PostLikes.Count(x => x.PostId == p.PostId && x.Status),
                                  ShareCount = _db.PostShares.Count(x => x.PostId == p.PostId)

                              }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Post - GetAllByEntity - " + json, ex);
                throw;
            }

        }

        public List<PostViewModel> GetAll(int groupId, int userId)
        {
            try
            {
                
                var result = (from p in _db.Posts
                              where p.EntityId == groupId && p.EntityTypeId == (int)Lookups.EntityTypes.Group
                              select new PostViewModel
                              {
                                  PostId = p.PostId,
                                  EntityId = p.EntityId,
                                  EntityTypeId = p.EntityTypeId,
                                  PostedBy = p.PostedBy,
                                  PostedOn = p.PostedOn,
                                  Text = p.Text,
                                  ContentTypeId = p.ContentTypeId,
                                  Url = p.Url,
                                  LikeCount = _db.PostStatInfoes.Count(x => x.PostId == p.PostId && x.IsLiked),
                                  ShareCount = _db.PostShares.Count(x => x.PostId == p.PostId),
                                  UserInfo = (from u in _db.Users
                                              where u.UserId == p.PostedBy
                                              select new UserGeneralViewModel()
                                              {
                                                  UserId = u.UserId,
                                                  Image = u.Image,
                                                  FirstName = u.FirstName,
                                                  LastName = u.LastName
                                              }).FirstOrDefault()
                              }).OrderByDescending(x=>x.PostId).Take(20).OrderBy(x=>x.PostId).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Post - GetAll - " + groupId, ex);
                throw;
            }

        }

        public List<PostViewModel> GetAll(int groupId, int lastPostId, int userId)
        {
            try
            {
                var result = (from p in _db.Posts
                              where p.EntityId == groupId
                                    && p.EntityTypeId == (int)Lookups.EntityTypes.Group
                                    && p.PostId > lastPostId
                              select new PostViewModel
                              {
                                  PostId = p.PostId,
                                  EntityId = p.EntityId,
                                  EntityTypeId = p.EntityTypeId,
                                  PostedBy = p.PostedBy,
                                  PostedOn = p.PostedOn,
                                  Text = p.Text,
                                  ContentTypeId = p.ContentTypeId,
                                  Url = p.Url,
                                  LikeCount = _db.PostLikes.Count(x => x.PostId == p.PostId && x.Status),
                                  ShareCount = _db.PostShares.Count(x => x.PostId == p.PostId),
                                  UserInfo = (from u in _db.Users
                                              where u.UserId == p.PostedBy
                                              select new UserGeneralViewModel()
                                              {
                                                  UserId = u.UserId,
                                                  Image = u.Image,
                                                  FirstName = u.FirstName,
                                                  LastName = u.LastName
                                              }).FirstOrDefault()
                              }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Post - GetAll - " + groupId, ex);
                throw;
            }

        }

        public PostViewModel GetById(int postId)
        {
            try
            {
                var result = (from p in _db.Posts
                              where p.PostId == postId
                              select new PostViewModel
                              {
                                  PostId = p.PostId,
                                  EntityId = p.EntityId,
                                  EntityTypeId = p.EntityTypeId,
                                  PostedBy = p.PostedBy,
                                  PostedOn = p.PostedOn,
                                  Text = p.Text,
                                  ContentTypeId = p.ContentTypeId,
                                  Url = p.Url,
                                  LikeCount = _db.PostLikes.Count(x => x.PostId == p.PostId && x.Status),
                                  ShareCount = _db.PostShares.Count(x => x.PostId == p.PostId)

                              }).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Post - SelectById" + postId, ex);
                throw;
            }

        }

        public int Add(PostModel model)
        {
            try
            {
                var post = new EDM.Post()
                {
                    Text = model.Text,
                    ContentTypeId = model.ContentTypeId,
                    Url = model.Url,
                    EntityId = model.EntityId,
                    EntityTypeId = model.EntityTypeId,
                    PostedBy = model.PostedBy,
                    PostedOn = UnixTimeBaseClass.UnixTimeNow
                };

                _db.Posts.Add(post);
                _db.SaveChanges();

                return post.PostId;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Post - Add - " + json, ex);
                throw;
            }

        }

        public bool Delete(int postId)
        {
            try
            {
                var existedPost = _db.Posts.Find(postId);

                if (existedPost != null) _db.Posts.Remove(existedPost);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Post - Delete" + postId, ex);
                throw;
            }

        }

        public bool DeleteByEntity(int entityTypeId, int entityId)
        {
            try
            {
                var existedPosts = _db.Posts.Where(x => x.EntityId == entityId && x.EntityTypeId == entityTypeId).ToList();

                _db.Posts.RemoveRange(existedPosts);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Post - DeleteByEntity - EntityId - " + entityId + ", EntityTypeId - " + entityTypeId, ex);
                throw;
            }

        }
    }
}
