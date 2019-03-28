using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BashBook.DAL.EDM;
using BashBook.Model.Lookup;

namespace BashBook.CMS.Controllers.Admin.Cricket
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class PlayerController : Controller
    {
        private BashBookEntities db = new BashBookEntities();

        // GET: Player
        public ActionResult Index()
        {
            var players = db.Players.Include(p => p.LookUpValue).Include(p => p.LookUpValue1);
            return View(players.ToList());
        }

        // GET: Player/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.Country), "LookupValueId", "Name");
            ViewBag.PlayerRoleId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.PlayerRole), "LookupValueId", "Name");
            return View();
        }

        // POST: Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlayerId,CountryId,PlayerRoleId,Name,Image")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.Country), "LookupValueId", "Name");
            ViewBag.PlayerRoleId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.PlayerRole), "LookupValueId", "Name");
            return View(player);
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.Country), "LookupValueId", "Name");
            ViewBag.PlayerRoleId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.PlayerRole), "LookupValueId", "Name");
            return View(player);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerId,CountryId,PlayerRoleId,Name,Image")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.Country), "LookupValueId", "Name");
            ViewBag.PlayerRoleId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.PlayerRole), "LookupValueId", "Name");
            return View(player);
        }

        // GET: Player/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
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
