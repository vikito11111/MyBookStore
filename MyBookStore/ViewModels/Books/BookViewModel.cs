namespace MyBookStore.ViewModels.Books
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal? Price { get; set; }

        public string CoverImage { get; set; }

        public string AuthorName { get; set; }

        public string GenreName { get; set; }

        public string SubGenreName { get; set; }

        public string PublisherName { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}
