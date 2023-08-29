using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Models;

namespace MyBookStore.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<PersonalDataModel> _logger;

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public decimal SpentMoney { get; set; }
        public string ApplicationRole { get; set; }

        public int NumberOfBooks { get; set; }

        public PersonalDataModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<PersonalDataModel> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.Users
                .Include(u => u.ApplicationUserLibrary)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            FullName = $"{user.FirstName} {user.LastName}";
            PhoneNumber = user.PhoneNumber;
            ProfilePictureUrl = user.ProfilePictureUrl;
            Email = user.Email;
            Balance = user.Balance;
            NumberOfBooks = user.ApplicationUserLibrary.Count();
            ApplicationRole = roles.FirstOrDefault();

            return Page();
        }
    }
}
