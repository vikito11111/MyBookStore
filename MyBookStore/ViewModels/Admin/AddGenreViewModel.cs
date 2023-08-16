using System.ComponentModel.DataAnnotations;

namespace MyBookStore.ViewModels.Admin
{
    public class AddGenreViewModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
    }
}
