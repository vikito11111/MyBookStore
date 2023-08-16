using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.Services.Books;
using MyBookStore.ViewModels.Search;

namespace MyBookStore.Controllers
{
    public class SearchController : Controller
    {
        private readonly IBookService _bookService;
        private readonly MyBookStoreDbContext _context;

        public SearchController(IBookService bookService, MyBookStoreDbContext context)
        {
            _context = context;
            _bookService = bookService;
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
