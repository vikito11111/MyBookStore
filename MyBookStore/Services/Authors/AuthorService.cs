using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.ViewModels.Admin;
using MyBookStore.ViewModels.Author;
using MyBookStore.ViewModels.Books;
using System.Security.Policy;

namespace MyBookStore.Services.Authors
{
    public class AuthorService : IAuthorService
    {
        private readonly MyBookStoreDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AuthorService(MyBookStoreDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<(bool isSuccess, string errorMessage)> AddAuthorAsync(AddAuthorViewModel model)
        {
            var existingAuthor = _context.Authors.FirstOrDefault(x => x.Name == model.Name);

            if (existingAuthor != null)
            {
                return (false, "An author with the same name already exists.");
            }

            try
            {
                var author = new Author
                {
                    Name = model.Name,
                    Bio = model.Bio,
                    Birth = model.Birth
                };

                if (model.Image != null)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;

                    var imagesFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");

                    Directory.CreateDirectory(imagesFolder);

                    var filePath = Path.Combine(imagesFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }

                    author.Image = "/images/" + uniqueFileName;
                }

                _context.Authors.Add(author);
                _context.SaveChanges();

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
