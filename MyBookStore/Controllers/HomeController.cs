using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.ViewModels.Home;
using System.Diagnostics;

namespace MyBookStore.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> logger;
        private readonly MyBookStoreDbContext db;
        private const int MaxBooksToShow = 5;

		public HomeController(ILogger<HomeController> logger, MyBookStoreDbContext db)
		{
			this.db = db;
			this.logger = logger;
		}

		public IActionResult Index()
		{
            var newestBooks = db.Books.Include(a => a.Author).Include(a => a.Genre).OrderByDescending(book => book.PublicationDate).Take(MaxBooksToShow).ToList();

            if (newestBooks == null)
            {
                return NotFound();
            }

            var model = new IndexViewModel
            {
                NewestBooks = newestBooks,
            };

            return View(model);
        }

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}