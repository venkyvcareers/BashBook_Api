using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Transactions;
using BashBook.DAL.Group;
using BashBook.Model;
using BashBook.Model.Group;
using System.Web.Script.Serialization;
using BashBook.Model.Global;
using BashBook.Model.Lookup;

namespace BashBook.BAL.Group
{
    public class GroupOperation : BaseBusinessAccessLayer
    {
        readonly GroupRepository _group = new GroupRepository();
        readonly GroupUserRepository _groupUser = new GroupUserRepository();

        public List<GroupPreviewModel> GetPreviewList(int userId)
        {
            return _group.GetPreviewList(userId);
        }

        public ManageGroupModel GetForEdit(int groupId)
        {
            var result = _group.GetForEdit(groupId);

            result.Users = _groupUser.GetUsersWithRole(groupId);
            return result;
        }

        public int Add(ManageGroupModel model)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    model.Image = Cdn.Base64ToImageUrl(model.Image);
                    var groupId = _group.Add(new GroupModel()
                    {
                        Image = model.Image,
                        Title = model.Title,
                        Message = model.Message,
                        UserId = model.UserId
                    });

                    foreach (var user in model.Users)
                    {
                        _groupUser.Add(new GroupUserModel()
                        {
                            GroupId = groupId,
                            UserId = user.UserId,
                            CreatedBy = model.UserId,
                            RoleId = user.RoleId
                        });
                    }

                    scope.Complete();

                    return groupId;
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

        public bool Edit(ManageGroupModel model)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    model.Image = Cdn.Base64ToImageUrl(model.Image);
                    var groupId = _group.Edit(new GroupModel()
                    {
                        Image = model.Image,
                        Title = model.Title,
                        Message = model.Message,
                        UserId = model.UserId,
                        GroupId = model.GroupId,
                    });


                    var existedUsers = _groupUser.GetAllUsers(model.GroupId);
                    var newUsers = model.Users.Select(x => x.UserId).ToList();
                    foreach (var user in model.Users)
                    {
                        if (!existedUsers.Contains(user.UserId))
                        {
                            _groupUser.Add(new GroupUserModel()
                            {
                                GroupId = model.GroupId,
                                UserId = user.UserId,
                                CreatedBy = model.UserId,
                                RoleId = user.RoleId
                            });
                        }
                        else
                        {
                            _groupUser.UpdateRole(new UpdateRoleModel()
                            {
                                UserId = user.UserId,
                                GroupId = model.GroupId,
                                RoleId = user.RoleId
                            });
                        }
                    }

                    foreach (var item in existedUsers)
                    {
                        if (!newUsers.Contains(item))
                        {
                            _groupUser.DeleteUser(new GroupUserIdModel()
                            {
                                UserId = item,
                                GroupId = model.GroupId
                            });
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
            return _group.UpdateImage(model);
        }

        public bool UpdateMessage(StringModel model)
        {
            return _group.UpdateMessage(model);
        }

        public bool UpdateTitle(StringModel model)
        {
            return _group.UpdateTitle(model);
        }

        public bool Delete(int groupId)
        {

            using (var scope = new TransactionScope())
            {
                try
                {
                    var users = _groupUser.GetAllUsers(groupId);

                    foreach (var userId in users)
                    {
                        _groupUser.DeleteUser(new GroupUserIdModel()
                        {
                            UserId = userId,
                            GroupId = groupId
                        });
                    }
                    var result = _group.Delete(groupId);

                    scope.Complete();

                    return result;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    Log.Error("BL-Group - Delete - " + groupId, ex);
                    throw new ReturnExceptionModel(new CustomExceptionModel() { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
                }
            }

        }
    }
}
