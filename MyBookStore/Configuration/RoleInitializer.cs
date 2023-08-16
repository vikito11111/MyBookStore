using Microsoft.AspNetCore.Identity;

namespace MyBookStore.Configuration
{
    public static class RoleInitializer
    {
        public static async Task EnsureRolesExist(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = new[] { "Member", "Super-Member", "Admin" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
