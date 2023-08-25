using MyBookStore.Data;
using MyBookStore.Models;

namespace MyBookStore.Services.Books
{
    public class BookRatingService : IBookRatingService
    {
        private readonly MyBookStoreDbContext _context;

        public BookRatingService(MyBookStoreDbContext context)
        {
            _context = context;
        }

        public decimal RateBook(int bookId, int rating, string userId)
        {
            var existingRating = _context.BookRatings
                .FirstOrDefault(r => r.BookId == bookId && r.UserId == userId);

            if (existingRating != null)
            {
                existingRating.Rating = rating;
            }
            else
            {
                var newRating = new BookRating
                {
                    BookId = bookId,
                    UserId = userId,
                    Rating = rating
                };

                _context.BookRatings.Add(newRating);
            }

            var book = _context.Books.Find(bookId);

            var allRatingsForThisBook = _context.BookRatings
                .Where(r => r.BookId == bookId)
                .ToList();

            if (allRatingsForThisBook.Count > 0)
            {
                double totalRatingValue = allRatingsForThisBook.Sum(r => r.Rating);

                book.AverageRating = (decimal)(totalRatingValue / allRatingsForThisBook.Count);
            }
            else
            {
                book.AverageRating = rating;
            }

            _context.SaveChanges();

            return book.AverageRating;
        }
    }
}
