using MyBookStore.Models;
using MyBookStore.ViewModels.Admin;

namespace MyBookStore.Services.Publishers
{
    public interface IPublisherService
    {
        Task<(bool isSuccess, string errorMessage)> AddPublisherAsync(AddPublisherViewModel model);

        Task<Publisher> GetPublisherById(int id);
    }
}
