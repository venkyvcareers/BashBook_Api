using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using BashBook.DAL.EDM;
using BashBook.Model.Post;
using BashBook.Utility;

namespace BashBook.DAL.Post
{
    public class ChatRepository : BaseDataAccessLayer
    {
        public BashBookEntities _db = new BashBookEntities();
        public List<ChatModel> GetAll(int lastChatId)
        {
            try
            {
                var result = (from c in _db.Chats
                    where c.ChatId > lastChatId
                              select new ChatModel
                              {
                                  ChatId = c.ChatId,
                                  Message = c.Message,
                                  PostedOn = c.PostedOn,
                                  UserName = c.UserName
                              }).OrderBy(x=>x.ChatId).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Chat - GetAll", ex);
                throw;
            }

        }

        public int Add(ChatModel model)
        {
            try
            {
                var chat = new EDM.Chat()
                {
                    Message = model.Message,
                    UserName = model.UserName,
                    PostedOn = UnixTimeBaseClass.UnixTimeNow
                };

                _db.Chats.Add(chat);
                _db.SaveChanges();

                return chat.ChatId;
            }
            catch (Exception ex)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                Log.Error("Chat - Add - " + json, ex);
                throw;
            }

        }


    }
}
