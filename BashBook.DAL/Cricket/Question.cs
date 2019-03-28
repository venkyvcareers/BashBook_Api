using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashBook.DAL.EDM;
using BashBook.Model.Cricket;

namespace BashBook.DAL.Cricket
{
    public class QuestionRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public List<QuestionRuleListModel> GetQuestionRuleList()
        {
            var questions = _db.QuestionRules.Select(x => x.QuestionId).Distinct().ToList();
            var result = (from q in _db.Questions
                          where questions.Contains(q.QuestionId)
                          select new QuestionRuleListModel()
                          {
                              QuestionId = q.QuestionId,
                              QuestionText = q.Text,
                              Rules = (from qr in _db.QuestionRules
                                       where qr.QuestionId == q.QuestionId
                                       select new RuleModel()
                                       {
                                           Text = qr.Text,
                                           Prioriry = qr.Priority
                                       }).OrderBy(x => x.Prioriry).ToList()
                          }).ToList();

            return result;
        }
    }
}
