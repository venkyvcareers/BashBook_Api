using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BashBook.DAL.EDM;

namespace BashBook.CMS.Controllers.Admin.Cricket
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class TournamentTeamController : Controller
    {
        private BashBookEntities db = new BashBookEntities();

        // GET: TournamentTeam
        public ActionResult Index()
        {
            var tournamentTeams = db.TournamentTeams.Include(t => t.Team).Include(t => t.Tournament);
            return View(tournamentTeams.ToList());
        }

        // GET: TournamentTeam/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TournamentTeam tournamentTeam = db.TournamentTeams.Find(id);
            if (tournamentTeam == null)
            {
                return HttpNotFound();
            }
            return View(tournamentTeam);
        }

        // GET: TournamentTeam/Create
        public ActionResult Create()
        {
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "Name");
            ViewBag.TournamentId = new SelectList(db.Tournaments, "TournamentId", "Name");
            return View();
        }

        // POST: TournamentTeam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TournamentTeamId,TournamentId,TeamId")] TournamentTeam tournamentTeam)
        {
            if (ModelState.IsValid)
            {
                db.TournamentTeams.Add(tournamentTeam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "Name", tournamentTeam.TeamId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "TournamentId", "Name", tournamentTeam.TournamentId);
            return View(tournamentTeam);
        }

        // GET: TournamentTeam/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TournamentTeam tournamentTeam = db.TournamentTeams.Find(id);
            if (tournamentTeam == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "Name", tournamentTeam.TeamId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "TournamentId", "Name", tournamentTeam.TournamentId);
            return View(tournamentTeam);
        }

        // POST: TournamentTeam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TournamentTeamId,TournamentId,TeamId")] TournamentTeam tournamentTeam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tournamentTeam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "Name", tournamentTeam.TeamId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "TournamentId", "Name", tournamentTeam.TournamentId);
            return View(tournamentTeam);
        }

        // GET: TournamentTeam/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TournamentTeam tournamentTeam = db.TournamentTeams.Find(id);
            if (tournamentTeam == null)
            {
                return HttpNotFound();
            }
            return View(tournamentTeam);
        }

        // POST: TournamentTeam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TournamentTeam tournamentTeam = db.TournamentTeams.Find(id);
            db.TournamentTeams.Remove(tournamentTeam);
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
    }
}
