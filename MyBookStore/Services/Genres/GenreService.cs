using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.ViewModels.Admin;

namespace MyBookStore.Services.Genres
{
    public class GenreService : IGenreService
    {
        private readonly MyBookStoreDbContext _context;

        public GenreService(MyBookStoreDbContext context)
        {
            _context = context;
        }

        public bool AddGenre(AddGenreViewModel model, out string errorMessage)
        {
            var existingGenre = _context.Genres.FirstOrDefault(p => p.Name == model.Name);

            if (existingGenre != null)
            {
                errorMessage = "A genre with the same name already exists.";
                return false;
            }

            try
            {
                var genre = new Genre
                {
                    Name = model.Name
                };

                _context.Genres.Add(genre);
                _context.SaveChanges();

                errorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;

                return false;
            }
        }
    }
}
