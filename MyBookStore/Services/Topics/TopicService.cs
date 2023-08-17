using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Data;
using MyBookStore.Models;

namespace MyBookStore.Services.Topics
{
    public class TopicService : ITopicService
    {
        private readonly MyBookStoreDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TopicService(MyBookStoreDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IEnumerable<Topic> GetTopicsByForum(int forumId)
        {
            return _context.Topics
                .Where(t => t.ForumId == forumId)
                .ToList();
        }

        public Topic GetTopicById(int id)
        {
            return _context.Topics
                .Include(t => t.Creator)
                .SingleOrDefault(t => t.Id == id);
        }

        public void AddTopic(Topic topic, string creatorId)
        {
            topic.CreatorId = creatorId;
            _context.Topics.Add(topic);
            _context.SaveChanges();
        }

        public void EditTopic(Topic topic)
        {
            _context.Topics.Update(topic);
            _context.SaveChanges();
        }

        public void DeleteTopic(int topicId)
        {
            var topic = GetTopicById(topicId);
            _context.Topics.Remove(topic);
            _context.SaveChanges();
        }

        public async Task<bool> CanUserEditTopic(string userId, int topicId)
        {
            var topic = GetTopicById(topicId);

            if (topic == null)
            {
                return false;
            }

            if (topic.CreatorId == userId)
            {
                return true;
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return _userManager.IsInRoleAsync(user, "Admin").Result;
            }

            return false;
        }
    }
}
