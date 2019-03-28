using System.Collections.Generic;
using BashBook.DAL.Cricket;
using BashBook.Model.Cricket;

namespace BashBook.BAL.Cricket
{
    public class QuestionOperation : BaseBusinessAccessLayer
    {
        readonly QuestionRepository _question = new QuestionRepository();
        public List<QuestionRuleListModel> GetQuestionRuleList()
        {
            return _question.GetQuestionRuleList();
        }
    }
}
