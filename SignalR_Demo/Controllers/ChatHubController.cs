using Microsoft.AspNetCore.Mvc;

namespace SignalR_Demo.Controllers
{
    public class ChatHubController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

