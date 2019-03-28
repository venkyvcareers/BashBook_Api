using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Web;
using BashBook.DAL.EDM;

namespace BashBook.DAL
{
    public class BaseDataAccessLayer
    {

        public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string CurrentUserName => HttpContext.Current.User.Identity.Name;

        public int CurrentUserId
        {
            get
            {
                try
                {
                    string email = HttpContext.Current.User.Identity.Name;
                    BashBookEntities db = new BashBookEntities();
                    return db.Users.First(x => x.Email.ToLower() == email.ToLower()).UserId;
                }
                catch (Exception)
                {
                    return 1;
                    //throw new ReturnCustomExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.NotFound, Message = "Please login again" });
                }
            }
        }

    }
}
