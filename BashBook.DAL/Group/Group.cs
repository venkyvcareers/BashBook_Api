using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.DAL.Global;
using BashBook.Model.Global;
using BashBook.Model.Group;
using BashBook.Model.Lookup;
using BashBook.Utility;

namespace BashBook.DAL.Group
{
    public class GroupRepository : BaseDataAccessLayer
    {
        private readonly BashBookEntities _db = new BashBookEntities();

        public List<GroupPreviewModel> GetPreviewList(int userId)
        {
            var result = (from gu in _db.GroupUsers
                          from g in _db.Groups
                          where gu.UserId == userId && gu.GroupId == g.GroupId
                          select new GroupPreviewModel
                          {
                              Name = g.Title,
                              GroupId = g.GroupId,
                              Image = g.Image,
                              Message = g.Message,
                              UnReadPostCount = _db.Posts.Count(x=>x.EntityTypeId == (int)Lookups.EntityTypes.Group && x.EntityId == g.GroupId && x.PostedBy != userId && x.PostedOn > _db.GroupUsers.FirstOrDefault(xx => xx.GroupId == g.GroupId && xx.UserId == userId).LastSeenOn)
                          }).ToList();

            return result;
        }

        public ManageGroupModel GetForEdit(int groupId)
        {
            var result = (from g in _db.Groups
                          where g.GroupId == groupId
                          select new ManageGroupModel()
                          {
                              GroupId = g.GroupId,
                              UserId = g.CreatedBy,
                              Image = g.Image,
                              Message = g.Message,
                              Title = g.Title
                          }).FirstOrDefault();
            return result;
        }
        public List<int> GetGroupIdList(int userId)
        {
            var result = (from gu in _db.GroupUsers
                          where gu.UserId == userId
                          select gu.GroupId).Distinct().ToList();

            return result;
        }

        public int Add(GroupModel model)
        {
            try
            {
                var group = new EDM.Group()
                {
                    ParentId = model.ParentGroupId,
                    Title = model.Title,
                    Image = model.Image,
                    Message = model.Message,
                    CreatedBy = model.UserId,
                    CreatedOn = UnixTimeBaseClass.UnixTimeNow
                };

                _db.Groups.Add(group);
                _db.SaveChanges();

                return group.GroupId;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Group - Add- " + json, ex);
                throw;
            }
        }

        public bool Edit(GroupModel model)
        {
            try
            {
                var group = _db.Groups.First(x => x.GroupId == model.GroupId);

                group.Title = model.Title;
                group.Image = model.Image;
                group.Message = model.Message;
                group.LastUpdatedBy = model.UserId;
                group.LastUpdatedOn = UnixTimeBaseClass.UnixTimeNow;

                _db.Entry(group).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Group - Edit- " + json, ex);
                throw;
            }
        }

        public bool UpdateImage(StringModel model)
        {
            try
            {
                var group = _db.Groups.First(x => x.GroupId == model.Id);

                group.Image = model.Text;
                group.LastUpdatedOn = UnixTimeBaseClass.UnixTimeNow;

                _db.Entry(group).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Group - Update group pic- " + json, ex);
                throw;
            }
        }

        public bool UpdateTitle(StringModel model)
        {
            try
            {
                var group = _db.Groups.First(x => x.GroupId == model.Id);

                group.Title = model.Text;
                group.LastUpdatedOn = UnixTimeBaseClass.UnixTimeNow;

                _db.Entry(group).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Group - Update Name - " + json, ex);
                throw;
            }
        }

        public bool UpdateMessage(StringModel model)
        {
            try
            {
                var group = _db.Groups.First(x => x.GroupId == model.Id);

                group.Message = model.Text;
                group.LastUpdatedOn = UnixTimeBaseClass.UnixTimeNow;

                _db.Entry(group).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Group - Update Message - " + json, ex);
                throw;
            }
        }

        public bool Delete(int groupId)
        {
            try
            {
                var groups = _db.Groups.Find(groupId);

                if (groups != null) _db.Groups.Remove(groups);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Group - Delete" + groupId, ex);
                throw;
            }

        }
    }
}
