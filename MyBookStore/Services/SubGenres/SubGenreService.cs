using Humanizer.Localisation;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.ViewModels.Admin;

namespace MyBookStore.Services.SubGenres
{
    public class SubGenreService : ISubGenreService
    {
        private readonly MyBookStoreDbContext _context;

        public SubGenreService(MyBookStoreDbContext context)
        {
            _context = context;
        }

        public bool AddSubGenre(AddSubGenreViewModel model, out string errorMessage)
        {
            var existingSubGenre =  _context.SubGenres.FirstOrDefault(x => x.Name == model.Name);

            if (existingSubGenre != null)
            {
                errorMessage = "A subgenre with the same name already exists.";
                return false;
            }

            try
            {
                var subGenre = new SubGenre 
                { 
                    Name = model.Name
                };

                _context.SubGenres.Add(subGenre);
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
