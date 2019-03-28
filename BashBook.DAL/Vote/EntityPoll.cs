using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.Model.Lookup;
using BashBook.Model.Poll;

namespace BashBook.DAL.Vote
{
    public class EntityPollRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public List<int> GetEntityIdList(int pollId, int entityTypeId)
        {
            var result = (from ep in _db.EntityPolls
                          where ep.EntityTypeId == entityTypeId
                                && ep.PollId == pollId
                          select ep.EntityId).ToList();

            return result;
        }

        public bool Add(EntityPollModel model)
        {
            try
            {
                var entityPoll = new EntityPoll()
                {
                    PollId = model.PollId,
                    EntityId = model.EntityId,
                    EntityTypeId = model.EntityTypeId
                };

                _db.EntityPolls.Add(entityPoll);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("EntityPoll - Add- " + json, ex);
                throw;
            }
        }

        public bool Delete(int pollId, int entityTypeId, int entityId)
        {
            var entityPoll = _db.EntityPolls.First(x =>
                x.PollId == pollId && x.EntityTypeId == entityTypeId && x.EntityId == entityId);

            _db.EntityPolls.Remove(entityPoll);
            _db.SaveChanges();

            return true;
        }
    }
}
