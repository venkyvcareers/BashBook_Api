using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web.Script.Serialization;
using BashBook.DAL.Event;
using BashBook.Model;
using BashBook.Model.Event;
using BashBook.Model.Global;
using BashBook.Model.Group;

namespace BashBook.BAL.Event
{
    public class EventOperation : BaseBusinessAccessLayer
    {
        readonly EventRepository _event = new EventRepository();
        readonly EventUserRepository _eventUser =new EventUserRepository();

        public List<EventModel> GetAllByEntity(int entityTypeId, int entityId)
        {
            return _event.GetAllByEntity(entityTypeId, entityId);
        }

        public EventModel GetById(int postId)
        {
            return _event.GetById(postId);
        }

        public int Add(ManageEventModel model)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    model.Image = Cdn.Base64ToImageUrl(model.Image);
                    var eventId = _event.Add(new EventModel()
                    {
                        Image = model.Image,
                        Title = model.Title,
                        Message = model.Message,
                        CreatedBy = model.UserId,
                        DateTime = model.DateTime,
                        EntityId = model.EntityId,
                        EntityTypeId = model.EntityTypeId,
                        OccationId = model.OccationId
                    });

                    foreach (var user in model.Users)
                    {
                        _eventUser.Add(new EventUserModel()
                        {
                            UserId = user.UserId,
                            CreatedBy = model.UserId,
                            EventId = eventId
                        });
                    }

                    scope.Complete();

                    return eventId;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string json = js.Serialize(model);
                    Log.Error("BL-Group - Add" + json, ex);
                    throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
                }
            }
        }

        public bool Edit(ManageEventModel model)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    model.Image = Cdn.Base64ToImageUrl(model.Image);
                    var groupId = _event.Edit(new EventModel()
                    {
                        Image = model.Image,
                        Title = model.Title,
                        Message = model.Message,
                        CreatedBy = model.UserId,
                        DateTime = model.DateTime,
                        EntityId = model.EntityId,
                        EntityTypeId = model.EntityTypeId,
                        OccationId = model.OccationId
                    });


                    var existedUsers = _eventUser.GetAllUsers(model.EventId);
                    var newUsers = model.Users.Select(x => x.UserId).ToList();
                    foreach (var user in model.Users)
                    {
                        if (!existedUsers.Contains(user.UserId))
                        {
                            _eventUser.Add(new EventUserModel()
                            {
                                UserId = user.UserId,
                                CreatedBy = model.UserId,
                            });
                        }
                        else
                        {
                            _eventUser.UpdateRole(new UpdateRoleModel()
                            {
                                UserId = user.UserId,
                                GroupId = model.EventId,
                                RoleId = user.RoleId
                            });
                        }
                    }

                    foreach (var item in existedUsers)
                    {
                        if (!newUsers.Contains(item))
                        {
                            //_eventUser.DeleteUser(new GroupUserIdModel()
                            //{
                            //    UserId = item,
                            //    GroupId = model.EventId
                            //});
                        }
                    }

                    scope.Complete();

                    return groupId;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string json = js.Serialize(model);
                    Log.Error("BL-Group - Edit" + json, ex);
                    throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
                }
            }

        }

        public bool UpdateImage(StringModel model)
        {
            model.Text = Cdn.Base64ToImageUrl(model.Text);
            return _event.UpdateImage(model);
        }

        public bool UpdateMessage(StringModel model)
        {
            return _event.UpdateMessage(model);
        }

        public bool UpdateTitle(StringModel model)
        {
            return _event.UpdateTitle(model);
        }

        public bool Delete(int postId)
        {
            return _event.Delete(postId);
        }

        public bool DeleteByEntity(int entityTypeId, int entityId)
        {
            return _event.DeleteByEntity(entityTypeId, entityId);
        }
    }
}
