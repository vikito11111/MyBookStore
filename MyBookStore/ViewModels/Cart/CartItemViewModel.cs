using MyBookStore.ViewModels.Books;

namespace MyBookStore.ViewModels.Cart
{
    public class CartItemViewModel
    {
        public int Id { get; set; }

        public BookViewModel Book { get; set; }
    }
}
