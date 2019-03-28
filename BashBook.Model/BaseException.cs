using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;

namespace BashBook.Model
{
    public class CustomExceptionModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class ReturnExceptionModel : Exception
    {
        public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ReturnExceptionModel(CustomExceptionModel model)
        {
            Log.Error(model.Message, new Exception(model.Message));
            throw new HttpResponseException(new HttpResponseMessage(model.StatusCode) { Content = new ObjectContent(typeof(CustomExceptionModel), model, GlobalConfiguration.Configuration.Formatters.JsonFormatter) });
        }
    }
}
