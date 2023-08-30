using MyBookStore.Models;
using MyBookStore.ViewModels.Author;

namespace MyBookStore.Services.Authors
{
    public interface IAuthorViewModelService
    {
        AuthorDetailsViewModel GetAuthorDetailsViewModel(Author author);
    }
}
