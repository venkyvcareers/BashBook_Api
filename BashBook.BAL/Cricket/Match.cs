using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web.Script.Serialization;
using BashBook.DAL.Cricket;
using BashBook.Model;
using BashBook.Model.Cricket;
using BashBook.Model.Lookup;
using BashBook.Utility;

namespace BashBook.BAL.Cricket
{
    public class MatchOperation : BaseBusinessAccessLayer
    {
        readonly MatchRepository _match = new MatchRepository();
        readonly MatchUserAnswerRepository _matchUserAnswer = new MatchUserAnswerRepository();
        readonly MatchUserScoreRepository _matchUserScore = new MatchUserScoreRepository();
        readonly UserMatchPredictionRepository _userMatchPrediction = new UserMatchPredictionRepository();
        public List<MatchPreviewModel> GetAll(int tournamentId)
        {
            return _match.GetMatchList(tournamentId);
        }

        public MatchPreviewModel GetById(int matchId)
        {
            return _match.GetById(matchId);
        }

        public List<MatchPreviewModel> GetSliderMatchList(int tournamentId)
        {
            return _match.GetSliderMatchList(tournamentId);
        }

        public List<MatchWinnerViewModel> GetMatchWinnerList(int tournamentId)
        {
            var result = new List<MatchWinnerViewModel>();
            var list = _matchUserScore.GetMatchWinnerList(tournamentId);

            foreach (var match in list)
            {
                foreach (var user in match.Users)
                {
                    result.Add(new MatchWinnerViewModel()
                    {
                        MatchId = match.MatchId,
                        MatchNumber = match.MatchNumber,
                        Score = user.Score,
                        User = user.User
                    });
                }
            }

            return result;
        }

        public List<MatchUserCountModel> GetUserCounts(int tournamentId)
        {
            return _matchUserAnswer.GetUserCounts(tournamentId);
        }

        public List<CategoryQuestionModel> GetAllQuestion(int matchId)
        {
            return _match.GetAllQuestion(matchId);
        }

        public List<CategoryQuestionModel> GetUserAnswers(int matchId, int userId)
        {
            return _match.GetUserAnswers(matchId, userId);
        }

        public List<CategoryQuestionModel> GetMatchAnswers(int matchId)
        {
            return _match.GetMatchAnswers(matchId);
        }

        public bool UpdateAnswers(UserMatchPredictionModel model)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    foreach (var answer in model.Answers)
                    {
                        _match.AddMatchQuestionAnswer(answer.MatchQuestionId, answer.Answer);
                    }

                    var matchAnswers = _match.GetMatchQuestionAnswers(model.MatchId);
                    var userAnswers = _matchUserAnswer.GetUserMatchAnswers(model.MatchId);

                    var users = userAnswers.Select(x => x.UserId).Distinct().ToList();
                    var scoreService = new ScoreCalculationService();
                    foreach (var user in users)
                    {
                        var matchUserAnswers = (from ua in userAnswers
                                                where ua.UserId == user
                                                select new QuestionAnswerModel()
                                                {
                                                    MatchQuestionId = ua.MatchQuestionId,
                                                    Answer = ua.Answer
                                                }).ToList();

                        int userScore = scoreService.GetUserMatchScore(matchAnswers, matchUserAnswers);

                        _matchUserScore.Add(new MatchUserScoreModel()
                        {
                            MatchId = model.MatchId,
                            UserId = user,
                            Score = userScore
                        });

                    }

                    _match.ChangeStatus(model.MatchId, (int)Lookups.MatchStatus.Completed);

                    scope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string json = js.Serialize(model);
                    Log.Error("BL-Match - AddTournamentPrediction" + json, ex);
                    throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
                }
            }
        }

        public bool AddPrediction(UserMatchPredictionModel model)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    if (_match.GetStartTime(model.MatchId) < (UnixTimeBaseClass.UnixTimeNow + 1800))
                    {
                        throw new InvalidOperationException("You can only add prediction before half an hour of match");
                    }
                    if (_userMatchPrediction.IsPredictionAdded(model.UserId, model.MatchId))
                    {
                        throw new InvalidOperationException("You have already added your predictions");
                    }

                    foreach (var answer in model.Answers)
                    {
                        _matchUserAnswer.Add(new MatchQuestionUserAnswerModel()
                        {
                            UserId = model.UserId,
                            MatchId = model.MatchId,
                            MatchQuestionId = answer.MatchQuestionId,
                            Answer = answer.Answer
                        });
                    }

                    scope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string json = js.Serialize(model);
                    Log.Error("BL-Match - AddTournamentPrediction" + json, ex);
                    throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
                }
            }
        }

        public bool EditPrediction(UserMatchPredictionModel model)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    if (_match.GetStartTime(model.MatchId) < (UnixTimeBaseClass.UnixTimeNow + 1800))
                    {
                        throw new InvalidOperationException("You can only edit prediction before half an hour of match");
                    }

                    foreach (var answer in model.Answers)
                    {
                        _matchUserAnswer.Edit(new MatchQuestionUserAnswerModel()
                        {
                            UserId = model.UserId,
                            MatchId = model.MatchId,
                            MatchQuestionId = answer.MatchQuestionId,
                            Answer = answer.Answer
                        });
                    }

                    scope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string json = js.Serialize(model);
                    Log.Error("BL-Match - EditMatchPrediction" + json, ex);
                    throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
                }
            }
        }

        public MatchPredictionDataModel GetPredictionData(int matchId)
        {
            return _match.GetPredictionData(matchId);
        }
    }
}
