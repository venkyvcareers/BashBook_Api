using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashBook.Model.Cricket
{
    public class QuestionRuleListModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<RuleModel> Rules { get; set; }
    }

    public class RuleModel
    {
        public int Prioriry { get; set; }
        public string Text { get; set; }
    }
}
