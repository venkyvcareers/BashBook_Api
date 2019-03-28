using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BashBook.DAL.EDM;
using BashBook.Model.Cricket;

namespace BashBook.CMS.Service
{
    public class PredictionScoreCalculation
    {
        public int GetUserMatchScore(List<MatchQuestionAnswerModel> categoryMatchAnswers, List<QuestionAnswerModel> userAnswers)
        {
            var score = 0;

            foreach (var category in categoryMatchAnswers)
            {
                bool isAllCorrect = true;
                foreach (var macthAnswer in category.Answers)
                {
                    if (userAnswers.Any(x => x.MatchQuestionId == macthAnswer.MatchQuestionId))
                    {
                        int userAnswer = userAnswers.First(x => x.MatchQuestionId == macthAnswer.MatchQuestionId)
                            .Answer;

                        if (userAnswer == macthAnswer.Answer)
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