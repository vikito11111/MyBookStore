using Microsoft.AspNetCore.Mvc;

namespace MyBookStore.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
