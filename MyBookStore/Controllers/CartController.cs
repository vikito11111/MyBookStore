using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.Services.Carts;
using MyBookStore.ViewModels.Books;
using MyBookStore.ViewModels.Cart;

namespace MyBookStore.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly MyBookStoreDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(MyBookStoreDbContext context, UserManager<ApplicationUser> userManager, ICartService cartService) : base(userManager, context)
        {
            _context = context;
            _userManager = userManager;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var cartViewModel = await _cartService.GetCartViewModelAsync(userId);

            return View(cartViewModel);
        }

        public async Task<IActionResult> AddToCart(int bookId)
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            try
            {
                await _cartService.AddToCartAsync(userId, bookId);
                return RedirectToAction("Index", "Home");
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Cart", new { id = bookId });
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int itemId)
        {
            await _cartService.RemoveFromCartAsync(itemId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var userId = _userManager.GetUserId(User);

            try
            {
                await _cartService.CheckoutAsync(userId);

                TempData["SuccessMessage"] = "Purchase successful! Books have been added to your library.";

                return RedirectToAction("Index", "Home");
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                return RedirectToAction("Index");
            }
        }
    }
}
