using BashBook.Model.User;
using System;
using System.Collections.Generic;
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
        public List<int> GetAll(int userId)
        {
            var result = (from uc in _db.UserContacts
                          where uc.UserId == userId
                          select uc.ContactId).ToList();

            return result;
        }

        public List<int> GetContacts(int userId, int statusId)
        {
            var result = (from uc in _db.UserContacts
                where uc.UserId == userId && uc.StatusId == statusId
                          select uc.ContactId).ToList();

            return result;
        }

        public List<int> GetSuggestedList(int userId)
        {
            var directFriends = (from uc in _db.UserContacts
                where uc.UserId == userId && uc.StatusId == (int)Lookups.ContactStatuses.Accepted
                select uc.ContactId).ToList();

            var result = directFriends;

            return result;
        }
        public List<int> GetAcceptedList(int userId)
        {
            var result = (from uc in _db.UserContacts
                where uc.UserId == userId && uc.StatusId == (int)Lookups.ContactStatuses.Accepted
                select uc.ContactId).ToList();

            return result;
        }

        public List<int> GetRequestedList(int userId)
        {
            var result = (from uc in _db.UserContacts
                where uc.UserId == userId && uc.StatusId == (int)Lookups.ContactStatuses.Requested
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
