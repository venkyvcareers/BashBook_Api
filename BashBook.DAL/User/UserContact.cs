using BashBook.Model.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BashBook.DAL.EDM;
using BashBook.DAL.Global;
using BashBook.Model.Lookup;
using BashBook.Utility;

namespace BashBook.DAL.User
{
    public class UserContactRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public List<UserPreviewModel> GetRequestedList(int userId, int statusId)
        {
            var result = (from uc in _db.UserContacts
                          from u in _db.Users
                          where uc.StatusId == statusId && uc.ContactId == userId && u.UserId == uc.UserId
                          select new UserPreviewModel()
                          {
                              UserId = uc.User.UserId,
                              Image = uc.User.Image,
                              FirstName = uc.User.FirstName,
                              LastName = uc.User.LastName
                          }).Take(20).ToList();

            return result;
        }

        public List<UserPreviewModel> GetAcceptedList(int userId, int statusId)
        {
            var result = (from uc in _db.UserContacts
                          from u in _db.Users
                          where uc.StatusId == statusId && (uc.ContactId == userId || uc.UserId == userId) && (u.UserId == uc.UserId || u.UserId == uc.ContactId) && u.UserId != userId
                          select new UserPreviewModel()
                          {
                              UserId = u.UserId,
                              Image = u.Image,
                              FirstName = u.FirstName,
                              LastName = u.LastName
                          }).Take(20).ToList();

            return result;
        }

        public List<UserPreviewModel> GetContactByStatus(int userId, int statusId)
        {
            var result = (from uc in _db.UserContacts
                          where uc.StatusId == statusId && (uc.ContactId == userId || uc.UserId == userId)
                          select new UserPreviewModel()
                          {
                              UserId = uc.User.UserId,
                              Image = uc.User.Image,
                              FirstName = uc.User.FirstName,
                              LastName = uc.User.LastName
                          }).Take(20).ToList();

            return result;
        }

        public List<UserPreviewModel> GetContacts(int userId, string key)
        {
            var userContacts = _db.UserContacts.Where(x => x.UserId == userId).Select(x => x.ContactId).ToList();
            userContacts.Add(userId);

            var result = (from u in _db.Users
                          where !userContacts.Contains(u.UserId)
                            && (key == "" || u.FirstName.ToLower().Contains(key) || u.LastName.ToLower().Contains(key))
                          select new UserPreviewModel()
                          {
                              UserId = u.UserId,
                              Image = u.Image,
                              FirstName = u.FirstName,
                              LastName = u.LastName
                          }).Take(20).ToList();

            return result;
        }

        public List<int> GetContactIds(int userId, int statusId)
        {
            var result = (from uc in _db.UserContacts
                          where (uc.ContactId == userId || uc.UserId == userId) && uc.StatusId == statusId
                          select uc.ContactId).ToList();

            return result;
        }

        public int Add(UserContactModel model)
        {
            var contact = new UserContact()
            {
                UserId = model.UserId,
                ContactId = model.ContactId,
                StatusId = model.StatusId,
                CreatedOn = UnixTimeBaseClass.UnixTimeNow
            };

            _db.UserContacts.Add(contact);
            _db.SaveChanges();

            return contact.UserContactId;
        }

        public bool Update(UserContactModel model)
        {
            var userContact =
                _db.UserContacts.First(x => x.UserId == model.UserId && x.ContactId == model.ContactId);

            userContact.StatusId = model.StatusId;
            userContact.LastUpdatedOn = UnixTimeBaseClass.UnixTimeNow;
            _db.Entry(userContact).State = EntityState.Modified;
            _db.SaveChanges();

            return true;
        }

        public bool Delete(int userOccationId)
        {
            try
            {
                var userOccation = _db.UserOccations.First(x => x.UserOccationId == userOccationId);

                _db.UserOccations.Remove(userOccation);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("User Occation - Delete - " + userOccationId, ex);
                throw;
            }
        }
    }
}
