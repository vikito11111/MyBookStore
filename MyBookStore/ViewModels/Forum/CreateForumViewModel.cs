using System.ComponentModel.DataAnnotations;

namespace MyBookStore.ViewModels.Forum
{
    public class CreateForumViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string? CreatorId { get; set; }
    }
}
