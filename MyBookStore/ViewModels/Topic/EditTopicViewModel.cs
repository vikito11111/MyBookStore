namespace MyBookStore.ViewModels.Topic
{
    public class EditTopicViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? CreatorId { get; set; }

        public DateTime? Created { get; set; }

        public int ForumId { get; set; }
    }
}
