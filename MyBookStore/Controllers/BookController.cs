using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;

namespace MyBookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly MyBookStoreDbContext db;

        public BookController(MyBookStoreDbContext db)
        {
            this.db = db;
        }

        public IActionResult ViewBook(int id)
        {
            var book = db.Books
                .Include(a => a.Author)
                .Include(a => a.Genre)
                .Include(a => a.Publisher)
                .SingleOrDefault(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}
