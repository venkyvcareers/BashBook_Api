using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BashBook.DAL.EDM;
using BashBook.Model.Lookup;

namespace BashBook.CMS.Controllers.Admin.Cricket
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class TournamentQuestionController : Controller
    {
        private BashBookEntities db = new BashBookEntities();

        // GET: TournamentQuestion
        public ActionResult Index(int tournamentId)
        {
            var tournamentQuestions = db.TournamentQuestions.Where(x=>x.TournamentId == tournamentId).Include(t => t.PredictionCategory).Include(t => t.Question).Include(t => t.Tournament);
            return View(tournamentQuestions.ToList());
        }

        // GET: TournamentQuestion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TournamentQuestion tournamentQuestion = db.TournamentQuestions.Find(id);
            if (tournamentQuestion == null)
            {
                return HttpNotFound();
            }
            return View(tournamentQuestion);
        }

        // GET: TournamentQuestion/Create
        public ActionResult Create()
        {
            ViewBag.PredictionCategoryId = new SelectList(db.PredictionCategories, "PredictionCategoryId", "Name");
            ViewBag.QuestionId = new SelectList(db.Questions.Where(x => x.IsForMatch != true), "QuestionId", "Text");
            ViewBag.TournamentId = new SelectList(db.Tournaments, "TournamentId", "Name");
            return View();
        }

        // POST: TournamentQuestion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TournamentQuestion tournamentQuestion)
        {
            if (ModelState.IsValid)
            {
                db.TournamentQuestions.Add(tournamentQuestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PredictionCategoryId = new SelectList(db.PredictionCategories, "PredictionCategoryId", "Name", tournamentQuestion.PredictionCategoryId);
            ViewBag.QuestionId = new SelectList(db.Questions.Where(x => x.IsForMatch != true), "QuestionId", "Text", tournamentQuestion.QuestionId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "TournamentId", "Name", tournamentQuestion.TournamentId);
            return View(tournamentQuestion);
        }

        // GET: TournamentQuestion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TournamentQuestion tournamentQuestion = db.TournamentQuestions.Find(id);
            if (tournamentQuestion == null)
            {
                return HttpNotFound();
            }

            ViewBag.PredictionCategoryId = new SelectList(db.PredictionCategories, "PredictionCategoryId", "Name", tournamentQuestion.PredictionCategoryId);
            ViewBag.QuestionId = new SelectList(db.Questions.Where(x => x.IsForMatch != true), "QuestionId", "Text", tournamentQuestion.QuestionId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "TournamentId", "Name", tournamentQuestion.TournamentId);
            return View(tournamentQuestion);
        }

        // POST: TournamentQuestion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TournamentQuestion tournamentQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tournamentQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PredictionCategoryId = new SelectList(db.PredictionCategories, "PredictionCategoryId", "Name", tournamentQuestion.PredictionCategoryId);
            ViewBag.QuestionId = new SelectList(db.Questions.Where(x => x.IsForMatch != true), "QuestionId", "Text", tournamentQuestion.QuestionId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "TournamentId", "Name", tournamentQuestion.TournamentId);
            return View(tournamentQuestion);
        }


        // GET: TournamentQuestion/Answer/5
        public ActionResult Answer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TournamentQuestion tournamentQuestion = db.TournamentQuestions.Find(id);
            if (tournamentQuestion == null)
            {
                return HttpNotFound();
            }

            var question = tournamentQuestion.Question;
            var teams = db.TournamentTeams
                .Where(y => y.TournamentId == tournamentQuestion.TournamentId)
                .Select(yy => yy.TeamId).ToList();
            switch (question.OptionsTypeId)
            {
                case (int)Lookups.QuestionOptionsType.Team:
                    ViewBag.answerId = new SelectList(db.Teams
                        .Where(x => teams.Contains(x.TeamId)), "TeamId", "Name");
                    break;
                case (int)Lookups.QuestionOptionsType.Player:
                    var players = db.TeamPlayers.Where(x => teams.Contains(x.TeamId)).Select(x => x.PlayerId).ToList();

                    ViewBag.answerId = new SelectList(db.Players.Where(x => players.Contains(x.PlayerId)), "PlayerId", "Name");
                    break;
                case (int)Lookups.QuestionOptionsType.Lookup:
                    ViewBag.answerId = new SelectList(db.LookUpValues.Where(x => x.ParentId == question.LookupParentId), "LookupValueId", "Name");
                    break;
                default:
                    break;
            }


            ViewBag.PredictionCategoryId = new SelectList(db.PredictionCategories, "PredictionCategoryId", "Name", tournamentQuestion.PredictionCategoryId);
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Text", tournamentQuestion.QuestionId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "TournamentId", "Name", tournamentQuestion.TournamentId);
            return View(tournamentQuestion);
        }

        // POST: TournamentQuestion/Answer/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Answer(TournamentQuestion tournamentQuestion, int answerId)
        {
            if (ModelState.IsValid)
            {
                tournamentQuestion.Answer = answerId;
                db.Entry(tournamentQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PredictionCategoryId = new SelectList(db.PredictionCategories, "PredictionCategoryId", "Name", tournamentQuestion.PredictionCategoryId);
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Text", tournamentQuestion.QuestionId);
            ViewBag.TournamentId = new SelectList(db.Tournaments, "TournamentId", "Name", tournamentQuestion.TournamentId);
            return View(tournamentQuestion);
        }

        // GET: TournamentQuestion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TournamentQuestion tournamentQuestion = db.TournamentQuestions.Find(id);
            if (tournamentQuestion == null)
            {
                return HttpNotFound();
            }
            return View(tournamentQuestion);
        }

        // POST: TournamentQuestion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TournamentQuestion tournamentQuestion = db.TournamentQuestions.Find(id);
            db.TournamentQuestions.Remove(tournamentQuestion);
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
