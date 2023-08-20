namespace MyBookStore.ViewModels.Cart
{
    public class CartViewModel
    {
        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public List<CartItemViewModel> CartItems { get; set; }
    }
}
