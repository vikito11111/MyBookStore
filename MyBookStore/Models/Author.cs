using MyBookStore.Common;
using System.ComponentModel.DataAnnotations;

namespace MyBookStore.Models
{
	public class Author
	{
		public Author()
		{
			Books = new HashSet<Book>();
		}

		[Key]
		public int Id { get; set; }

		[Required]
		[MinLength(AuthorGlobalConstant.AuthorNameMinLength)]
		public string Name { get; set; }

		[Required]
		public DateTime Birth { get; set; }

		[Required]
		public string Bio { get; set; }

        public string? Image { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public override string ToString()
		{
			return $"{Name}";
		}
	}
}
