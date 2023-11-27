using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChatApp.Dto.Abstract
{
    public abstract class DtoBase
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

    }
}
