using MyBookStore.Common;
using System.ComponentModel.DataAnnotations;

namespace MyBookStore.Models
{
	public class Publisher
	{
		public Publisher()
		{
			Books = new HashSet<Book>();
		}

		[Key]
		public int Id { get; set; }

		[Required]
		[MinLength(PublisherGlobalConstant.PublisherNameMinLength)]
		public string Name { get; set; }

		[Required]
		public string Bio { get; set; }

		[Required]
		public DateTime Established { get; set; }

        public string? Image { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public override string ToString()
		{
			return $"{Name}";
		}
	}
}
