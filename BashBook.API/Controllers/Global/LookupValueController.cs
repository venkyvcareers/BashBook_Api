using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BashBook.BAL.Global;
using BashBook.Model.Global;
using BashBook.Model.Group;

namespace BashBook.API.Controllers.Global
{
    [RoutePrefix("api/LookupValue")]
    public class LookupValueController : BaseController
    {
        readonly LookupValueOperation _lookupValue = new LookupValueOperation();

        [HttpGet]
        [Route("GetAll")]
        public List<LookupValueModel> GetAll()
        {
            return _lookupValue.GetAll();
        }

        [HttpGet]
        [Route("GetAllJson")]
        public List<LookupValueJsonModel> GetAllJson()
        {
            return _lookupValue.GetAllJson();
        }

    }
}
