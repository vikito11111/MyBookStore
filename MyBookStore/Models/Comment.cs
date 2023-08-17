namespace MyBookStore.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int TopicId { get; set; }

        public Topic Topic { get; set; }
    }
}
