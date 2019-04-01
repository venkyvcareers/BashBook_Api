using System.Collections.Generic;
using System.Web.Http;
using BashBook.BAL.Cricket;
using BashBook.Model.Cricket;

namespace BashBook.API.Controllers.Cricket
{
    [Authorize]
    [RoutePrefix("api/Match")]
    public class MatchController : BaseController
    {
        readonly MatchOperation _match = new MatchOperation();
        readonly UserMatchPredictionOperation _prediction = new UserMatchPredictionOperation();

        [HttpGet]
        [Route("GetAll/{tournamentId}")]
        public List<MatchPreviewModel> GetAll(int tournamentId)
        {
            return _match.GetAll(tournamentId);
        }

        [HttpGet]
        [Route("GetById/{matchId}")]
        public MatchPreviewModel GetById(int matchId)
        {
            return _match.GetById(matchId);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetSliderMatchList/{tournamentId}")]
        public List<MatchPreviewModel> GetSliderMatchList(int tournamentId)
        {
            return _match.GetSliderMatchList(tournamentId);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetMatchWinnerList/{tournamentId}")]
        public List<MatchWinnerViewModel> GetMatchWinnerList(int tournamentId)
        {
            return _match.GetMatchWinnerList(tournamentId);
        }

        [HttpGet]
        [Route("GetUserCounts/{tournamentId}")]
        public List<MatchUserCountModel> GetUserCounts(int tournamentId)
        {
            return _match.GetUserCounts(tournamentId);
        }

        [HttpGet]
        [Route("GetAllQuestion/{matchId}")]
        public List<CategoryQuestionModel> GetAllQuestion(int matchId)
        {
            return _match.GetAllQuestion(matchId);
        }

        [HttpGet]
        [Route("GetUserAnswers/{matchId}")]
        public List<CategoryQuestionModel> GetUserAnswers(int matchId)
        {
            int userId = GetUserId();
            return _match.GetUserAnswers(matchId, userId);
        }

        [HttpGet]
        [Route("GetMatchAnswers/{matchId}")]
        public List<CategoryQuestionModel> GetMatchAnswers(int matchId)
        {
            return _match.GetMatchAnswers(matchId);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [Route("UpdateAnswers")]
        public bool UpdateAnswers(UserMatchPredictionModel model)
        {
            return _match.UpdateAnswers(model);
        }

        [HttpPost]
        [Route("AddPrediction")]
        public bool AddPrediction(UserMatchPredictionModel model)
        {
            return _match.AddPrediction(model);
        }

        [HttpPost]
        [Route("EditPrediction")]
        public bool EditPrediction(UserMatchPredictionModel model)
        {
            return _match.EditPrediction(model);
        }

        [HttpGet]
        [Route("GetPredictionData/{matchId}")]
        public MatchPredictionDataModel GetPredictionData(int matchId)
        {
            return _match.GetPredictionData(matchId);
        }

        [HttpGet]
        [Route("GetLeaderBoard/{matchId}")]
        public List<UserScoreViewModel> GetMatchLeaderBoard(int matchId)
        {
            return _prediction.GetLeaderBoardByMatch(matchId);
        }
    }
}