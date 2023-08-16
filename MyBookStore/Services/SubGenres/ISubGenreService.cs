using MyBookStore.ViewModels.Admin;

namespace MyBookStore.Services.SubGenres
{
    public interface ISubGenreService
    {
        bool AddSubGenre(AddSubGenreViewModel model, out string errorMessage);
    }
}
