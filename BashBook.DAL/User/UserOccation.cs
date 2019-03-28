using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.Model.User;
using BashBook.Utility;

namespace BashBook.DAL.User
{
    public class UserOccationRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public List<UserOccationViewModel> GetAll(int userId)
        {
            var result = (from uo in _db.UserOccations
                where uo.UserId == userId
                select new UserOccationViewModel
                {
                    UserOccationId = uo.UserOccationId,
                    Date = uo.Date,
                    OccationId = uo.OccationId,
                    CreatedUserInfo = (from u in _db.Users
                        where u.UserId == uo.CreatedBy
                        select new UserGeneralViewModel
                        {
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            UserId = u.UserId,
                            Image = u.Image
                        }).FirstOrDefault()
                }).ToList();

            return result;
        }

        public int Add(UserOccationModel model)
        {
            try
            {
                var userOccation = new UserOccation()
                {
                    UserId = model.UserId,
                    OccationId = model.OccationId,
                    Date = model.Date,
                    CreatedBy = model.CreatedBy,
                    CreatedOn = UnixTimeBaseClass.UnixTimeNow
                };

                _db.UserOccations.Add(userOccation);
                _db.SaveChanges();

                return userOccation.UserOccationId;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("User Occation - Add - " + json, ex);
                throw;
            }
        }

        public bool Edit(UserOccationModel model)
        {
            try
            {
                var userOccation = _db.UserOccations.First(x => x.UserOccationId == model.UserOccationId);

                userOccation.Date = model.Date;
                userOccation.UserId = model.UserId;
                userOccation.OccationId = model.OccationId;
                userOccation.LastUpdatedBy = model.CreatedBy;
                userOccation.LastUpdatedOn = UnixTimeBaseClass.UnixTimeNow;

                _db.Entry(userOccation).State = EntityState.Modified;

                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("User Occation - Edit - " + json, ex);
                throw;
            }
        }

        public bool Delete(int userOccationId)
        {
            try
            {
                var userOccation = _db.UserOccations.First(x => x.UserOccationId == userOccationId);

                _db.UserOccations.Remove(userOccation);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("User Occation - Delete - " + userOccationId, ex);
                throw;
            }
        }
    }
}
