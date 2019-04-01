using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web.Mvc;
using BashBook.CMS.Service;
using BashBook.DAL.EDM;
using BashBook.Model.Cricket;

namespace BashBook.CMS.Controllers.Admin.Cricket
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class MatchQuestionController : Controller
    {
        private BashBookEntities db = new BashBookEntities();

        // GET: MatchQuestion
        public ActionResult Index(int matchId)
        {
            var matchQuestions = db.MatchQuestions.Where(x => x.MatchId == matchId).Include(m => m.Match).Include(m => m.PredictionCategory).Include(m => m.Question);
            return View(matchQuestions.ToList());
        }

        public ActionResult Clone()
        {
            ViewBag.fromMatchId = new SelectList(db.Matches, "MatchId", "Number");
            ViewBag.toMatchId = new SelectList(db.Matches, "MatchId", "Number");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Clone(int fromMatchId, int toMatchId)
        {
            if (db.MatchQuestions.Any(x => x.MatchId == toMatchId))
            {
                ViewBag.ErrorMessage = "Already some questions added";
                ViewBag.fromMatchId = new SelectList(db.Matches, "MatchId", "Number");
                ViewBag.toMatchId = new SelectList(db.Matches, "MatchId", "Number");
                return View();
            }
            var questions = db.MatchQuestions.Where(x => x.MatchId == fromMatchId).ToList();

            foreach (var fromQuestion in questions)
            {
                var toQuestion = new MatchQuestion()
                {
                    MatchId = toMatchId,
                    PredictionCategoryId = fromQuestion.PredictionCategoryId,
                    QuestionId = fromQuestion.QuestionId
                };
                db.MatchQuestions.Add(toQuestion);
            }

            db.SaveChanges();

            return RedirectToAction("Index", new { matchId = toMatchId });
        }

        public ActionResult GenerateScore()
        {
            ViewBag.matchId = new SelectList(db.Matches, "MatchId", "Number");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateScore(int matchId)
        {
            if (!db.MatchQuestions.Any(x => x.MatchId == matchId))
            {
                ViewBag.ErrorMessage = "No Questions Available";
                ViewBag.matchId = new SelectList(db.Matches, "MatchId", "Number");
                return View();
            }

            if (db.MatchQuestions.Any(x => x.MatchId == matchId && x.Answer == null))
            {
                ViewBag.ErrorMessage = "Question answers are not filled";
                ViewBag.matchId = new SelectList(db.Matches, "MatchId", "Number");
                return View();
            }

            var categories = db.MatchQuestions.Where(x => x.MatchId == matchId).Select(y => y.PredictionCategoryId)
                .Distinct().ToList();

            var matchAnswers = (from pc in db.PredictionCategories
                                where categories.Contains(pc.PredictionCategoryId)
                                select new MatchQuestionAnswerModel()
                                {
                                    BonusTimes = pc.BonusTimes,
                                    CategoryTypeId = pc.CategoryTypeId,
                                    LossPoints = pc.LossPoints,
                                    WinPoints = pc.WinPoints,
                                    Answers = (from mq in db.MatchQuestions
                                               where mq.MatchId == matchId && mq.PredictionCategoryId == pc.PredictionCategoryId
                                               select new QuestionAnswerModel()
                                               {
                                                   Answer = mq.Answer ?? 0,
                                                   MatchQuestionId = mq.MatchQuestionId
                                               }).ToList()
                                }).ToList();



            var userAnswer = (from mua in db.MatchUserAnswers
                              where mua.MatchQuestion.MatchId == matchId
                              select mua).ToList();

            var users = userAnswer.Select(x => x.UserId).Distinct().ToList();

            var scoreService = new PredictionScoreCalculation();

            foreach (var user in users)
            {
                var matchUserAnswers = (from ua in userAnswer
                    where ua.UserId == user
                    select new QuestionAnswerModel()
                    {
                        MatchQuestionId = ua.MatchQuestionId,
                        Answer = ua.Answer
                    }).ToList();

                int userScore = scoreService.GetUserMatchScore(matchAnswers, matchUserAnswers);

                var matchUserScore = new MatchUserScore()
                {
                    MatchId = matchId,
                    UserId = user,
                    Score = userScore
                };

                db.MatchUserScores.Add(matchUserScore);
            }

            db.SaveChanges();

            return RedirectToAction("Index", new
            {
                matchId = matchId
            });
        }

        // GET: MatchQuestion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchQuestion matchQuestion = db.MatchQuestions.Find(id);
            if (matchQuestion == null)
            {
                return HttpNotFound();
            }
            return View(matchQuestion);
        }

        // GET: MatchQuestion/Create
        public ActionResult Create()
        {
            ViewBag.MatchId = new SelectList(db.Matches, "MatchId", "Number");
            ViewBag.PredictionCategoryId = new SelectList(db.PredictionCategories, "PredictionCategoryId", "Name");
            ViewBag.QuestionId = new SelectList(db.Questions.Where(x => x.IsForMatch == true), "QuestionId", "Text");
            return View();
        }

        // POST: MatchQuestion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MatchQuestion matchQuestion)
        {
            if (ModelState.IsValid)
            {
                db.MatchQuestions.Add(matchQuestion);
                db.SaveChanges();
                return RedirectToAction("Index", new { matchId = matchQuestion.MatchId });
            }

            ViewBag.MatchId = new SelectList(db.Matches, "MatchId", "Number", matchQuestion.MatchId);
            ViewBag.PredictionCategoryId = new SelectList(db.PredictionCategories, "PredictionCategoryId", "Name", matchQuestion.PredictionCategoryId);
            ViewBag.QuestionId = new SelectList(db.Questions.Where(x => x.IsForMatch == true), "QuestionId", "Text", matchQuestion.QuestionId);
            return View(matchQuestion);
        }

        // GET: MatchQuestion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchQuestion matchQuestion = db.MatchQuestions.Find(id);
            if (matchQuestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.MatchId = new SelectList(db.Matches, "MatchId", "Number", matchQuestion.MatchId);
            ViewBag.PredictionCategoryId = new SelectList(db.PredictionCategories, "PredictionCategoryId", "Name", matchQuestion.PredictionCategoryId);
            ViewBag.QuestionId = new SelectList(db.Questions.Where(x => x.IsForMatch == true), "QuestionId", "Text", matchQuestion.QuestionId);
            return View(matchQuestion);
        }

        // POST: MatchQuestion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MatchQuestion matchQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matchQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { matchId = matchQuestion.MatchId });
            }
            ViewBag.MatchId = new SelectList(db.Matches, "MatchId", "Number", matchQuestion.MatchId);
            ViewBag.PredictionCategoryId = new SelectList(db.PredictionCategories, "PredictionCategoryId", "Name", matchQuestion.PredictionCategoryId);
            ViewBag.QuestionId = new SelectList(db.Questions.Where(x => x.IsForMatch == true), "QuestionId", "Text", matchQuestion.QuestionId);
            return View(matchQuestion);
        }

        // GET: MatchQuestion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchQuestion matchQuestion = db.MatchQuestions.Find(id);
            if (matchQuestion == null)
            {
                return HttpNotFound();
            }
            return View(matchQuestion);
        }

        // POST: MatchQuestion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MatchQuestion matchQuestion = db.MatchQuestions.Find(id);
            db.MatchQuestions.Remove(matchQuestion);
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
