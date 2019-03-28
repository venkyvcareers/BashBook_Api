using System.Collections.Generic;
using System.Linq;
using BashBook.DAL.EDM;
using BashBook.Model.Log;

namespace BashBook.DAL.Log
{
    public class LogInfoRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();
        public List<LogInfoModel> GetAll()
        {
            return (from l in _db.LogInfoes
                select new LogInfoModel
                {
                    Exception = l.Exception,
                    Level = l.Level,
                    Id = l.Id,
                    Message = l.Message,
                    Date = l.Date,
                    Logger = l.Logger,
                    Thread = l.Thread,
                    Method = l.Method,
                    StackTrace = l.StackTrace,
                    Type = l.Type
                }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
