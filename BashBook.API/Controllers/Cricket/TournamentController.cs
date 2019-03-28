using System.Collections.Generic;
using System.Web.Http;
using BashBook.BAL.Cricket;
using BashBook.Model.Cricket;

namespace BashBook.API.Controllers.Cricket
{
    //[Authorize]
    [RoutePrefix("api/Tournament")]
    public class TournamentController : BaseController
    {
        readonly TournamentOperation _tournament = new TournamentOperation();
        readonly UserMatchPredictionOperation _prediction = new UserMatchPredictionOperation();

        [HttpGet]
        [Route("GetAll")]
        public List<TournamentModel> GetAll()
        {
            return _tournament.GetAll();
        }

        [HttpPost]
        [Route("AddTournamentPrediction")]
        public bool AddTournamentPrediction(UserMatchPredictionModel model)
        {
            return _tournament.AddTournamentPrediction(model);
        }

        [HttpGet]
        [Route("GetAllQuestion/{tournamentId}")]
        public List<CategoryQuestionModel> GetAllQuestion(int tournamentId)
        {
            return _tournament.GetAllQuestion(tournamentId);
        }

        [HttpGet]
        [Route("GetPredictionData/{tournamentId}")]
        public TournamentPredictionDataModel GetPredictionData(int tournamentId)
        {
            return _tournament.GetPredictionData(tournamentId);
        }

        [HttpGet]
        [Route("GetLeaderBoard/{tournamentId}")]
        public List<UserScoreViewModel> GetTournamentLeaderBoard(int tournamentId)
        {
            return _prediction.GetLeaderBoardByTournament(tournamentId);
        }
    }
}