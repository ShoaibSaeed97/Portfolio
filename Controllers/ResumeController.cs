using Microsoft.AspNetCore.Mvc;

namespace PersonalPortfolio.Controllers
{
    public class ResumeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
