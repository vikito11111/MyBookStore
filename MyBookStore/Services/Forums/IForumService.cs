using MyBookStore.Models;

namespace MyBookStore.Services.Forums
{
    public interface IForumService
    {
        IEnumerable<Forum> GetAll();

        Forum GetById(int id);

        void AddForum(Forum forum);

        void UpdateForum(Forum forum);

        void DeleteForum(int id);
    }
}
