using Microsoft.AspNetCore.Mvc;

namespace SignalR_Demo.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
