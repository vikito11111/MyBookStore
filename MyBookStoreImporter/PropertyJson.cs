namespace BookStoreWeb.Importer
{
    public class JsonProperty
    {
        public string Title { get; set; }

        public AuthorJson Author { get; set; }

        public string Genre { get; set; }

        public string SubGenre { get; set; }

        public int Height { get; set; }

        public decimal Price { get; set; }

        public PublisherJson Publisher { get; set; }

        public string CoverImage { get; set; }

        public int Quantity { get; set; }

        public string PublicationDate { get; set; }

        public bool IsBestSeller { get; set; }
    }
}