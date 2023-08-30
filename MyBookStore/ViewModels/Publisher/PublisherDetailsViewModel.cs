using MyBookStore.ViewModels.Books;

namespace MyBookStore.ViewModels.Publisher
{
    public class PublisherDetailsViewModel
    {
        public string Name { get; set; }

        public string Bio { get; set; }

        public DateTime Established { get; set; }

        public string Image { get; set; }

        public List<BookViewModel> Books { get; set; }
    }
}
