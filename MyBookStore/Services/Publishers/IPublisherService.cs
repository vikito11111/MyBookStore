using MyBookStore.ViewModels.Admin;

namespace MyBookStore.Services.Publishers
{
    public interface IPublisherService
    {
        bool AddPublisher(AddPublisherViewModel model, out string errorMessage);
    }
}
