using System.Collections.Generic;
using BashBook.DAL.Log;
using BashBook.Model.Log;

namespace BashBook.BAL.Log
{
    public class LogInfoOperation : BaseBusinessAccessLayer
    {
        readonly LogInfoRepository _logInfo = new LogInfoRepository();

        public List<LogInfoModel> GetAll()
        {
            return _logInfo.GetAll();
        }
    }
}
