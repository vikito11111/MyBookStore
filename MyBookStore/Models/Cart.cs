namespace MyBookStore.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public decimal TotalPrice => CartItems.Sum(item => item.Book.Price ?? 0);

        public virtual List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
