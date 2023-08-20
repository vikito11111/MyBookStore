using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using MyBookStore.Common;
using MyBookStore.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace MyBookStore.ViewModels.Admin
{
    public class AddNewBookViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }

        [Required]
        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }

        [Required]
        [ForeignKey(nameof(SubGenre))]
        public int SubGenreId { get; set; }

        [Required]
        [ForeignKey(nameof(Publisher))]
        public int PublisherId { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        [Range(BookGlobalConstant.BookPriceMinLength, BookGlobalConstant.BookPriceMaxLength)]
        public decimal? Price { get; set; }

        public IFormFile CoverImage { get; set; }

        [Required]
        public DateTime PublicationDate { get; set; }

        public List<SelectListItem>? Authors { get; set; }

        public List<SelectListItem>? Publishers { get; set; }

        public List<SelectListItem>? Genres { get; set; }

        public List<SelectListItem>? SubGenres { get; set; }
    }
}
