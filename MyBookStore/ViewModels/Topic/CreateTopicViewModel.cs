using MyBookStore.Models;

namespace MyBookStore.ViewModels.Topic
{
    public class CreateTopicViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string? CreatorId { get; set; }

        public int? ForumId { get; set; }

        public DateTime? Created { get; set; }
    }
}
