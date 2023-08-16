using MyBookStore.Common;
using System.ComponentModel.DataAnnotations;

namespace MyBookStore.Models
{
	public class SubGenre
	{
		public SubGenre()
		{
			Books = new HashSet<Book>();
		}

		[Key]
		public int Id { get; set; }

		[Required]
		[MinLength(SubGenreGlobalConstant.SubGenreNameMinLength)]
		public string Name { get; set; }

		public ICollection<Book> Books { get; set; }
	}
}
