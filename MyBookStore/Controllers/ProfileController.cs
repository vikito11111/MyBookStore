using Microsoft.AspNetCore.Mvc;

namespace MyBookStore.Controllers
{
    public class ProfileController : Controller
    {



        public IActionResult Index()
        {
            return View();
        }
    }
}
