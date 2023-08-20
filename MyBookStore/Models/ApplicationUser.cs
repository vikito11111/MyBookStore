using Microsoft.AspNetCore.Identity;

namespace MyBookStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePictureUrl { get; set; }

        public decimal Balance { get; set; }

        public virtual ICollection<ApplicationUserLibrary> ApplicationUserLibrary { get; set; } = new List<ApplicationUserLibrary>();

        public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();
    }
}

