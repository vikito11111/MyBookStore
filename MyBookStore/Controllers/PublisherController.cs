using Microsoft.AspNetCore.Mvc;
using MyBookStore.Models;
using MyBookStore.Services.Publishers;

namespace MyBookStore.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherService _publisherService;
        private readonly IPublisherViewModelService _publisherViewModelService;

        public PublisherController(IPublisherService publisherService, IPublisherViewModelService publisherViewModelService)
        {
            _publisherService = publisherService;
            _publisherViewModelService = publisherViewModelService;
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _publisherService.GetPublisherById(id);

            if (publisher == null)
            {
                return NotFound();
            }

            var viewModel = _publisherViewModelService.GetPublisherDetailsViewModel(publisher);

            return View(viewModel);
        }
    }
}
