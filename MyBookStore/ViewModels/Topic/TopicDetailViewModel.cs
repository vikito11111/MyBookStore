using MyBookStore.Models;
using MyBookStore.ViewModels.Comments;

namespace MyBookStore.ViewModels.Topic
{
    public class TopicDetailViewModel
    {
        public MyBookStore.Models.Topic Topic { get; set; }

        public IList<CommentViewModel> Comments { get; set; }
        //public IEnumerable<Comment> Comments { get; set; }

        public string NewComment { get; set; }
    }
}
