using FluentValidation;

using WebChatApp.Business.Abstract;

using WebChatApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebChatApp.Dto;
using WebChatApp.Dto.Result;
using WebChatApp.Dto.Error;
using WebChatApp.Dto.Filter;
using WebChatApp.Dto.LoadMore;
using Microsoft.AspNetCore.Http;

namespace WebChatApp.Business
{
    public class ChatManager : ServiceBase<ChatEntity>,IChatService
    {
        public ChatManager(IHttpContextAccessor httpContext) : base(httpContext)
        {
        }

        public BusinessLayerResult<ChatDto> AddChat(ChatDto chatDto)
        {
            var response = new BusinessLayerResult<ChatDto>();
            try
            {
                var entity = new ChatEntity
                {
                    
                    Name= chatDto.Name,
                    
                    
                    


                    CreateTime = DateTime.Now,
                   
                    CreateIpAddress = IpAddress,
                    IsDeleted = false,
                };
                var validationResult = Validator.Validate(entity);

                if (validationResult.IsValid)
                {
                    Add(entity);
                    response.Result = Mapper.Map<ChatDto>(entity);
                }
                if (validationResult.Errors.Count > 0)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        response.AddErrorMessages(ErrorMessageCode.ChatAddChatValidationError, error.ErrorMessage);
                    }

                }

            }
            catch (Exception ex)
            {
                response.AddErrorMessages(ErrorMessageCode.ChatAddChatExceptionError, ex.Message);
            }
            return response;
        }

        public BusinessLayerResult<ChatDto> UpdateChat(ChatDto chatDto)
        {
            var response = new BusinessLayerResult<ChatDto>();

            try
            {
                var entity = GetById(chatDto.Id);
                if (entity != null)
                {
                   entity.Name= chatDto.Name;


                    entity.IsDeleted = false;
                    entity.UpdateIpAddress = IpAddress;
                    entity.UpdateTime = DateTime.Now;
                }
                var validatorResult = UpdateValidator.Validate(entity);

                if (validatorResult.IsValid)
                {
                    Update(entity);
                    response.Result = Mapper.Map<ChatDto>(entity);
                }
                if (validatorResult.Errors.Count > 0)
                {
                    foreach (var error in validatorResult.Errors)
                    {
                        response.AddErrorMessages(ErrorMessageCode.ChatUpdateChatValidationError, error.ErrorMessage);

                    }
                }
            }
            catch (Exception ex)
            {
                response.AddErrorMessages(ErrorMessageCode.ChatUpdateChatExceptionError, ex.Message);
            }
            return response;
        }

        public BusinessLayerResult<ChatDto> DeleteChat(long chatId)
        {
            var response = new BusinessLayerResult<ChatDto>();
            try
            {
                var entity = GetById(chatId);
                entity.IsDeleted = true;

                Update(entity);
            }
            catch (Exception ex)
            {
                response.AddErrorMessages(ErrorMessageCode.ChatDeleteChatExceptionError, ex.Message);
            }
            return response;
        }

        private bool Filter(ChatEntity chat, ChatFilter chatFilter)
        {
            return 
                (chatFilter==null||(
                    (string.IsNullOrEmpty(chatFilter.Name) || chat.Name.Contains(chatFilter.Name))
                    ||(chatFilter.Id==null||chatFilter.Id==chat.Id)
                ));
          
         
        }

        public BusinessLayerResult<BaseLoadMoreData<ChatDto>> FilterChatList(BaseLoadMoreFilter<ChatFilter> filter)
        {
            var response = new BusinessLayerResult<BaseLoadMoreData<ChatDto>>();
            try
            {
                var result = new BaseLoadMoreData<ChatDto>();
                List<ChatDto> contentList = new List<ChatDto>();
                var chatFilter= filter.Filter;
                if (chatFilter != null)
                {
                    contentList = Mapper.Map<List<ChatDto>>(GetAll(chat =>
                    (
                        (chatFilter.Name == null || chat.Name.Contains(chatFilter.Name))
                        && (chatFilter.Id == null || chatFilter.Id == chat.Id)
                    )));
                }
                else
                {
                    contentList = Mapper.Map<List<ChatDto>>(GetAll());
                }

                var contentCount = contentList.Count;
                var firstIndex = filter.PageCount * filter.ContentCount;
                var lastIndex = firstIndex + filter.ContentCount;

                if (contentCount < firstIndex)
                {
                    response.AddErrorMessages(ErrorMessageCode.ChatFilterChatListError, "No more chat");
                }
                else
                {
                    result.Values = new List<ChatDto>();
                    for (int i = 0; i < lastIndex; i++)
                    {
                        if (i >= contentCount)
                        {
                            break;
                        }
                        result.Values.Add(contentList[i]);
                    }

                    result.NextPage = (lastIndex < contentCount);

                    result.PreviousPage = (firstIndex != 0);
                }
                response.Result = result;
            }
            catch (Exception ex)
            {
                response.AddErrorMessages(ErrorMessageCode.ChatFilterChatListExceptionError, ex.Message);
            }
            return response;
        }

        public BusinessLayerResult<ChatDto> GetChat(long id)
        {
            var response = new BusinessLayerResult<ChatDto>();
            try
            {
                var entity = GetById(id);
                if (entity != null)
                {
                    response.Result = Mapper.Map<ChatDto>(entity);

                }
                else
                {
                    response.AddErrorMessages(ErrorMessageCode.ChatGetChatNotFoundExceptionError, "Chat was not found.");
                }
            }
            catch (Exception ex)
            {
                response.AddErrorMessages(ErrorMessageCode.ChatGetChatExceptionError, ex.Message);
            }
            return response;
        }
    }
}


