using Microsoft.AspNetCore.Mvc;

namespace SignalR_Demo.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
