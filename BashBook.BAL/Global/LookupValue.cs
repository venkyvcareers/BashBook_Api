using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashBook.DAL.Global;
using BashBook.Model.Global;
using BashBook.Model.Post;

namespace BashBook.BAL.Global
{
    public class LookupValueOperation : BaseBusinessAccessLayer
    {
        readonly LookupValueRepository _lookupValue = new LookupValueRepository();

        public List<LookupValueModel> GetAll()
        {
            return _lookupValue.GetAll();
        }

        public List<LookupValueJsonModel> GetAllJson()
        {
            return _lookupValue.GetAllJson();
        }

    }
}
