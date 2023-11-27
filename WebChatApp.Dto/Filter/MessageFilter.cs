using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChatApp.Dto.Filter
{
    public class MessageFilter
    {
        public string? SenderName { get; set; }
        public string? Message { get; set; }

        public long? ChatId { get; set; }
    }
}
