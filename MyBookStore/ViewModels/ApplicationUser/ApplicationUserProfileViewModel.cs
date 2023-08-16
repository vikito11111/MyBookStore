namespace MyBookStore.ViewModels.ApplicationUser
{
    public class ApplicationUserProfileViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public IFormFile ProfilePicture { get; set; }
    }
}
