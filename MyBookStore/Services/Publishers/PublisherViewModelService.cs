using MyBookStore.Models;
using MyBookStore.ViewModels.Books;
using MyBookStore.ViewModels.Publisher;

namespace MyBookStore.Services.Publishers
{
    public class PublisherViewModelService : IPublisherViewModelService
    {
        public PublisherDetailsViewModel GetPublisherDetailsViewModel(Publisher publisher)
        {
            return new PublisherDetailsViewModel
            {
                Name = publisher.Name,
                Image = publisher.Image,
                Established = publisher.Established,
                Bio = publisher.Bio,
                Books = publisher.Books.Select(b => new BookViewModel
                {
                    Title = b.Title,
                    CoverImage = b.CoverImage
                }).ToList()
            };
        }
    }
}
