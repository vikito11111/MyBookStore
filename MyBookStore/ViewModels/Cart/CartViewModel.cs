namespace MyBookStore.ViewModels.Cart
{
    public class CartViewModel
    {
        public int Id { get; set; }

        public bool IsSuperMember { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DiscountedTotalPrice { get; set; }

        public List<CartItemViewModel> CartItems { get; set; }
    }
}
