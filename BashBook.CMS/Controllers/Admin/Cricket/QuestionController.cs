using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BashBook.DAL.EDM;
using BashBook.Model.Lookup;

namespace BashBook.CMS.Controllers.Admin.Cricket
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class QuestionController : Controller
    {
        private BashBookEntities db = new BashBookEntities();

        // GET: Question
        public ActionResult Index()
        {
            var questions = db.Questions.Include(q => q.LookUpValue).Include(q => q.LookUpValue1).Include(q => q.LookUpValue2);
            return View(questions.ToList());
        }

        // GET: Question/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Question/Create
        public ActionResult Create()
        {
            GetViewBagData();
            return View();
        }

        // POST: Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            GetViewBagData();
            return View(question);
        }

        // GET: Question/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.SelectionTypeId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.PollSelectionTypes), "LookupValueId", "Name", question.SelectionTypeId);
            ViewBag.OptionsTypeId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.QuestionOptionsType), "LookupValueId", "Name", question.OptionsTypeId);
            ViewBag.LookupParentId = new SelectList(db.LookUpValues.Where(x => x.ParentId == 0), "LookupValueId", "Name", question.LookupParentId);
            ViewBag.LookupDisplayTypeId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.LookupDisplayType), "LookupValueId", "Name", question.LookupDisplayTypeId);

            return View(question);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            GetViewBagData();
            return View(question);
        }

        // GET: Question/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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

        public void GetViewBagData()
        {
            ViewBag.SelectionTypeId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.PollSelectionTypes), "LookupValueId", "Name");
            ViewBag.OptionsTypeId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.QuestionOptionsType), "LookupValueId", "Name");
            ViewBag.LookupParentId = new SelectList(db.LookUpValues.Where(x => x.ParentId == 0), "LookupValueId", "Name");
            ViewBag.LookupDisplayTypeId = new SelectList(db.LookUpValues.Where(x => x.ParentId == (int)Lookups.Parents.LookupDisplayType), "LookupValueId", "Name");

        }
    }
}
