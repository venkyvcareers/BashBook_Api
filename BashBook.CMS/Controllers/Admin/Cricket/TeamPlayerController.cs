using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BashBook.DAL.EDM;

namespace BashBook.CMS.Controllers.Admin.Cricket
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class TeamPlayerController : Controller
    {
        private BashBookEntities db = new BashBookEntities();

        // GET: TeamPlayer
        public ActionResult Index()
        {
            var teamPlayers = db.TeamPlayers.Include(t => t.Player).Include(t => t.Team);
            return View(teamPlayers.ToList());
        }

        // GET: TeamPlayer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamPlayer teamPlayer = db.TeamPlayers.Find(id);
            if (teamPlayer == null)
            {
                return HttpNotFound();
            }
            return View(teamPlayer);
        }

        // GET: TeamPlayer/Create
        public ActionResult Create()
        {
            ViewBag.PlayerId = new SelectList(db.Players, "PlayerId", "Name");
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "Name");
            return View();
        }

        // POST: TeamPlayer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamPlayerId,TeamId,PlayerId")] TeamPlayer teamPlayer)
        {
            if (ModelState.IsValid)
            {
                db.TeamPlayers.Add(teamPlayer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlayerId = new SelectList(db.Players, "PlayerId", "Name", teamPlayer.PlayerId);
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "Name", teamPlayer.TeamId);
            return View(teamPlayer);
        }

        // GET: TeamPlayer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamPlayer teamPlayer = db.TeamPlayers.Find(id);
            if (teamPlayer == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlayerId = new SelectList(db.Players, "PlayerId", "Name", teamPlayer.PlayerId);
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "Name", teamPlayer.TeamId);
            return View(teamPlayer);
        }

        // POST: TeamPlayer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamPlayerId,TeamId,PlayerId")] TeamPlayer teamPlayer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamPlayer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlayerId = new SelectList(db.Players, "PlayerId", "Name", teamPlayer.PlayerId);
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "Name", teamPlayer.TeamId);
            return View(teamPlayer);
        }

        // GET: TeamPlayer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamPlayer teamPlayer = db.TeamPlayers.Find(id);
            if (teamPlayer == null)
            {
                return HttpNotFound();
            }
            return View(teamPlayer);
        }

        // POST: TeamPlayer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamPlayer teamPlayer = db.TeamPlayers.Find(id);
            db.TeamPlayers.Remove(teamPlayer);
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
