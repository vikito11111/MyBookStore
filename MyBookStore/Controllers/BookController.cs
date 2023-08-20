using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.Services.Books;

namespace MyBookStore.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly MyBookStoreDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookController(IBookService bookService, MyBookStoreDbContext db, UserManager<ApplicationUser> userManager) : base(userManager, db)
        {
            _bookService = bookService;
            this.db = db;
            _userManager = userManager;
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
