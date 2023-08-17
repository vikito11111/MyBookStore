using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;

namespace MyBookStore.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly MyBookStoreDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentService(MyBookStoreDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IEnumerable<Comment> GetCommentsByTopic(int topicId)
        {
            return _context.Comments
                .Where(c => c.TopicId == topicId)
                .Include(c => c.User)
                .ToList();
        }

        public void AddComment(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void EditComment(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            _context.Comments.Update(comment);
            _context.SaveChanges();
        }

        public void DeleteComment(int commentId)
        {
            var comment = _context.Comments.Find(commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
        }

        public bool CanUserEditComment(string userId, int commentId)
        {
            var comment = _context.Comments.Find(commentId);

            if (comment == null)
            {
                return false;
            }

            if (comment.UserId == userId)
            {
                return true;
            }

            var user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                return _userManager.IsInRoleAsync(user, "Admin").Result;
            }

            return false;
        }
    }
}
