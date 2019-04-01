using System.Collections.Generic;
using System.Web.Http;
using BashBook.BAL.Cricket;
using BashBook.Model.Cricket;

namespace BashBook.API.Controllers.Cricket
{
    [Authorize]
    [RoutePrefix("api/UserPrediction")]
    public class UserPredictionController : BaseController
    {
        readonly UserMatchPredictionOperation _prediction = new UserMatchPredictionOperation();


        [HttpGet]
        [Route("GetPredictedMatchList/{tournamentId}")]
        public List<UserMatchStatusModel> GetPredictedMatchList(int tournamentId)
        {
            int userId = GetUserId();
            return _prediction.GetPredictedMatchList(tournamentId, userId);
        }

        [HttpPost]
        [Route("Add")]
        public int Add(PredictionModel model)
        {
            return _prediction.Add(model);
        }

        [HttpPost]
        [Route("Edit")]
        public bool Edit(PredictionModel model)
        {
            return _prediction.Edit(model);
        }
    }
}