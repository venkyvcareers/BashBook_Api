using System.Collections.Generic;
using BashBook.BAL.Post;
using BashBook.Model.Post;
using System.Web.Http;

namespace BashBook.API.Controllers.Post
{
    [Authorize]
    [RoutePrefix("api/Chat")]
    public class ChatController : BaseController
    {
        readonly ChatOperation _post = new ChatOperation();

        [HttpGet]
        [Route("GetAll/{lastChatId}")]
        public List<ChatModel> GetAll(int lastChatId)
        {
            return _post.SelectAll(lastChatId);
        }

        [HttpPost]
        [Route("Add")]
        public int Add(ChatModel model)
        {
            return _post.Add(model);
        }
    }
}
