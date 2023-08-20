using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.Services.Authors;
using MyBookStore.Services.Books;
using MyBookStore.Services.Genres;
using MyBookStore.Services.Publishers;
using MyBookStore.Services.SubGenres;
using MyBookStore.ViewModels.Admin;
using System.Security.Policy;
using Publisher = MyBookStore.Models.Publisher;

namespace MyBookStore.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IAuthorService _authorService;
        private readonly ISubGenreService _subGenreService;
        private readonly IGenreService _genreService;
        private readonly IPublisherService _publisherService;
        private readonly IBookService _bookService;
        private readonly MyBookStoreDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IAuthorService authorService, ISubGenreService subGenreService, IGenreService genreService, IPublisherService publisherService, IBookService bookService, MyBookStoreDbContext context, IWebHostEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager) : base(userManager, context)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _bookService = bookService;
            _publisherService = publisherService;
            _genreService = genreService;
            _subGenreService = subGenreService;
            _authorService = authorService;
            _userManager = userManager;
        }

        public IActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAuthor(AddAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_authorService.AddAuthor(model, out string errorMessage))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                }
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
                if (_genreService.AddGenre(model, out string errorMessage))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                }
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
                if (_subGenreService.AddSubGenre(model, out string errorMessage))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                }
            }

            return View(model);
        }

        public IActionResult AddPublisher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPublisher(AddPublisherViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_publisherService.AddPublisher(model, out string errorMessage))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                }
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
                if (_bookService.AddBook(model, out string errorMessage))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", errorMessage);
                }
            }

            model.Authors = _context.Authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).ToList();
            model.Publishers = _context.Publishers.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList();
            model.Genres = _context.Genres.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name }).ToList();
            model.SubGenres = _context.SubGenres.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList();

            return View(model);
        }
    }
}
