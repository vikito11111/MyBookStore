using MyBookStore.Common;
using System.ComponentModel.DataAnnotations;

namespace MyBookStore.Models
{
	public class Genre
	{
		public Genre()
		{
			Books = new HashSet<Book>();
		}

		[Key]
		public int Id { get; set; }

		[Required]
		[MinLength(GenreGlobalConstant.GenreNameMinLength)]
		public string Name { get; set; }

		public virtual ICollection<Book> Books { get; set; }

		public override string ToString()
		{
			return $"{Name}";
		}
	}
}
