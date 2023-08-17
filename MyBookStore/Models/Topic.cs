namespace MyBookStore.Models
{
    public class Topic
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        public DateTime Created { get; set; }

        public int ForumId { get; set; }

        public Forum Forum { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
