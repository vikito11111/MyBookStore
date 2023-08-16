using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace MyBookStore.ViewModels.Admin
{
    public class AddAuthorViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Birth { get; set; }

        [Required]
        [MinLength(5)]
        public string Bio { get; set; }

        public IFormFile Image { get; set; }
    }
}
