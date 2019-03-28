using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Script.Serialization;
using BashBook.DAL.Cricket;
using BashBook.Model;
using BashBook.Model.Cricket;
using BashBook.Model.Lookup;
using BashBook.Utility;

namespace BashBook.BAL.Cricket
{
    public class UserMatchPredictionOperation : BaseBusinessAccessLayer
    {
        readonly UserMatchPredictionRepository _userMatchPrediction = new UserMatchPredictionRepository();
        readonly MatchRepository _match = new MatchRepository();

        public List<UserMatchStatusModel> GetPredictedMatchList(int tournamentId, int userId)
        {
            return _userMatchPrediction.GetPredictedMatchList(tournamentId, userId);
        }

        public List<UserScoreViewModel> GetLeaderBoardByMatch(int matchId)
        {
            return _userMatchPrediction.GetLeaderBoardByMatch(matchId);
        }

        public List<UserScoreViewModel> GetLeaderBoardByTournament(int tournamentId)
        {
            return _userMatchPrediction.GetLeaderBoardByTournament(tournamentId);
        }

        public int Add(PredictionModel model)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    if (_match.GetStartTime(model.MatchId) < (UnixTimeBaseClass.UnixTimeNow + 1800))
                    {
                        throw new InvalidOperationException("You can only add prediction before 30 Minutes of Match");
                    }
                    if (_userMatchPrediction.IsPredictionAdded(model.UserId, model.MatchId))
                    {
                        throw new InvalidOperationException("You have already added your predictions");
                    }
                    //int predictionId = _prediction.Add(model);

                    //int id = _userMatchPrediction.Add(new UserMatchAnswerModel
                    //{
                    //    UserId = model.UserId,
                    //    MatchId = model.MatchId,
                    //    PredictionId = predictionId
                    //});

                    scope.Complete();

                    return 1;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string json = js.Serialize(model);
                    Log.Error("BL-UserMatchPrediction - Add" + json, ex);
                    throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
                }
            }
        }

        public bool Edit(PredictionModel model)
        {
            var match = _match.GetById(model.MatchId);

            if (match.StatusId != (int) Lookups.MatchStatus.YetToStart)
            {
                throw new InvalidOperationException("Prediction can only edited before 1 Hour of match time");
            }
            if (match.StartsOn < (UnixTimeBaseClass.UnixTimeNow + 1800))
            {
                throw new InvalidOperationException("Prediction can only edited before 1 Hour of match time");
            }
            return true;
        }
    }
}
