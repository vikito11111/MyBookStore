using MyBookStore.Models;
using MyBookStore.ViewModels.Admin;
using MyBookStore.ViewModels.Author;

namespace MyBookStore.Services.Authors
{
    public interface IAuthorService
    {
        Task<(bool isSuccess, string errorMessage)> AddAuthorAsync(AddAuthorViewModel model);

        Task<Author> GetAuthorById(int id);
    }
}
