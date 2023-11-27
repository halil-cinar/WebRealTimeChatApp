using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChatApp.Dto;
using WebChatApp.Entities.Concrete;

namespace WebChatApp.Business.DependencyResolvers.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            #region ChatMapping
                   
            CreateMap<ChatDto, ChatEntity>()
                    .ForMember(x => x.CreateTime, opt => opt.Ignore())
                    .ForMember(x => x.CreateIpAddress, opt => opt.Ignore())
                    .ForMember(x => x.UpdateTime, opt => opt.Ignore())
                    .ForMember(x => x.UpdateIpAddress, opt => opt.Ignore())
                    .ReverseMap();
            #endregion
            #region MessageMapping

            CreateMap<MessageDto, MessageEntity>()
                    .ForMember(x => x.CreateTime, opt => opt.Ignore())
                    .ForMember(x => x.CreateIpAddress, opt => opt.Ignore())
                    .ForMember(x => x.UpdateTime, opt => opt.Ignore())
                    .ForMember(x => x.UpdateIpAddress, opt => opt.Ignore())
                    .ReverseMap();

            #endregion

        }
    }
}
