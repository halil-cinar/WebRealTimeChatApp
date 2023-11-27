using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChatApp.Dto.Abstract;

namespace WebChatApp.Dto
{
    public class MessageDto : DtoBase
    {
        public string SenderName { get; set; }
        public string Message { get; set; }
        public long ChatId { get; set; }
        public DateTime SendTime { get; set; }
        public bool MySend { get; set; }

    }
}
