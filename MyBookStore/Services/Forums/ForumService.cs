using MyBookStore.Data;
using MyBookStore.Models;

namespace MyBookStore.Services.Forums
{
    public class ForumService : IForumService
    {
        private readonly MyBookStoreDbContext _context;

        public ForumService(MyBookStoreDbContext context)
        {
            _context = context;
        }

        public void AddForum(Forum forum)
        {
            _context.Forums.Add(forum);
            _context.SaveChanges();
        }

        public void DeleteForum(int id)
        {
            var forum = GetById(id);
            _context.Forums.Remove(forum);
            _context.SaveChanges();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _context.Forums.ToList();
        }

        public Forum GetById(int id)
        {
            return _context.Forums.SingleOrDefault(f => f.Id == id);
        }

        public void UpdateForum(Forum forum)
        {
            _context.Forums.Update(forum);
            _context.SaveChanges();
        }
    }
}
