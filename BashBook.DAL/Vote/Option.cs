using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.Model.User;
using BashBook.Model.Poll;

namespace BashBook.DAL.Vote
{
    public class OptionRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public List<OptionModel> GetAll(int pollId)
        {
            var result = (from o in _db.Options
                where o.PollId == pollId
                select new OptionModel
                {
                    Text = o.Text,
                    Image = o.Image,
                    OptionId = o.OptionId
                }).ToList();

            return result;
        }

        public List<int> GetAllOptionId(int pollId)
        {
            var result = (from o in _db.Options
                where o.PollId == pollId
                select o.OptionId).ToList();

            return result;
        }

        public List<OptionCountModel> GetOptionResponseCount(int pollId)
        {
            var result = (from o in _db.Options
                where o.PollId == pollId
                select new OptionCountModel
                {
                    OptionId = o.OptionId,
                    Count = _db.UserVotes.Count(x=>x.PollId == pollId && x.OptionId == o.OptionId)
                }).ToList();

            return result;
        }

        public List<UserGeneralViewModel> GetUserList(int optionId)
        {
            var result = (from uv in _db.UserVotes
                from u in _db.Users 
                where uv.OptionId == optionId && u.UserId == uv.UserId
                select new UserGeneralViewModel
                {
                    UserId = u.UserId,
                    LastName = u.LastName,
                    FirstName = u.FirstName,
                    Image = u.Image
                }).ToList();

            return result;
        }

        public List<int> GetOptionList(int userId, int pollId)
        {
            var result = (from uv in _db.UserVotes
                where uv.PollId == pollId && userId == uv.UserId
                select uv.OptionId).ToList();

            return result;
        }

        public int Add(OptionModel model)
        {
            try
            {
                var option = new Option()
                {
                    PollId = model.PollId,
                    Text = model.Text,
                    Image = model.Image,
                };

                _db.Options.Add(option);
                _db.SaveChanges();

                return option.OptionId;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Option - Add- " + json, ex);
                throw;
            }
        }

        public bool Edit(OptionModel model)
        {
            try
            {
                var option = _db.Options.First(x => x.OptionId == model.OptionId);

                option.Text = model.Text;
                option.Image = model.Image;

                _db.Entry(option).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Option - Edit- " + json, ex);
                throw;
            }
        }

        public bool Delete(int optionId)
        {
            try
            {
                //Poll
                var option = _db.Options.First(x => x.OptionId == optionId);
                _db.Options.Remove(option);

                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Option - Delete" + optionId, ex);
                throw;
            }

        }
    }
}
