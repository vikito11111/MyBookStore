using MyBookStore.ViewModels.Books;

namespace MyBookStore.ViewModels.Author
{
    public class AuthorDetailsViewModel
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public DateTime Birth { get; set; }

        public string Bio { get; set; }

        public List<BookViewModel> Books { get; set; }
    }
}
