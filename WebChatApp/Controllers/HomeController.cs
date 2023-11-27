using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebChatApp.Business.Abstract;
using WebChatApp.Dto;
using WebChatApp.Dto.LoadMore;
using WebChatApp.Dto.Result;
using WebChatApp.Models;

namespace WebChatApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor httpContext;
        private readonly IChatService chatService;
        private readonly IMessageService messageService;

        public HomeController(ILogger<HomeController> logger,IHttpContextAccessor httpContext,IChatService chatService,IMessageService messageService)
        {
            _logger = logger;
            this.httpContext = httpContext;
            this.chatService = chatService;
            this.messageService = messageService;
        }

        [HttpGet("/{chatId}")]
        [HttpGet("/")]

        public IActionResult Index(long? chatId, [FromQuery] int chatPageIndex = 0, [FromQuery] int messagePageIndex = 0)
        { 
            if(chatId == null)
            {
                chatId = 1;
            }
            ViewBag.chatPageIndex = chatPageIndex;
            ViewBag.messagePageIndex = messagePageIndex;
            
            var chatResult = chatService.FilterChatList(new Dto.Filter.BaseLoadMoreFilter<Dto.Filter.ChatFilter>
            {
                ContentCount = 10,
                PageCount = chatPageIndex,
                
            });
            if (chatResult.ErrorMessages.Count == 0) 
            {
                ViewBag.Chats = chatResult.Result;
                if (!chatResult.Result.NextPage)
                {
                    ViewBag.chatPageIndex--;
                }
            }
            

            var messageResult = messageService.FilterMessageList(new Dto.Filter.BaseLoadMoreFilter<Dto.Filter.MessageFilter>
            {
                ContentCount = 50,
                PageCount = messagePageIndex,
                Filter=new Dto.Filter.MessageFilter
                {
                    ChatId=(long)chatId
                }
            });
            if (messageResult.Status == ResultStatus.Success)
            {
                 ViewBag.Messages = messageResult.Result;
                if (!messageResult.Result.NextPage)
                {
                    ViewBag.messagePageIndex--;
                }
            }
            
           
            var selectedChatResult = chatService.FilterChatList(new Dto.Filter.BaseLoadMoreFilter<Dto.Filter.ChatFilter>
            {
                ContentCount = 1,
                PageCount = 0,
                Filter = new Dto.Filter.ChatFilter
                {
                    Id = (long)chatId
                }
            });
            if (selectedChatResult.Status == Dto.Result.ResultStatus.Success&&selectedChatResult.Result.Values.Count>0)
            {
                ViewBag.SelectedChat = selectedChatResult.Result.Values[0];
            }
            

            return View();
        }

        [HttpPost("/")]
        [HttpPost("/{chatId}")]
        public IActionResult AddChat(ChatDto chat)
        {
            chatService.AddChat(chat);
            return RedirectToAction("Index");
        }

        //[HttpPost("/addMessage")]
        ////[HttpPut("/{chatId}")]
        //public IActionResult AddMessage(MessageDto message)
        //{
        //    messageService.AddMessage(message);
        //    return RedirectToAction("Index");
        //}

       
        public IActionResult Messages()
        {
            return PartialView();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}