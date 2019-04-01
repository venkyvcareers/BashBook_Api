using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BashBook.DAL.EDM;
using BashBook.Model.Lookup;

namespace BashBook.CMS.Controllers.Admin.Cricket
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class GroundController : Controller
    {
        private BashBookEntities db = new BashBookEntities();

        // GET: Ground
        public ActionResult Index()
        {
            var grounds = db.Grounds.Include(g => g.LookUpValue);
            return View(grounds.ToList());
        }

        // GET: Ground/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ground ground = db.Grounds.Find(id);
            if (ground == null)
            {
                return HttpNotFound();
            }
            return View(ground);
        }

        // GET: Ground/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.LookUpValues.Where(x=>x.ParentId == (int)Lookups.Parents.Country), "LookupValueId", "Name");
            return View();
        }

        // POST: Ground/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ground ground)
        {
            if (ModelState.IsValid)
            {
                db.Grounds.Add(ground);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.Country), "LookupValueId", "Name", ground.CountryId);
            return View(ground);
        }

        // GET: Ground/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ground ground = db.Grounds.Find(id);
            if (ground == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.Country), "LookupValueId", "Name", ground.CountryId);
            return View(ground);
        }

        // POST: Ground/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ground ground)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ground).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.Country), "LookupValueId", "Name", ground.CountryId);
            return View(ground);
        }

        // GET: Ground/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ground ground = db.Grounds.Find(id);
            if (ground == null)
            {
                return HttpNotFound();
            }
            return View(ground);
        }

        // POST: Ground/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ground ground = db.Grounds.Find(id);
            db.Grounds.Remove(ground);
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
