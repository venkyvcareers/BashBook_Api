using System;
using System.Collections.Generic;
using System.Net;
using System.Transactions;
using System.Web.Script.Serialization;
using BashBook.DAL.Cricket;
using BashBook.Model;
using BashBook.Model.Cricket;

namespace BashBook.BAL.Cricket
{
    public class TournamentOperation : BaseBusinessAccessLayer
    {
        readonly TournamentRepository _tournament = new TournamentRepository();


        public List<TournamentModel> GetAll()
        {
            return _tournament.GetAll();
        }

        public bool AddTournamentPrediction(UserMatchPredictionModel model)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    foreach (var answer in model.Answers)
                    {
                        _tournament.AddUserQuestionAnswer(new MatchQuestionUserAnswerModel()
                        {
                            UserId = model.UserId,
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
                    Log.Error("BL-Tournament - AddTournamentPrediction" + json, ex);
                    throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
                }
            }
        }

        public List<CategoryQuestionModel> GetAllQuestion(int tournamentId)
        {
            return _tournament.GetAllQuestion(tournamentId);
        }

        public TournamentPredictionDataModel GetPredictionData(int tournamentId)
        {
            return _tournament.GetPredictionData(tournamentId);
        }
    }
}
