using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.ViewModels.Admin;
using MyBookStore.ViewModels.Search;

namespace MyBookStore.Services.Books
{
    public class BookService : IBookService
    {
        private readonly MyBookStoreDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookService(MyBookStoreDbContext context, IWebHostEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool AddBook(AddNewBookViewModel model, out string errorMessage)
        {
            try
            {
                var book = new Book
                {
                    Title = model.Title,
                    Height = model.Height,
                    Price = model.Price,
                    PublicationDate = model.PublicationDate,
                    AuthorId = model.AuthorId,
                    PublisherId = model.PublisherId,
                    GenreId = model.GenreId,
                    SubGenreId = model.SubGenreId,
                };

                if (model.CoverImage != null && model.CoverImage.Length > 0)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.CoverImage.FileName;

                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.CoverImage.CopyTo(fileStream);
                    }

                    book.CoverImage = "/images/" + uniqueFileName;
                }

                _context.Books.Add(book);
                _context.SaveChanges();

                errorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public Book GetBookById(int id)
        {
            var book = _context.Books
            .Include(a => a.Author)
            .Include(a => a.Genre)
            .Include(a => a.Publisher)
            .SingleOrDefault(m => m.Id == id);

            return book;
        }

        public List<Book> GetNewestBooks()
        {
            var currentUserId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

            var newestBooks = _context.Books
                .Include(x => x.Author)
                .Include(x => x.Genre)
                .OrderByDescending(book => book.PublicationDate)
                .Take(10)
                .Select(b => new
                {
                    Book = b,
                    AverageRating = _context.BookRatings.Where(br => br.BookId == b.Id).DefaultIfEmpty().Average(br => (decimal?)br.Rating),
                    UserRatingObj = _context.BookRatings.FirstOrDefault(br => br.BookId == b.Id && br.UserId == currentUserId)
                })
                .ToList()
                .Select(b =>
                {
                    b.Book.AverageRating = b.AverageRating ?? 0;
                    b.Book.UserRating = b.UserRatingObj != null ? b.UserRatingObj.Rating : 0;
                    return b.Book;
                })
                .ToList();

            return newestBooks;
        }

        public List<Book> SearchBooks(SearchViewModel searchModel)
        {
            IQueryable<Book> books = _context.Books
                .Include(a => a.Author)
                .Include(a => a.Genre)
                .Include(a => a.Publisher)
                .AsQueryable();

            switch (searchModel.SearchType)
            {
                case "Title":
                    books = books.Where(b => b.Title.Contains(searchModel.Query));
                    break;
                case "Genre":
                    books = books.Where(b => b.Genre.Name.Contains(searchModel.Query));
                    break;
                case "Author":
                    books = books.Where(b => b.Author.Name.Contains(searchModel.Query));
                    break;
            }

            return books.ToList();
        }
    }
}
