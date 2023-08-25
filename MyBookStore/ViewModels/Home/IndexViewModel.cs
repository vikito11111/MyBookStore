using MyBookStore.Models;

namespace MyBookStore.ViewModels.Home
{
	public class IndexViewModel
	{
		public IEnumerable<Book> NewestBooks { get; set; }

		public List<Book> BestSellingBooks { get; set; }
	}
}
