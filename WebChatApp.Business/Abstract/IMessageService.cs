using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChatApp.Dto;
using WebChatApp.Dto.Filter;
using WebChatApp.Dto.LoadMore;
using WebChatApp.Dto.Result;

namespace WebChatApp.Business.Abstract
{
    public interface IMessageService
    {
        public BusinessLayerResult<MessageDto> GetMessage(long id);
        public BusinessLayerResult<BaseLoadMoreData<MessageDto>> FilterMessageList(BaseLoadMoreFilter<MessageFilter> filter);

        public BusinessLayerResult<MessageDto> DeleteMessage(long messageId);
        public BusinessLayerResult<MessageDto> UpdateMessage(MessageDto messageDto);
        public BusinessLayerResult<MessageDto> AddMessage(MessageDto messageDto);
    }
}
