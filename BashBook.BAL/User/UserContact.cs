using System.Collections.Generic;
using BashBook.DAL.User;
using BashBook.Model.Lookup;
using BashBook.Model.User;

namespace BashBook.BAL.User
{
    public class UserContactOperation : BaseBusinessAccessLayer
    {
        readonly UserContactRepository _userContact = new UserContactRepository();
        readonly UserOperation _user = new UserOperation();


        public List<UserPreviewModel> GetContactPreviewList(int userId)
        {
            return _user.GetContactPreviewList(userId);
        }

        public List<UserPreviewModel> GetSearchList(int userId, string key)
        {
            key = string.IsNullOrEmpty(key) ? "" : key.ToLower();
            return _userContact.GetContacts(userId, key);
        }

        public List<UserPreviewModel> GetRequestedList(int userId)
        {
            return _userContact.GetRequestedList(userId, (int)Lookups.ContactStatuses.Requested);
        }

        public List<UserPreviewModel> GetAcceptedList(int userId)
        {
            return _userContact.GetAcceptedList(userId, (int)Lookups.ContactStatuses.Accepted);
        }

        public int AddFriendRequest(int contactId, int userId)
        {
            var userContact = new UserContactModel()
            {
                StatusId = (int)Lookups.ContactStatuses.Requested,
                UserId = userId,
                ContactId = contactId,
            };
            return _userContact.Add(userContact);
        }

        public bool AcceptFriendRequest(int contactId, int userId)
        {
            var userContact = new UserContactModel()
            {
                StatusId = (int)Lookups.ContactStatuses.Accepted,
                UserId = contactId,
                ContactId = userId,
            };
            return _userContact.Update(userContact);
        }
    }
}
