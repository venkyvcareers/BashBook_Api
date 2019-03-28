using System.Collections.Generic;
using System.Web.Http;
using BashBook.BAL.Cricket;
using BashBook.Model.Cricket;

namespace BashBook.API.Controllers.Cricket
{
    [Authorize]
    [RoutePrefix("api/Question")]
    public class QuestionController : BaseController
    {
        readonly QuestionOperation _question = new QuestionOperation();

        [HttpGet]
        [Route("RuleList")]
        public List<QuestionRuleListModel> GetQuestionRuleList()
        {
            return _question.GetQuestionRuleList();
        }
    }
}
