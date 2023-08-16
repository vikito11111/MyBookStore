using MyBookStore.Data;

namespace MyBookStoreDatabaseCreate
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var db = new MyBookStoreDbContext();

            ResetDatabase(db);
        }

        private static void ResetDatabase(MyBookStoreDbContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database was successfully deleted!");

            db.Database.EnsureCreated();
            Console.WriteLine("Database was successfully created!");
        }
    }
}