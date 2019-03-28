using System.Collections.Generic;
using System.Web.Http;
using BashBook.BAL.Log;
using BashBook.Model.Log;

namespace BashBook.API.Controllers.LogInfo
{
    [Authorize]
    [RoutePrefix("api/LogInfo")]
    public class LogInfoController : BaseController
    {
        readonly LogInfoOperation _logInfo = new LogInfoOperation();

        [HttpGet]
        [Route("GetAll")]
        public List<LogInfoModel> GetAll()
        {
            return _logInfo.GetAll();
        }
    }
}
