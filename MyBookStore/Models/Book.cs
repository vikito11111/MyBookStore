using MyBookStore.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyBookStore.Models
{
	public class Book
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

        [Required]
        [ForeignKey(nameof(Author))]
		public int AuthorId { get; set; }
		public Author Author { get; set; }

        [Required]
        [ForeignKey(nameof(Genre))]
		public int GenreId { get; set; }
		public Genre Genre { get; set; }

        [Required]
        [ForeignKey(nameof(SubGenre))]
		public int SubGenreId { get; set; }
		public SubGenre SubGenre { get; set; }

        [Required]
        [ForeignKey(nameof(Publisher))]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        [Required]
		public int Height { get; set; }

        [Required]
        [Range(BookGlobalConstant.BookPriceMinLength, BookGlobalConstant.BookPriceMaxLength)]
		public decimal? Price { get; set; }

        public string? CoverImage { get; set; }

		[Required]
		public DateTime PublicationDate { get; set; }

		public virtual ICollection<ApplicationUserLibrary> ApplicationUserLibrary { get; set; } = new List<ApplicationUserLibrary>();
	}
}
