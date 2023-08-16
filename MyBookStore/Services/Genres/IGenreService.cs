using MyBookStore.ViewModels.Admin;

namespace MyBookStore.Services.Genres
{
    public interface IGenreService
    {
        bool AddGenre(AddGenreViewModel model, out string errorMessage);
    }
}
