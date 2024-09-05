using Microsoft.AspNetCore.Mvc;

namespace ProjectBackend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
