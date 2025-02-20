﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.ViewModels.Books;
using MyBookStore.ViewModels.Cart;

namespace MyBookStore.Services.Carts
{
    public class CartService : ICartService
    {
        private readonly MyBookStoreDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartService(MyBookStoreDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddToCartAsync(string userId, int bookId)
        {
            var userOwnsBook = _context.ApplicationUserLibraries
                .Any(ul => ul.ApplicationUserId == userId && ul.BookId == bookId);

            if (userOwnsBook)
            {
                throw new InvalidOperationException("You already own this book.");
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var book = _context.Books.Find(bookId);
            if (book == null)
            {
                throw new InvalidOperationException("Book not found.");
            }

            var cart = _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.ApplicationUserId == user.Id);

            if (cart == null)
            {
                cart = new Cart { ApplicationUserId = user.Id };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == bookId);
            if (cartItem == null)
            {
                cartItem = new CartItem { BookId = bookId, CartId = cart.Id };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task CheckoutAsync(string userId)
        {
            const decimal SUPER_MEMBER_DISCOUNT = 0.15m;

            const decimal SUPER_MEMBER_THRESHOLD = 200;

            decimal discount = 0;

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Book)
                .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

            var user = await _userManager.FindByIdAsync(userId);

            if (await _userManager.IsInRoleAsync(user, "Super-Member"))
            {
                discount = cart.TotalPrice * SUPER_MEMBER_DISCOUNT;
            }

            decimal finalPrice = cart.TotalPrice - discount;

            if (user.Balance < finalPrice)
            {
                throw new InvalidOperationException("Insufficient balance. Please top up your account.");
            }

            user.Balance -= finalPrice;

            foreach (var cartItem in cart.CartItems)
            {
                var userLibraryItem = new ApplicationUserLibrary
                {
                    ApplicationUserId = userId,
                    BookId = cartItem.BookId
                };

                _context.ApplicationUserLibraries.Add(userLibraryItem);
            }

            user.SpentMoney += finalPrice;

            if (await _userManager.IsInRoleAsync(user, "Super-Member"))
            {
                user.SuperMemberPurchasesCount += 1;

                if (user.SuperMemberPurchasesCount >= 5)
                {
                    await _userManager.RemoveFromRoleAsync(user, "Super-Member");

                    await _userManager.AddToRoleAsync(user, "Member");

                    user.SuperMemberPurchasesCount = 0;

                    user.SpentMoney = 0;
                }
            }
            else if (user.SpentMoney >= SUPER_MEMBER_THRESHOLD && !await _userManager.IsInRoleAsync(user, "Super-Member"))
            {
                await _userManager.AddToRoleAsync(user, "Super-Member");

                user.SuperMemberPurchasesCount = 0;

                await _userManager.RemoveFromRoleAsync(user, "Member");
            }

            _context.CartItems.RemoveRange(cart.CartItems);

            await _context.SaveChangesAsync();
        }

        public async Task<CartViewModel> GetCartViewModelAsync(string userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Book)
                .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

            if (cart == null)
            {
                return new CartViewModel();
            }

            var cartViewModel = new CartViewModel
            {
                Id = cart.Id,
                TotalPrice = cart.TotalPrice,
                CartItems = cart.CartItems.Select(ci => new CartItemViewModel
                {
                    Id = ci.Id,
                    Book = new BookViewModel
                    {
                        Id = ci.Book.Id,
                        Title = ci.Book.Title,
                        Price = ci.Book.Price,
                        CoverImage = ci.Book.CoverImage,
                        AuthorName = ci.Book.Author?.Name,
                        GenreName = ci.Book.Genre?.Name,
                        SubGenreName = ci.Book.SubGenre?.Name,
                        PublisherName = ci.Book.Publisher?.Name,
                        PublicationDate = ci.Book.PublicationDate
                    }
                }).ToList()
            };

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Super-Member"))
                {
                    cartViewModel.IsSuperMember = true;

                    cartViewModel.DiscountedTotalPrice = cartViewModel.TotalPrice * 0.85m;
                }
            }

            return cartViewModel;
        }

        public async Task RemoveFromCartAsync(int itemId)
        {
            var cartItem = await _context.CartItems.FindAsync(itemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
