using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.ViewModels.Admin;
using System.Security.Policy;
using Publisher = MyBookStore.Models.Publisher;

namespace MyBookStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly MyBookStoreDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminController(MyBookStoreDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        /*public IActionResult AddBook()
        {
            var authors = _context.Authors.ToList();
            var genres = _context.Genres.ToList();
            var subGenres = _context.SubGenres.ToList();
            var publishers = _context.Publishers.ToList();

            var viewModel = new AddBookPageViewModel
            {
                Authors = new SelectList(authors, "Id", "Name"),
                Genres = new SelectList(genres, "Id", "Name"),
                SubGenres = new SelectList(subGenres, "Id", "Name"),
                Publishers = new SelectList(publishers, "Id", "Name")
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddBook(AddBookPageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Book.Author.Id == 0 && !string.IsNullOrEmpty(viewModel.NewAuthor))
                {
                    var newAuthor = new Author { Name = viewModel.NewAuthor };
                    _context.Authors.Add(newAuthor);
                    _context.SaveChanges();
                    viewModel.Book.Author.Id = newAuthor.Id;
                }

                if (viewModel.Book.Genre.Id == 0 && !string.IsNullOrEmpty(viewModel.NewGenre))
                {
                    var newGenre = new Genre { Name = viewModel.NewGenre };
                    _context.Genres.Add(newGenre);
                    _context.SaveChanges();
                    viewModel.Book.Genre.Id = newGenre.Id;
                }

                if (viewModel.Book.SubGenre.Id == 0 && !string.IsNullOrEmpty(viewModel.NewSubGenre))
                {
                    var newSubGenre = new SubGenre { Name = viewModel.NewSubGenre };
                    _context.SubGenres.Add(newSubGenre);
                    _context.SaveChanges();
                    viewModel.Book.SubGenre.Id = newSubGenre.Id;
                }

                if (viewModel.Book.Publisher.Id == 0 && !string.IsNullOrEmpty(viewModel.NewPublisher))
                {
                    var newPublisher = new Publisher { Name = viewModel.NewPublisher };
                    _context.Publishers.Add(newPublisher);
                    _context.SaveChanges();
                    viewModel.Book.Publisher.Id = newPublisher.Id;
                }

                var book = new Book
                {
                    Title = viewModel.Book.Title,
                    AuthorId = viewModel.Book.Author.Id,
                    GenreId = viewModel.Book.Genre.Id,
                    SubGenreId = viewModel.Book.SubGenre.Id,
                    Height = viewModel.Book.Height,
                    Price = viewModel.Book.Price,
                    PublisherId = viewModel.Book.Publisher.Id,
                    Quantity = viewModel.Book.Quantity,
                    PublicationDate = viewModel.Book.PublicationDate,
                    CoverImage = viewModel.Book.CoverImage
                };

                _context.Books.Add(book);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(viewModel);
        }*/

        public IActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(AddAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingAuthor = await _context.Authors
                    .Where(a => a.Name == model.Name)
                    .FirstOrDefaultAsync();

                if (existingAuthor != null)
                {
                    ModelState.AddModelError("Name", "An author with this name already exists.");
                    return View(model);
                }

                var author = new Author
                {
                    Name = model.Name,
                    Birth = model.Birth,
                    Bio = model.Bio
                };

                if (model.Image != null)
                {
                    // Get a unique filename for the file
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;

                    // Get the path to the images folder
                    var imagesFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");

                    // Ensure the directory exists
                    Directory.CreateDirectory(imagesFolder);

                    // Get the full path for the new image
                    var filePath = Path.Combine(imagesFolder, uniqueFileName);

                    // Use a FileStream to copy the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }

                    // Set the Image property on the author to the relative path of the image
                    author.Image = "/images/" + uniqueFileName;
                }

                _context.Authors.Add(author);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGenre(AddGenreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingGenre = _context.Genres
                    .FirstOrDefault(g => g.Name == model.Name);

                if (existingGenre != null)
                {
                    ModelState.AddModelError("Name", "A genre with this name already exists.");
                    return View(model);
                }

                var genre = new Genre 
                { 
                    Name = model.Name 
                };

                _context.Genres.Add(new Genre 
                { 
                    Name = model.Name 
                });
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public IActionResult AddSubGenre()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSubGenre(AddSubGenreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingSubGenre = _context.SubGenres
                    .FirstOrDefault(sg => sg.Name == model.Name);

                if (existingSubGenre != null)
                {
                    ModelState.AddModelError("Name", "A subgenre with this name already exists.");
                    return View(model);
                }

                var subGenre = new SubGenre
                {
                    Name = model.Name
                };

                _context.SubGenres.Add(new SubGenre 
                { 
                    Name = model.Name 
                });
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public IActionResult AddPublisher()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPublisher(AddPublisherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingPublisher = _context.Publishers.FirstOrDefault(p => p.Name == model.Name);

                if (existingPublisher != null)
                {
                    ModelState.AddModelError("Name", "A publisher with this name already exists.");
                    return View(model);
                }

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

                    publisher.Image = "/publisher/" + uniqueFileName;
                }

                _context.Publishers.Add(publisher);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public IActionResult AddNewBook()
        {
            var viewModel = new AddNewBookViewModel
            {
                Authors = _context.Authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).ToList(),
                Publishers = _context.Publishers.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList(),
                Genres = _context.Genres.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name }).ToList(),
                SubGenres = _context.SubGenres.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddNewBook(AddNewBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Title = model.Title,
                    Height = model.Height,
                    Price = model.Price,
                    Quantity = model.Quantity,
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

                return RedirectToAction("Index", "Home");
            }
            else
            {
                model.Authors = _context.Authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).ToList();
                model.Publishers = _context.Publishers.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList();
                model.Genres = _context.Genres.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name }).ToList();
                model.SubGenres = _context.SubGenres.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList();

                return View(model);
            }
        }
    }
}
