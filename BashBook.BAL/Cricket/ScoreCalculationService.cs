using BashBook.Model.Cricket;
using System.Collections.Generic;
using System.Linq;

namespace BashBook.BAL.Cricket
{
    public class ScoreCalculationService
    {
        public int GetUserMatchScore(List<MatchQuestionAnswerModel> categoryMatchAnswers, List<QuestionAnswerModel> userAnswers)
        {
            var score = 0;

            foreach (var category in categoryMatchAnswers)
            {
                bool isAllCorrect = true;
                foreach (var matchAnswer in category.Answers)
                {
                    if (userAnswers.Any(x => x.MatchQuestionId == matchAnswer.MatchQuestionId))
                    {
                        int userAnswer = userAnswers.First(x => x.MatchQuestionId == matchAnswer.MatchQuestionId)
                            .Answer;

                        if (userAnswer == matchAnswer.Answer)
                        {
                            score = score + category.WinPoints;
                        }
                        else
                        {
                            isAllCorrect = false;
                            score = score - category.LossPoints;
                        }
                    }
                    else
                    {
                        isAllCorrect = false;
                    }
                }

                if (isAllCorrect && category.BonusTimes > 1)
                {
                    score = score + (category.WinPoints * category.Answers.Count * (category.BonusTimes - 1));
                }
            }

            return score;
        }
    }
}