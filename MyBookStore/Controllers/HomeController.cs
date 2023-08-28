using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.Services.Books;
using MyBookStore.ViewModels.Home;
using System.Diagnostics;

namespace MyBookStore.Controllers
{
	public class HomeController : BaseController
	{
		private readonly IBookService _bookService;
		private readonly ILogger<HomeController> _logger;
        private readonly MyBookStoreDbContext _context;
        private const int MaxBooksToShow = 5;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IBookService bookService, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, MyBookStoreDbContext context) : base(userManager, context)
        {
			_context = context;
			_logger = logger;
			_bookService = bookService;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
            var newestBooks = _bookService.GetNewestBooks();

            if (newestBooks == null)
            {
                return NotFound();
            }

            var model = new IndexViewModel
            {
                NewestBooks = newestBooks,
            };

			if (User.Identity.IsAuthenticated)
			{
				var userId = _userManager.GetUserId(User);

				model.RecommendedBooks = _bookService.GetRecommendedBooksForUser(userId);

                model.BooksBySameAuthor = _bookService.GetBooksBySameAuthorExcludingUserLibrary(userId);
            }

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
