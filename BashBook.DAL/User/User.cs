using BashBook.Model.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BashBook.Utility;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.DAL.Global;
using BashBook.Model.Global;
using BashBook.Model.Lookup;

namespace BashBook.DAL.User
{
    public class UserRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public UserModel GetById(int userId)
        {
            try
            {
                var result = (from u in _db.Users
                              where u.UserId == userId
                              select new UserModel
                              {
                                  UserId = u.UserId,
                                  Mobile = u.Mobile,
                                  Email = u.Email,
                                  GenderId = u.GenderId ?? 0,
                                  LastName = u.LastName,
                                  FirstName = u.FirstName,
                                  Message = u.Message,
                                  Address = u.Address,
                                  Image = u.Image,
                                  DateOfBirth = u.DateOfBirth??0
                              }).First();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("User - GetAllUserList - " + userId, ex);
                throw;
            }

        }

        public List<UserPreviewModel> GetContactPreviewList(int userId)
        {
            var result = (from u in _db.Users
                where u.UserId != userId
                select new UserPreviewModel()
                {
                    UserId = u.UserId,
                    Image = u.Image,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                }).ToList();
            return result;
        }

        public UserPreviewModel GetUserInfo(EmailModel model)
        {
            var result = (from u in _db.Users
                          where u.Email == model.Email
                          select new UserPreviewModel()
                          {
                              UserId = u.UserId,
                              Image = u.Image,
                              FirstName = u.FirstName,
                              LastName = u.LastName
                          }).FirstOrDefault();
            return result;
        }
        public UserPreviewModel GetPreviewById(int userId)
        {
            try
            {
                var result = (from u in _db.Users
                              where u.UserId == userId
                              select new UserPreviewModel
                              {
                                  UserId = u.UserId,
                                  LastName = u.LastName,
                                  FirstName = u.FirstName,
                                  Image = u.Image
                              }).First();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("User - GetPreviewById", ex);
                throw;
            }
        }

        public List<UserPreviewModel> GetPreviewsByList(List<int> users)
        {
            try
            {
                var result = (from u in _db.Users
                              where users.Contains(u.UserId)
                              select new UserPreviewModel
                              {
                                  UserId = u.UserId,
                                  LastName = u.LastName,
                                  FirstName = u.FirstName,
                                  Image = u.Image
                              }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(users);
                Log.Error("User - GetPreviewsByList - " + json, ex);
                throw;
            }
        }

        public List<EntityPreviewModel> GetPreviewList(int userId)
        {
            int entityType = (int)Lookups.EntityTypes.User;
            var result = (from uc in _db.UserContacts
                          from u in _db.Users
                          where uc.UserId == userId && uc.ContactId == u.UserId
                          select new EntityPreviewModel
                          {
                              Name = u.FirstName + " " + u.LastName,
                              EntityId = uc.ContactId,
                              Image = u.Image,
                              Message = u.Message,
                              EntityTypeId = entityType,
                              LastMessage = (from p in _db.Posts
                                             where p.EntityTypeId == entityType &&
                                                   ((p.EntityId == userId && p.PostedBy == uc.ContactId) || (p.EntityId == uc.ContactId && p.PostedBy == userId))
                                             select new LastMessageModel
                                             {
                                                 Text = p.Text,
                                                 ContentTypeId = p.ContentTypeId,
                                                 PostedOn = p.PostedOn
                                             }).OrderByDescending(x => x.PostedOn).FirstOrDefault()
                          }).ToList();

            return result;
        }
        public UserRegisterValidationModel Register(UserRegisterModel model)
        {
            try
            {
                var result = new UserRegisterValidationModel();

                //result.UserName = _db.Users.Any(u => u.UserName.ToLower() == model.UserName.ToLower());
                result.Mobile = _db.Users.Any(u => u.Mobile.ToLower() == model.Mobile.ToLower());
                result.Email = _db.Users.Any(u => u.Email.ToLower() == model.Email.ToLower());

                if (result.Email || result.Mobile)
                {
                    return result;
                }

                var user = new EDM.User()
                {
                    Mobile = model.Mobile,
                    Email = model.Email,
                    //UserName = model.UserName,
                    //FirstName = model.FirstName,
                    //LastName = model.LastName,
                    CreatedOn = UnixTimeBaseClass.UnixTimeNow
                };

                _db.Users.Add(user);
                _db.SaveChanges();

                result.UserId = user.UserId;
                return result;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("User - Register - " + json, ex);
                throw;
            }
        }
        public bool Update(UserModel model)
        {
            try
            {
                var user = _db.Users.First(x => x.UserId == model.UserId);

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Message = model.Message;
                user.Address = model.Address;
                user.Image = model.Image;
                user.DateOfBirth = model.DateOfBirth;
                user.GenderId = model.GenderId;
                user.LastUpdatedOn = UnixTimeBaseClass.UnixTimeNow;

                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("User - Update - " + json, ex);
                throw;
            }
        }
        public bool UpdateMessage(StringModel model)
        {
            try
            {
                var user = _db.Users.First(x => x.UserId == model.Id);

                user.Message = model.Text;
                user.LastUpdatedOn = UnixTimeBaseClass.UnixTimeNow;

                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("User - Update Status - " + json, ex);
                throw;
            }
        }
        public string UpdateImage(StringModel model)
        {
            try
            {
                var user = _db.Users.First(x => x.UserId == model.Id);

                user.Image = model.Text;
                user.LastUpdatedOn = UnixTimeBaseClass.UnixTimeNow;

                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();

                return user.Image;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("User - Update Profile Pic- " + json, ex);
                throw;
            }
        }
        public bool IsMobileExisted(string mobile)
        {
            try
            {
                return _db.Users.Count(x => x.Mobile == mobile) > 0;
            }
            catch (Exception ex)
            {
                Log.Error("User - IsMobileExisted - " + mobile, ex);
                throw;
            }
        }

        public int GetUserId(string email)
        {
            try
            {
                return _db.Users.First(x => x.Email == email).UserId;
            }
            catch (Exception ex)
            {
                Log.Error("User - GetUserId - " + email, ex);
                throw;
            }
        }
        public bool Delete(int userId)
        {
            try
            {
                var user = _db.Users.First(x => x.UserId == userId);
                if (user != null)
                {
                    _db.Users.Remove(user);
                    _db.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Log.Error("User - Delete - " + userId, ex);
                throw;
            }
        }
    }
}
