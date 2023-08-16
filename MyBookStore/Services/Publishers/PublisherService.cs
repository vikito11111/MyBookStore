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

        public bool AddPublisher(AddPublisherViewModel model, out string errorMessage)
        {
            var existingPublisher = _context.Publishers.FirstOrDefault(p => p.Name == model.Name);

            if (existingPublisher != null)
            {
                errorMessage = "A publisher with the same name already exists.";
                return false;
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
                        model.Image.CopyToAsync(fileStream);
                    }

                    publisher.Image = "/publisher/" + uniqueFileName;
                }

                _context.Publishers.Add(publisher);
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
