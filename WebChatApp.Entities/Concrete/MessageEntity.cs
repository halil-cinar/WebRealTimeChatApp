using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChatApp.Entities.Abstract;

namespace WebChatApp.Entities.Concrete
{
    public class MessageEntity:EntityBase
    {
        public string SenderName { get; set; }
        public string Message { get; set; }

        public long ChatId { get; set; }

        public string SenderIP { get; set; }

        [ForeignKey("ChatId")]
        public ChatEntity Chat { get; set; }

        public DateTime SendTime { get; set; }
    }
}
