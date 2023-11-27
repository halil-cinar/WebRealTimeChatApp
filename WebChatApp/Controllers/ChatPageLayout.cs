using Microsoft.AspNetCore.Mvc;

namespace WebChatApp.Controllers
{
    public class ChatPageLayout : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
