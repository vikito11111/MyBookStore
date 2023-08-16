using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.ViewModels.Search;

namespace MyBookStore.Controllers
{
    public class SearchController : Controller
    {
        private readonly MyBookStoreDbContext db;

        public SearchController(MyBookStoreDbContext db)
        {
            this.db = db;
        }

        //[Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Index(SearchViewModel model)
        {
            IQueryable<Book> books = db.Books
                .Include(a => a.Author)
                .Include(a => a.Genre)
                .Include(a => a.Publisher)
                .AsQueryable();

            switch (model.SearchType)
            {
                case "Title":
                    books = books.Where(b => b.Title.Contains(model.Query));
                    break;
                case "Genre":
                    books = books.Where(b => b.Genre.Name.Contains(model.Query));
                    break;
                case "Author":
                    books = books.Where(b => b.Author.Name.Contains(model.Query));
                    break;
            }

            var resultBooks = books.ToList();

            if (resultBooks.Count == 0)
            {
                // Handle the case when no results are found
                ViewBag.ErrorMessage = "No books found matching your search criteria.";
                return View("Index"); // or return another view if you want
            }

            return View("Results", resultBooks);
        }
    }
}
