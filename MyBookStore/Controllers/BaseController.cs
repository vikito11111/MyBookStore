using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;

namespace MyBookStore.Controllers
{
    public class BaseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MyBookStoreDbContext _context;

        public BaseController(UserManager<ApplicationUser> userManager, MyBookStoreDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = _userManager.GetUserId(User);
            var cartItemCount = _context.CartItems.Count(c => c.Cart.ApplicationUserId == userId);
            ViewBag.CartItemCount = cartItemCount;

            base.OnActionExecuting(context);
        }
    }
}
