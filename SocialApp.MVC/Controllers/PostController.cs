using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialApp.MVC.Data;
using SocialApp.MVC.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialApp.MVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;

        public PostController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        [HttpGet("newsfeed/post/{id}")]
        public async Task<ActionResult> Index(int id)
        {
            var user = await userManager.GetUserAsync(this.User);

            var post = dbContext.Post
                .Where(p => p.Id == id)
                .FirstOrDefault();

            var comments = dbContext.Comment
                .Where(c => c.PostId == id)
                .ToList();

            var currentUserLikePost = dbContext.Like
                .Where(c => c.PostId == id && c.UserId == user.Id)
                .Any();

            return View(new PostViewModel
            {
                Post = post,
                Comments = comments,
                CurrentUserLikePost = currentUserLikePost
            });
        }

        [HttpPost("Post/Comment")]
        public async Task<ActionResult> Comment([FromForm] Comment comment)
        {
            var user = await userManager.GetUserAsync(this.User);

            dbContext.Comment
                .Add(new Comment
                {
                    Date = DateTime.UtcNow,
                    PostId = comment.PostId,
                    Message = comment.Message,
                    UserId = user.Id,
                    Lastname = user.UserName
                });

            dbContext.SaveChanges();

            return RedirectToAction("Index", new { id = comment.PostId });
        }

        [HttpGet("Post/Unlike/{id}")]
        public async Task<ActionResult> Unlike(int id)
        {
            var user = await userManager.GetUserAsync(this.User);

            var post = dbContext.Post
                .Where(p => p.Id == id)
                .FirstOrDefault();

            post.LikeCounter--;

            dbContext.Post.Update(post);

            var like = dbContext.Like
                .Where(c => c.PostId == id && c.UserId == user.Id)
                .FirstOrDefault();

            dbContext.Remove(like);

            dbContext.SaveChanges();

            return RedirectToAction("Index", new { id = id });
        }

        [HttpGet("Post/Like/{id}")]
        public async Task<ActionResult> Like(int id)
        {
            var user = await userManager.GetUserAsync(this.User);

            var post = dbContext.Post
             .Where(p => p.Id == id)
             .FirstOrDefault();

            post.LikeCounter++;

            dbContext.Post.Update(post);

            dbContext.Like
                .Add(new Like
                {
                    Date = DateTime.UtcNow,
                    PostId = id,
                    UserId = user.Id,
                });

            dbContext.SaveChanges();

            return RedirectToAction("Index", new { id = id });
        }
    }
}
