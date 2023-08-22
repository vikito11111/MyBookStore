using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.Services.Forums;
using MyBookStore.Services.Topics;
using MyBookStore.ViewModels.Forum;

namespace MyBookStore.Controllers
{
    [Authorize]
    public class ForumController : BaseController
    {
        private readonly IForumService _forumService;
        private readonly ITopicService _topicService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MyBookStoreDbContext _context;

        public ForumController(IForumService forumService, ITopicService topicService, UserManager<ApplicationUser> userManager, MyBookStoreDbContext context) : base(userManager, context)
        {
            _forumService = forumService;
            _topicService = topicService;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var forums = _forumService.GetAll();
            var user = _userManager.GetUserAsync(User).Result;
            var isAdmin = _userManager.IsInRoleAsync(user, "Admin").Result;

            ViewBag.IsAdmin = isAdmin;
            ViewBag.UserId = user?.Id;

            return View(forums);
        }

        public IActionResult Topics(int id)
        {
            var forum = _forumService.GetById(id);
            var topics = _topicService.GetTopicsByForum(id);
            forum.Topics = topics.ToList();
            return View(forum);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateForumViewModel model)
        {
            if (ModelState.IsValid)
            {
                var forum = new Forum
                {
                    Title = model.Title,
                    Description = model.Description,
                    CreatorId = model.CreatorId,
                    Topics = new List<Topic>()
                };

                _forumService.AddForum(forum);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var forum = _forumService.GetById(id);
            var user = _userManager.GetUserAsync(User).Result;
            var isAdmin = _userManager.IsInRoleAsync(user, "Admin").Result;

            if (user.Id == forum.CreatorId || isAdmin)
            {
                var viewModel = new EditForumViewModel
                {
                    Id = forum.Id,
                    Title = forum.Title,
                    Description = forum.Description
                };
                return View(viewModel);
            }

            return Forbid();
        }

        [HttpPost]
        public IActionResult Edit(EditForumViewModel model)
        {
            if (ModelState.IsValid)
            {
                var forum = _forumService.GetById(model.Id);

                forum.Title = model.Title;

                forum.Description = model.Description;

                _forumService.UpdateForum(forum);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var forum = _forumService.GetById(id);
            var user = _userManager.GetUserAsync(User).Result;
            var isAdmin = _userManager.IsInRoleAsync(user, "Admin").Result;

            if (user.Id == forum.CreatorId || isAdmin)
            {
                return View(forum);
            }

            return Forbid();
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _forumService.DeleteForum(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
