using MyBookStore.Common;
using System.ComponentModel.DataAnnotations;

namespace MyBookStore.ViewModels.Admin
{
    public class AddPublisherViewModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        public DateTime Established { get; set; }

        [Required]
        [MinLength(10)]
        public string Bio { get; set; }

        public IFormFile Image { get; set; }
    }
}
