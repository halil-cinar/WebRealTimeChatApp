using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChatApp.Dto.Result;
using WebChatApp.Dto;
using WebChatApp.Dto.Filter;
using WebChatApp.Dto.LoadMore;
using WebChatApp.Entities.Concrete;

namespace WebChatApp.Business.Abstract
{
    public interface IChatService
    {
        public BusinessLayerResult<ChatDto> GetChat(long id);
        public BusinessLayerResult<BaseLoadMoreData<ChatDto>> FilterChatList(BaseLoadMoreFilter<ChatFilter> filter);
       
        public BusinessLayerResult<ChatDto> DeleteChat(long chatId);
        public BusinessLayerResult<ChatDto> UpdateChat(ChatDto chatDto);
        public BusinessLayerResult<ChatDto> AddChat(ChatDto chatDto);

    }
}
