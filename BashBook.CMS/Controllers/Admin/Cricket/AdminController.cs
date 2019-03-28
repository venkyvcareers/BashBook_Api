using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BashBook.CMS.Controllers.Admin.Cricket
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SendSMS()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendSMS(string mobile)
        {
            const string accountSid = "AC85fd54c5b2f332430e38a7ab00093a19";
            const string authToken = "7ebd79ca2a1919277434839a931f5507";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "This is the ship that made the Kessel Run in fourteen parsecs?",
                from: new Twilio.Types.PhoneNumber("+12023359499"),
                to: new Twilio.Types.PhoneNumber("+917207276789")
            );

            // Console.WriteLine(message.Sid);

            return View();
        }
    }
}