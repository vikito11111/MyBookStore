using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.Services.Books;
using MyBookStore.ViewModels.Home;

namespace MyBookStore.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookRatingService _bookRatingService;
        private readonly IBookService _bookService;
        private readonly MyBookStoreDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookController(IBookRatingService bookRatingService, IBookService bookService, MyBookStoreDbContext context, UserManager<ApplicationUser> userManager) : base(userManager, context)
        {
            _bookRatingService = bookRatingService;
            _bookService = bookService;
            _context = context;
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

        [HttpPost]
        public IActionResult RateBook([FromBody] RatingModel model)
        {
            int bookId = model.BookId;
            int rating = model.Rating;

            var currentUserId = _userManager.GetUserId(User);

            decimal averageRating = _bookRatingService.RateBook(bookId, rating, currentUserId);

            return Json(new { success = true, rating = rating, averageRating = averageRating });
        }
    }
}
