using Microsoft.AspNetCore.Mvc;

namespace HephzibahZion.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
