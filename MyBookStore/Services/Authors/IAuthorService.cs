using MyBookStore.ViewModels.Admin;

namespace MyBookStore.Services.Authors
{
    public interface IAuthorService
    {
        bool AddAuthor(AddAuthorViewModel model, out string errorMessage);
    }
}
