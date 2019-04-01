using System.Collections.Generic;
using BashBook.DAL.Event;
using BashBook.DAL.Group;
using BashBook.DAL.User;
using BashBook.Model.Global;
using BashBook.Model.User;

namespace BashBook.BAL.User
{
    public class UserOperation : BaseBusinessAccessLayer
    {
        readonly UserRepository _user = new UserRepository();
        readonly EventRepository _event = new EventRepository();
        readonly GroupRepository _group = new GroupRepository();


        public UserModel GetById(int userId)
        {
            // throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = "Duplicate categories found" });
            return _user.GetById(userId);
        }

        public List<UserPreviewModel> GetContactPreviewList(int userId)
        {
            return _user.GetContactPreviewList(userId);
        }

        public List<UserPreviewModel> GetSearchList(string key)
        {
            return _user.GetSearchList(key.ToLower());
        }

        

        public UserPreviewModel GetUserInfo(EmailModel model)
        {
            return _user.GetUserInfo(model);
        }

        public UserActivityModel GetActivityList(int userId)
        {
            var result = new UserActivityModel
            {
                Events = _event.GetPreviewList(userId),
                Groups = _group.GetPreviewList(userId),
                Contacts = _user.GetPreviewList(userId)
            };

            return result;
        }

        public bool Update(UserModel model)
        {
            if (!string.IsNullOrEmpty(model.Image))
            {
                model.Image = Cdn.Base64ToImageUrl(model.Image);
            }
            return _user.Update(model);
        }

        public bool UpdateMessage(StringModel model)
        {
            return _user.UpdateMessage(model);
        }

        public string UpdateImage(StringModel model)
        {
            model.Text = Cdn.Base64ToImageUrl(model.Text);
            return _user.UpdateImage(model);
        }

        public UserRegisterValidationModel Register(UserRegisterModel model)
        {
            return _user.Register(model);
        }

        public bool IsMobileExisted(string mobile)
        {
            return _user.IsMobileExisted(mobile);
        }

        public int GetUserId(string email)
        {
            return _user.GetUserId(email);
        }
        public bool Delete(int userId)
        {
            return _user.Delete(userId);
        }
    }
}
