using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashBook.DAL.Post;
using BashBook.Model.Post;

namespace BashBook.BAL.Post
{
    public class ChatOperation : BaseBusinessAccessLayer
    {
        readonly ChatRepository _post = new ChatRepository();

        public List<ChatModel> SelectAll(int lastChatId)
        {
            return _post.GetAll(lastChatId);
        }

        public int Add(ChatModel model)
        {
            return _post.Add(model);
        }


    }
}
