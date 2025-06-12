using Microsoft.AspNetCore.Mvc;

namespace PersonalPortfolio.Controllers
{
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
