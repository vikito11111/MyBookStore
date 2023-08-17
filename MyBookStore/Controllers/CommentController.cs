using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBookStore.Models;
using MyBookStore.Services.Comments;

namespace MyBookStore.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(ICommentService commentService, UserManager<ApplicationUser> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult AddComment(int topicId, string content)
        {
            var userId = _userManager.GetUserId(User);

            var comment = new Comment
            {
                Content = content,
                Created = DateTime.Now,
                UserId = userId,
                TopicId = topicId
            };

            _commentService.AddComment(comment);

            return RedirectToAction("Details", "Topic", new { id = topicId });
        }
    }
}
