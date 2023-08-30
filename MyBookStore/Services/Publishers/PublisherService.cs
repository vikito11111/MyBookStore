using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.ViewModels.Admin;
using System.Security.Policy;
using Publisher = MyBookStore.Models.Publisher;

namespace MyBookStore.Services.Publishers
{
    public class PublisherService : IPublisherService
    {
        private readonly MyBookStoreDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PublisherService(MyBookStoreDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<(bool isSuccess, string errorMessage)> AddPublisherAsync(AddPublisherViewModel model)
        {
            var existingPublisher = _context.Publishers.FirstOrDefault(p => p.Name == model.Name);

            if (existingPublisher != null)
            {
                return (false, "An author with the same name already exists.");
            }

            try
            {
                var publisher = new Publisher
                {
                    Name = model.Name,
                    Bio = model.Bio,
                    Established = model.Established
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

                    publisher.Image = "/images/" + uniqueFileName;
                }

                _context.Publishers.Add(publisher);
                _context.SaveChanges();

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<Publisher> GetPublisherById(int id)
        {
            return await _context.Publishers
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
