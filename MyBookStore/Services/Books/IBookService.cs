using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.ViewModels.Admin;
using MyBookStore.ViewModels.Search;

namespace MyBookStore.Services.Books
{
    public interface IBookService
    {
        bool AddBook(AddNewBookViewModel model, out string errorMessage);

        List<Book> GetNewestBooks();

        Book GetBookById(int id);

        List<Book> SearchBooks(SearchViewModel searchModel);
    }
}
