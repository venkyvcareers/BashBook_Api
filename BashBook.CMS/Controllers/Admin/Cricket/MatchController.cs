using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BashBook.DAL.EDM;
using BashBook.Model.Lookup;

namespace BashBook.CMS.Controllers.Admin.Cricket
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class MatchController : Controller
    {
        private BashBookEntities db = new BashBookEntities();

        // GET: Match
        public ActionResult Index()
        {
            var matches = db.Matches.Include(m => m.LookUpValue).Include(m => m.Team).Include(m => m.Team1).Include(m => m.Tournament);
            return View(matches.ToList());
        }

        // GET: Match/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // GET: Match/Create
        public ActionResult Create()
        {
            GetViewBagData(null);
            return View();
        }

        // POST: Match/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Match match, DateTime startTime)
        {
            match.StartsOn = (long)(startTime.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds;
            if (ModelState.IsValid)
            {
                db.Matches.Add(match);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            GetViewBagData(null);
            return View(match);
        }

        // GET: Match/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }

            ViewBag.existedDate = new DateTime(1970, 1, 1).AddSeconds(match.StartsOn).ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ss");
            GetViewBagData(match);
            return View(match);
        }

        // POST: Match/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Match match, DateTime startTime)
        {
            match.StartsOn = (long)(startTime.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds;
            if (ModelState.IsValid)
            {
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            GetViewBagData(match);
            return View(match);
        }

        // GET: Match/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // POST: Match/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Match match = db.Matches.Find(id);
            db.Matches.Remove(match);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void GetViewBagData(Match match)
        {
            if (match == null)
            {
                ViewBag.StatusId =
                    new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.MatchStatus),
                        "LookupValueId", "Name");
                ViewBag.HomeTeamId = new SelectList(db.Teams, "TeamId", "Name");
                ViewBag.OpponentTeamId = new SelectList(db.Teams, "TeamId", "Name");
                ViewBag.TournamentId = new SelectList(db.Tournaments, "TournamentId", "Name");
                ViewBag.GroundId = new SelectList(db.Grounds, "GroundId", "City");
            }
            else
            {
                ViewBag.StatusId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.MatchStatus), "LookupValueId", "Name", match.StatusId);
                ViewBag.HomeTeamId = new SelectList(db.Teams, "TeamId", "Name", match.HomeTeamId);
                ViewBag.OpponentTeamId = new SelectList(db.Teams, "TeamId", "Name", match.OpponentTeamId);
                ViewBag.TournamentId = new SelectList(db.Tournaments, "TournamentId", "Name", match.TournamentId);
                ViewBag.GroundId = new SelectList(db.Grounds, "GroundId", "City", match.GroundId);
            }

        }
    }
}
