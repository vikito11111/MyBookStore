using MyBookStore.Models;
using MyBookStore.ViewModels.Author;
using MyBookStore.ViewModels.Books;

namespace MyBookStore.Services.Authors
{
    public class AuthorViewModelService : IAuthorViewModelService
    {
        public AuthorDetailsViewModel GetAuthorDetailsViewModel(Author author)
        {
            return new AuthorDetailsViewModel
            {
                Name = author.Name,
                Image = author.Image,
                Birth = author.Birth,
                Bio = author.Bio,
                Books = author.Books.Select(b => new BookViewModel
                {
                    Title = b.Title,
                    CoverImage = b.CoverImage
                }).ToList()
            };
        }
    }
}
