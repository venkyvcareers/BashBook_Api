using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BashBook.DAL.EDM;

namespace BashBook.CMS.Controllers.Admin.Cricket
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class QuestionRuleController : Controller
    {
        private BashBookEntities db = new BashBookEntities();

        // GET: QuestionRule
        public ActionResult Index(int questionId)
        {
            ViewBag.QuestionId = questionId;
            var questionRules = db.QuestionRules.Where(x=>x.QuestionId == questionId).Include(q => q.Question);
            return View(questionRules.ToList());
        }

        // GET: QuestionRule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionRule questionRule = db.QuestionRules.Find(id);
            if (questionRule == null)
            {
                return HttpNotFound();
            }
            return View(questionRule);
        }

        // GET: QuestionRule/Create
        public ActionResult Create(int questionId)
        {
            ViewBag.QuestionId = new SelectList(db.Questions.Where(x=>x.QuestionId == questionId), "QuestionId", "Text");
            return View();
        }

        // POST: QuestionRule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionRule questionRule)
        {
            if (ModelState.IsValid)
            {
                db.QuestionRules.Add(questionRule);
                db.SaveChanges();
                return RedirectToAction("Index", new { questionId = questionRule.QuestionId });
            }

            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Text", questionRule.QuestionId);
            return View(questionRule);
        }

        // GET: QuestionRule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionRule questionRule = db.QuestionRules.Find(id);
            if (questionRule == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = new SelectList(db.Questions.Where(x=>x.QuestionId == questionRule.QuestionId), "QuestionId", "Text", questionRule.QuestionId);
            return View(questionRule);
        }

        // POST: QuestionRule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionRule questionRule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionRule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { questionId = questionRule.QuestionId });
            }
            ViewBag.QuestionId = new SelectList(db.Questions.Where(x => x.QuestionId == questionRule.QuestionId), "QuestionId", "Text", questionRule.QuestionId);
            return View(questionRule);
        }

        // GET: QuestionRule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionRule questionRule = db.QuestionRules.Find(id);
            if (questionRule == null)
            {
                return HttpNotFound();
            }
            return View(questionRule);
        }

        // POST: QuestionRule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionRule questionRule = db.QuestionRules.Find(id);
            db.QuestionRules.Remove(questionRule);
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
