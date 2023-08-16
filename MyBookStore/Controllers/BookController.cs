using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Services.Books;

namespace MyBookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly MyBookStoreDbContext db;

        public BookController(IBookService bookService, MyBookStoreDbContext db)
        {
            _bookService = bookService;
            this.db = db;
        }

        public IActionResult ViewBook(int id)
        {
            var book = _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}
