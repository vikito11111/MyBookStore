using MyBookStore.Models;

namespace MyBookStore.Services.Topics
{
    public interface ITopicService
    {
        IEnumerable<Topic> GetTopicsByForum(int forumId);

        Topic GetTopicById(int id);

        void AddTopic(Topic topic, string creatorId);

        void EditTopic(Topic topic);

        void DeleteTopic(int topicId);

        Task<bool> CanUserEditTopic(string userId, int topicId);
    }
}
