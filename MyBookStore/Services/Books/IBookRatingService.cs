namespace MyBookStore.Services.Books
{
    public interface IBookRatingService
    {
        decimal RateBook(int bookId, int rating, string userId);
    }
}
