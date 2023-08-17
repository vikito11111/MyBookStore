namespace MyBookStore.ViewModels.Comments
{
    public class CommentViewModel
    {
        public string Content { get; set; }

        public DateTime Created { get; set; }

        public string UserName { get; set; }

        public string UserProfilePictureUrl { get; set; }
    }
}
