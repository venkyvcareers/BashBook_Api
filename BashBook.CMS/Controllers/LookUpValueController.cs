using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using BashBook.DAL.EDM;

namespace BashBook.CMS.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class LookUpValueController : Controller
    {
        private readonly BashBookEntities _db = new BashBookEntities();
        //private readonly ApiService _api = new ApiService();

        // GET: LookUpValue
        public ActionResult Index(int? parentId)
        {
            if (parentId == null)
            {
                ViewBag.ParentId = 0;
                return View(_db.LookUpValues.Where(x => x.ParentId == 0).ToList().OrderBy(x => x.Name));
            }

            ViewBag.ParentId = parentId;
            return View(_db.LookUpValues.Where(x => x.ParentId == parentId).ToList().OrderBy(x=>x.Name));
        }

        // GET: LookUpValue/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpValue lookUpValue = _db.LookUpValues.Find(id);
            if (lookUpValue == null)
            {
                return HttpNotFound();
            }
            return View(lookUpValue);
        }

        // GET: LookUpValue/Create
        public ActionResult Create(int parentId)
        {
            ViewBag.ParentId = parentId;
            return View();
        }

        // POST: LookUpValue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LookupValueId,ParentId,Name,Code,Description,LogoUrl")] LookUpValue lookUpValue)
        {
            if (ModelState.IsValid)
            {
                //var userId = await _api.Post("/api/User/GetUserId", new { Email = User.Identity.Name });

                lookUpValue.IsActive = true;
                //lookUpValue.CreatedBy = Convert.ToInt32(userId);
                _db.LookUpValues.Add(lookUpValue);
                _db.SaveChanges();
                return RedirectToAction("Index", new { parentId  = lookUpValue.ParentId});
            }

            return View(lookUpValue);
        }

        // GET: LookUpValue/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpValue lookUpValue = _db.LookUpValues.Find(id);
            if (lookUpValue == null)
            {
                return HttpNotFound();
            }
            return View(lookUpValue);
        }

        // POST: LookUpValue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LookupValueId,ParentId,Name,Code,Description,LogoUrl,IsDefault,IsActive,IsSystemValue")] LookUpValue lookUpValue)
        {
            if (ModelState.IsValid)
            {
                //var userId = await _api.Post("/api/User/GetUserId", new { Email = User.Identity.Name });

                //lookUpValue.LastUpdatedBy = Convert.ToInt32(userId);
                _db.Entry(lookUpValue).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", new { parentId = lookUpValue.ParentId });
            }
            return View(lookUpValue);
        }

        // GET: LookUpValue/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LookUpValue lookUpValue = _db.LookUpValues.Find(id);
            if (lookUpValue == null)
            {
                return HttpNotFound();
            }
            return View(lookUpValue);
        }

        // POST: LookUpValue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_db.LookUpValues.Any(x => x.ParentId == id))
            {
                LookUpValue lookUpValue = _db.LookUpValues.Find(id);
                ViewBag.ErrorMessage = "This is a parent item for other items. Can not be deleted";
                return View(lookUpValue);
            }
            else
            {
                LookUpValue lookUpValue = _db.LookUpValues.Find(id);
                _db.LookUpValues.Remove(lookUpValue);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
