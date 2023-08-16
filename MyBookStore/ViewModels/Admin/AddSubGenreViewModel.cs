using System.ComponentModel.DataAnnotations;

namespace MyBookStore.ViewModels.Admin
{
    public class AddSubGenreViewModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
    }
}
