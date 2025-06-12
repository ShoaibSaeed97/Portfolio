using Microsoft.AspNetCore.Mvc;

namespace PersonalPortfolio.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
