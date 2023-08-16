namespace MyBookStore.Models
{
    public class ApplicationUserLibrary
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
