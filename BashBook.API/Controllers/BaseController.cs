using System.Web.Http;
using BashBook.BAL.User;

namespace BashBook.API.Controllers
{
    public class BaseController : ApiController
    {
        readonly UserOperation _user = new UserOperation();
        public int GetUserId()
        {
            return _user.GetUserId(User.Identity.Name);
        }
    }
}
