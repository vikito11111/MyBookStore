using MyBookStore.Models;
using MyBookStore.ViewModels.Publisher;

namespace MyBookStore.Services.Publishers
{
    public interface IPublisherViewModelService
    {
        PublisherDetailsViewModel GetPublisherDetailsViewModel(Publisher publisher);
    }
}
