using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashBook.Model.Post
{
    public class ChatModel
    {
        public int ChatId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public long PostedOn { get; set; }
    }

}
