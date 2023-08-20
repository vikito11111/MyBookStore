using MyBookStore.ViewModels.Cart;

namespace MyBookStore.Services.Carts
{
    public interface ICartService
    {
        Task AddToCartAsync(string userId, int bookId);

        Task RemoveFromCartAsync(int itemId);

        Task<CartViewModel> GetCartViewModelAsync(string userId);

        Task CheckoutAsync(string userId);
    }
}
