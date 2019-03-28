using System.Collections.Generic;
using BashBook.DAL.User;
using BashBook.Model.User;

namespace BashBook.BAL.User
{
    public class UserOccationOperation : BaseBusinessAccessLayer
    {
        readonly UserOccationRepository _userOccation = new UserOccationRepository();

        public List<UserOccationViewModel> GetAll(int userId)
        {
            return _userOccation.GetAll(userId);
        }

        public int Add(UserOccationModel model)
        {
            return _userOccation.Add(model);
        }

        public bool Edit(UserOccationModel model)
        {
            return _userOccation.Edit(model);
        }

        public bool Delete(int userOccationId)
        {
            return _userOccation.Delete(userOccationId);
        }
    }
}
