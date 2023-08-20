using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.Services.Comments;
using MyBookStore.Services.Topics;
using MyBookStore.ViewModels.Comments;
using MyBookStore.ViewModels.Topic;

namespace MyBookStore.Controllers
{
    [Authorize]
    public class TopicController : BaseController
    {
        private readonly ITopicService _topicService;
        private readonly ICommentService _commentService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MyBookStoreDbContext _context;

        public TopicController(ITopicService topicService, ICommentService commentService, UserManager<ApplicationUser> userManager, MyBookStoreDbContext context) : base(userManager, context)
        {
            _topicService = topicService;
            _commentService = commentService;
            _userManager = userManager;
        }

        public IActionResult Index(int id)
        {
            var topic = _topicService.GetTopicById(id);
            return View(topic);
        }

        public IActionResult Create(int forumId)
        {
            var model = new CreateTopicViewModel { ForumId = forumId, Created = DateTime.Now };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateTopicViewModel model)
        {
            if (ModelState.IsValid)
            {
                var creatorId = _userManager.GetUserId(User);

                var topic = new Topic
                {
                    Title = model.Title,
                    Description = model.Description,
                    CreatorId = creatorId,
                    Created = model.Created ?? DateTime.Now,
                    ForumId = model.ForumId ?? 0
                };

                _topicService.AddTopic(topic, creatorId);
                return RedirectToAction("Index", "Forum");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userId = _userManager.GetUserId(User);
            if (!await _topicService.CanUserEditTopic(userId, id))
            {
                return Forbid();
            }

            var topic = _topicService.GetTopicById(id);

            var viewModel = new EditTopicViewModel
            {
                Id = topic.Id,
                Title = topic.Title,
                Description = topic.Description,
                CreatorId = topic.CreatorId,
                Created = topic.Created,
                ForumId = topic.ForumId
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTopicViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (!await _topicService.CanUserEditTopic(userId, id))
                {
                    return Forbid();
                }

                var topic = _topicService.GetTopicById(id);
                topic.Title = model.Title;
                topic.Description = model.Description;

                _topicService.EditTopic(topic);
                return RedirectToAction("Index", "Forum");
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var topic = _topicService.GetTopicById(id);
            var userId = _userManager.GetUserId(User);

            if (!await _topicService.CanUserEditTopic(userId, id))
            {
                return Forbid();
            }

            return View(topic);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);

            if (!await _topicService.CanUserEditTopic(userId, id))
            {
                return Forbid();
            }

            _topicService.DeleteTopic(id);
            return RedirectToAction("Index", "Forum");
        }

        public IActionResult Comments(int id)
        {
            var comments = _commentService.GetCommentsByTopic(id);
            return View(comments);
        }

        public IActionResult Details(int id)
        {
            var topic = _topicService.GetTopicById(id);
            var comments = _commentService.GetCommentsByTopic(id)
                                  .Select(c => new CommentViewModel
                                  {
                                      Content = c.Content,
                                      Created = c.Created,
                                      UserName = c.User.UserName,
                                      UserProfilePictureUrl = c.User.ProfilePictureUrl
                                  })
                                  .ToList();

            var viewModel = new TopicDetailViewModel
            {
                Topic = topic,
                Comments = comments
            };

            return View(viewModel);
        }
    }
}
