using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashBook.Model.Cricket
{
    public class MatchUserAnswerModel
    {
        public int MatchQuestionId { get; set; }
        public int UserId { get; set; }
        public int Answer { get; set; }
    }

    public class MatchUserCountModel
    {
        public int MatchId { get; set; }
        public int Number { get; set; }
        public int StatusId { get; set; }
        public int UserCount { get; set; }
    }
}
