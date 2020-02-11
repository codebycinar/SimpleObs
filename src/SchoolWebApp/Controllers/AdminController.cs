using Microsoft.AspNetCore.Mvc;

namespace SchoolWebApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}