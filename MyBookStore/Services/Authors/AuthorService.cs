using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.ViewModels.Admin;
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

        public bool AddAuthor(AddAuthorViewModel model, out string errorMessage)
        {
            var existingAuthor = _context.Authors.FirstOrDefault(x => x.Name == model.Name);

            if (existingAuthor != null)
            {
                errorMessage = "An author with the same name already exists.";
                return false;
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
                        model.Image.CopyToAsync(fileStream);
                    }

                    author.Image = "/authors/" + uniqueFileName;
                }

                _context.Authors.Add(author);
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
    }
}
