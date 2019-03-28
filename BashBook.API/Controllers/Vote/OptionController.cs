using System.Collections.Generic;
using System.Web.Http;
using BashBook.BAL.Vote;
using BashBook.Model.Poll;

namespace BashBook.API.Controllers.Vote
{
    [Authorize]
    [RoutePrefix("api/Option")]
    public class OptionController : BaseController
    {
        readonly OptionOperation _option = new OptionOperation();

        [HttpGet]
        [Route("GetAll/{pollId}")]
        public List<OptionModel> GetAll(int pollId)
        {
            return _option.GetAll(pollId);
        }

        [HttpPost]
        [Route("Add")]
        public int Add(OptionModel model)
        {
            return _option.Add(model);
        }

        [HttpPost]
        [Route("Edit")]
        public bool Edit(OptionModel model)
        {
            return _option.Edit(model);
        }
    }
}
