using Microsoft.AspNetCore.Mvc;
using MyBookStore.Data;
using MyBookStore.Services.Authors;
using MyBookStore.ViewModels.Author;
using MyBookStore.ViewModels.Books;

namespace MyBookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IAuthorViewModelService _viewModelService;

        public AuthorController(IAuthorService authorService, IAuthorViewModelService authorViewModelService)
        {
            _authorService = authorService;
            _viewModelService = authorViewModelService;
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _authorService.GetAuthorById(id);

            if (author == null)
            {
                return NotFound();
            }

            var viewModel = _viewModelService.GetAuthorDetailsViewModel(author);

            return View(viewModel);
        }
    }
}
