using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Diagnostics;
using WebChatApp.Business.Abstract;
using WebChatApp.Dto;

namespace WebChatApp.Hubs
{
    public class ChatHub:Hub
    {
        private readonly IMessageService messageService;

        public ChatHub(IMessageService messageService)
        {
            this.messageService = messageService;
        }
        public async Task SendMessage(string messageJson)
        {
            var message=JsonConvert.DeserializeObject<MessageDto>(messageJson);
            var a = message.ChatId;

            var result=messageService.AddMessage(message);
            if(result.Status==Dto.Result.ResultStatus.Error)
            {
                Debug.WriteLine(result.Status);
            }
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
