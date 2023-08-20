using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.Services.Books;
using MyBookStore.ViewModels.Search;

namespace MyBookStore.Controllers
{
    public class SearchController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly MyBookStoreDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SearchController(IBookService bookService, UserManager<ApplicationUser> userManager, MyBookStoreDbContext context) : base(userManager, context)
        {
            _context = context;
            _bookService = bookService;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Index(SearchViewModel searchModel)
        {
            var resultBooks = _bookService.SearchBooks(searchModel);

            if (resultBooks.Count == 0)
            {
                ViewBag.ErrorMessage = "No books found matching your search criteria.";
                return View("Index");
            }

            return View("Results", resultBooks);
        }
    }
}
