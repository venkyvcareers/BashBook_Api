using System.Collections.Generic;
using System.Linq;
using BashBook.DAL.EDM;
using BashBook.Model.Global;

namespace BashBook.DAL.Global
{
    public class LookupValueRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public List<LookupValueJsonModel> GetAllJson()
        {
            var result = (from lv in _db.LookUpValues
                          where lv.IsActive
                          select new LookupValueJsonModel
                          {
                              Id = lv.LookupValueId,
                              LogoUrl = lv.LogoUrl,
                              Name = lv.Name,
                              ParentId = lv.ParentId,
                              Code = lv.Code
                          }).ToList();

            return result;
        }

        public List<LookupValueModel> GetAll()
        {
            var result = (from lv in _db.LookUpValues
                          where lv.IsActive
                          select new LookupValueModel
                          {
                              Id = lv.LookupValueId,
                              LogoUrl = lv.LogoUrl,
                              Name = lv.Name,
                              ParentId = lv.ParentId,
                              IsActive = lv.IsActive,
                              IsDefault = lv.IsDefault ?? false
                          }).ToList();

            return result;
        }

    }
}
