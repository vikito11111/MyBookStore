using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;

namespace MyBookStore.Controllers
{
    public class LibraryController : Controller
    {
        private readonly MyBookStoreDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LibraryController(MyBookStoreDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var userBooks = await _context.ApplicationUserLibraries
                .Include(aul => aul.Book)
                .ThenInclude(b => b.Author)
                .Include(aul => aul.Book)
                .ThenInclude(b => b.Genre)
                .Include(aul => aul.Book)
                .ThenInclude(b => b.SubGenre)
                .Include(aul => aul.Book)
                .ThenInclude(b => b.Publisher)
                .Where(aul => aul.ApplicationUserId == user.Id)
                .Select(aul => aul.Book)
                .ToListAsync();

            return View(userBooks);
        }
    }
}
