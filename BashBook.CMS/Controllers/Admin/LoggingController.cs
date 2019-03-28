using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BashBook.DAL.EDM;

namespace BashBook.CMS.Controllers.Admin
{
    public class LoggingController : Controller
    {
        private BashBookEntities db = new BashBookEntities();
        // GET: Logging
        public ActionResult Index()
        {
            return View(db.LogInfoes.OrderByDescending(x=>x.Date));
        }
    }
}