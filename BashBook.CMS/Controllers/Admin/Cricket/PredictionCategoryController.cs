using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BashBook.DAL.EDM;
using BashBook.Model.Lookup;

namespace BashBook.CMS.Controllers.Admin.Cricket
{
    public class PredictionCategoryController : Controller
    {
        private BashBookEntities db = new BashBookEntities();

        // GET: PredictionCategory
        public ActionResult Index()
        {
            var predictionCategories = db.PredictionCategories.Include(p => p.LookUpValue);
            return View(predictionCategories.ToList());
        }

        // GET: PredictionCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PredictionCategory predictionCategory = db.PredictionCategories.Find(id);
            if (predictionCategory == null)
            {
                return HttpNotFound();
            }
            return View(predictionCategory);
        }

        // GET: PredictionCategory/Create
        public ActionResult Create()
        {
            ViewBag.CategoryTypeId = new SelectList(db.LookUpValues.Where(x=>x.ParentId == (int)Lookups.Parents.PredictionCategoryType), "LookupValueId", "Name");
            return View();
        }

        // POST: PredictionCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PredictionCategoryId,Name,CategoryTypeId,WinPoints,LossPoints,BonusTimes")] PredictionCategory predictionCategory)
        {
            if (ModelState.IsValid)
            {
                db.PredictionCategories.Add(predictionCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryTypeId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.PredictionCategoryType), "LookupValueId", "Name", predictionCategory.CategoryTypeId);
            return View(predictionCategory);
        }

        // GET: PredictionCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PredictionCategory predictionCategory = db.PredictionCategories.Find(id);
            if (predictionCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryTypeId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.PredictionCategoryType), "LookupValueId", "Name", predictionCategory.CategoryTypeId);
            return View(predictionCategory);
        }

        // POST: PredictionCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PredictionCategoryId,Name,CategoryTypeId,WinPoints,LossPoints,BonusTimes")] PredictionCategory predictionCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(predictionCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryTypeId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.PredictionCategoryType), "LookupValueId", "Name", predictionCategory.CategoryTypeId);
            return View(predictionCategory);
        }

        // GET: PredictionCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PredictionCategory predictionCategory = db.PredictionCategories.Find(id);
            if (predictionCategory == null)
            {
                return HttpNotFound();
            }
            return View(predictionCategory);
        }

        // POST: PredictionCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PredictionCategory predictionCategory = db.PredictionCategories.Find(id);
            db.PredictionCategories.Remove(predictionCategory);
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
