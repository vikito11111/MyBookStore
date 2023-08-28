using MyBookStore.Models;

namespace MyBookStore.ViewModels.Home
{
	public class IndexViewModel
	{
		public IEnumerable<Book> NewestBooks { get; set; }

		public IEnumerable<Book> RecommendedBooks { get; set; }

        public IEnumerable<Book> BooksBySameAuthor { get; set; }

        public List<Book> BestSellingBooks { get; set; }
	}
}
