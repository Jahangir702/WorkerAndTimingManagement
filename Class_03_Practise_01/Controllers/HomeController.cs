using Microsoft.AspNetCore.Mvc;

namespace Class_03_Practise_01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
