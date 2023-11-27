using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WebChatApp.Business.Abstract;
using WebChatApp.Dto;
using WebChatApp.Dto.Error;
using WebChatApp.Dto.Filter;
using WebChatApp.Dto.LoadMore;
using WebChatApp.Dto.Result;
using WebChatApp.Entities.Concrete;
using static System.Formats.Asn1.AsnWriter;

namespace WebChatApp.Business
{
    public class MessageManager:ServiceBase<MessageEntity>,IMessageService
    {
        public MessageManager(IHttpContextAccessor httpContext) : base(httpContext)
        {
        }

        public BusinessLayerResult<MessageDto> AddMessage(MessageDto messageDto)
        {
            var response = new BusinessLayerResult<MessageDto>();


            try
            {
                var entity = new MessageEntity
                {
                    ChatId = messageDto.ChatId,

                    Message = messageDto.Message,
                    SenderName = messageDto.SenderName,
                    SendTime = DateTime.Now,
                    SenderIP = IpAddress,



                    CreateTime = DateTime.Now,

                    CreateIpAddress = IpAddress,
                    IsDeleted = false
                };
                var validationResult = Validator.Validate(entity);

                if (validationResult.IsValid)
                {
                    Add(entity);
                    response.Result = Mapper.Map<MessageDto>(entity);
                }
                if (validationResult.Errors.Count > 0)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        response.AddErrorMessages(ErrorMessageCode.MessageAddMessageValidationError, error.ErrorMessage);
                    }

                }



            }
            catch (Exception ex)
            {

                response.AddErrorMessages(ErrorMessageCode.MessageAddMessageExceptionError, ex.Message);
            }
            
            return response;
        }

        public BusinessLayerResult<MessageDto> UpdateMessage(MessageDto messageDto)
        {
            var response = new BusinessLayerResult<MessageDto>();

            try
            {
                var entity = GetById(messageDto.Id);
                if (entity != null)
                {
                    entity.ChatId = messageDto.ChatId;
                    entity.Message = messageDto.Message;
                    entity.SenderName = messageDto.SenderName;
                    

                    entity.IsDeleted = false;
                    entity.UpdateIpAddress = IpAddress;
                    entity.UpdateTime = DateTime.Now;
                }
                var validatorResult = UpdateValidator.Validate(entity);

                if (validatorResult.IsValid)
                {
                    Update(entity);
                    response.Result = Mapper.Map<MessageDto>(entity);
                }
                if (validatorResult.Errors.Count > 0)
                {

                    foreach (var error in validatorResult.Errors)
                    {
                        response.AddErrorMessages(ErrorMessageCode.MessageUpdateMessageValidationError, error.ErrorMessage);

                    }
                }



            }
            catch (Exception ex)
            {
                response.AddErrorMessages(ErrorMessageCode.MessageUpdateMessageExceptionError, ex.Message);
            }
            
            return response;
        }

        public BusinessLayerResult<MessageDto> DeleteMessage(long messageId)
        {
            var response = new BusinessLayerResult<MessageDto>();
            using (var scope = new TransactionScope())
            {
                try
                {
                    var entity = GetById(messageId);
                    entity.IsDeleted = true;

                    Update(entity);
                    

                }
                catch (Exception ex)
                {
                    response.AddErrorMessages(ErrorMessageCode.MessageDeleteMessageExceptionError, ex.Message);
                }
            }
            return response;
        }

        private bool Filter(MessageEntity message, MessageFilter messageFilter)
        {
           
            return 
                (
                    (string.IsNullOrEmpty(messageFilter.Message) || message.Message.Contains(messageFilter.Message))
                    || (string.IsNullOrEmpty(messageFilter.SenderName) || message.SenderName.Contains(messageFilter.SenderName))
                    || (messageFilter.ChatId == null||messageFilter.ChatId==message.ChatId)

                );


        }

        public BusinessLayerResult<BaseLoadMoreData<MessageDto>> FilterMessageList(BaseLoadMoreFilter<MessageFilter> filter)
        {
            var response = new BusinessLayerResult<BaseLoadMoreData<MessageDto>>();
            try
            {
                var result = new BaseLoadMoreData<MessageDto>();
                List<MessageDto> contentList = new List<MessageDto>();
                List<MessageEntity> dataList;
                if(filter.Filter!= null)
                {
                    var messageFilter=filter.Filter;
                     dataList = GetAll(message =>
                     (messageFilter.Message==null||messageFilter.Message=="" || message.Message.Contains(messageFilter.Message))
                    && (messageFilter.SenderName == null || messageFilter.SenderName == "" || message.SenderName.Contains(messageFilter.SenderName))
                    && (messageFilter.ChatId == null || messageFilter.ChatId == message.ChatId))
                     ;

                }
                else
                {
                     dataList = GetAll();
                }
                
                //contentList = Mapper.Map<List<MessageDto>>(dataList);
                dataList.ForEach(x =>
                {
                    var item = Mapper.Map<MessageDto>(x);
                    item.MySend = x.SenderIP == IpAddress;
                    contentList.Add(item);
                });
                var contentCount = contentList.Count;
                var firstIndex = filter.PageCount * filter.ContentCount;
                var lastIndex = firstIndex + filter.ContentCount;

                if (contentCount < firstIndex)
                {
                    response.AddErrorMessages(ErrorMessageCode.MessageFilterMessageListError, "No more message");
                }
                else
                {
                    result.Values = new List<MessageDto>();
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
                response.AddErrorMessages(ErrorMessageCode.MessageFilterMessageListExceptionError, ex.Message);
            }
            return response;
        }

        public BusinessLayerResult<MessageDto> GetMessage(long id)
        {
            var response = new BusinessLayerResult<MessageDto>();
            try
            {
                var entity = GetById(id);
                if (entity != null)
                {
                    response.Result = Mapper.Map<MessageDto>(entity);

                }
                else
                {
                    response.AddErrorMessages(ErrorMessageCode.MessageGetMessageNotFoundExceptionError, "Message was not found.");
                }
            }
            catch (Exception ex)
            {
                response.AddErrorMessages(ErrorMessageCode.MessageGetMessageExceptionError, ex.Message);
            }
            return response;
        }

    }
}
