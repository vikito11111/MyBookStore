using MyBookStore.Data;
using MyBookStore.Models;
using System.Globalization;
using System.Text.Json;

namespace BookStoreWeb.Importer
{
    public class Program
    {
        static void Main(string[] args)
        {
            string json = File.ReadAllText(@"D:\MyBookStore\MyBookStore\MyBookStoreImporter\booksdata.json");

            List<JsonProperty> books = JsonSerializer.Deserialize<List<JsonProperty>>(json);

            using (var context = new MyBookStoreDbContext())
            {
                foreach (var jsonBook in books)
                {
                    var author = context.Authors.SingleOrDefault(a => a.Name == jsonBook.Author.Name)
                    ?? new Author
                    {
                        Name = jsonBook.Author.Name,
                        Birth = DateTime.ParseExact(jsonBook.Author.Birth, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                        Bio = jsonBook.Author.Bio,
                        Image = jsonBook.Author.Image
                    };

                    var genre = context.Genres.SingleOrDefault(g => g.Name == jsonBook.Genre) ?? new Genre { Name = jsonBook.Genre };
                    var subGenre = context.SubGenres.SingleOrDefault(sg => sg.Name == jsonBook.SubGenre) ?? new SubGenre { Name = jsonBook.SubGenre };
                    var publisher = context.Publishers.SingleOrDefault(p => p.Name == jsonBook.Publisher.Name) 
                    ?? new Publisher 
                    { 
                        Name = jsonBook.Publisher.Name,
                        Bio = jsonBook.Publisher.Bio,
                        Established = DateTime.ParseExact(jsonBook.Publisher.Established, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                        Image = jsonBook.Publisher.Image
                    };

                    if (author.Id == 0) context.Authors.Add(author);
                    if (genre.Id == 0) context.Genres.Add(genre);
                    if (subGenre.Id == 0) context.SubGenres.Add(subGenre);
                    if (publisher.Id == 0) context.Publishers.Add(publisher);

                    context.SaveChanges();

                    var book = new Book
                    {
                        Title = jsonBook.Title,
                        AuthorId = author.Id,
                        GenreId = genre.Id,
                        SubGenreId = subGenre.Id,
                        Height = jsonBook.Height,
                        Price = jsonBook.Price,
                        PublisherId = publisher.Id,
                        CoverImage = jsonBook.CoverImage,
                        PublicationDate = DateTime.ParseExact(jsonBook.PublicationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                    };

                    context.Books.Add(book);
                }

                context.SaveChanges();

                Console.WriteLine("Data successfully inputed into the database!");
            }
        }
    }
}