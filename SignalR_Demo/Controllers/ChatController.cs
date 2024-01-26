using Microsoft.AspNetCore.Mvc;

namespace SignalR_Demo.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

