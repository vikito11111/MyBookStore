using MyBookStore.Models;

namespace MyBookStore.Services.Comments
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetCommentsByTopic(int topicId);

        void AddComment(Comment comment);

        void EditComment(Comment comment);

        void DeleteComment(int commentId);

        bool CanUserEditComment(string userId, int commentId);
    }
}
