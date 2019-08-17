using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SocialApp.MVC.Contracts;
using SocialApp.MVC.Data;
using SocialApp.MVC.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SocialApp.MVC.Controllers
{
    [Authorize]
    public class NewsfeedController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly string storageUri;

        public NewsfeedController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.configuration = configuration;

            this.storageUri = configuration.GetValue<string>("ConnectionStrings:StorageUri");
        }

        public ActionResult Index()
        {
            var newsfeedPosts = dbContext.Post
                .OrderByDescending(p => p.Date)
                .ToList();

            return View(new NewsfeedViewModel
            {
                Posts = newsfeedPosts,
                FormPostModel = new FormPostModel()
            });
        }

        [HttpPost]
        [Route("newsfeed/post")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Post([FromForm] FormPostModel formPost)
        {
            var imageUrl = string.Empty;

            if (formPost.File != null)
            {
                var imageName = $"{Guid.NewGuid().ToString()}{GetFileExtension(formPost.File.FileName)}";
                imageUrl = $"{storageUri}/posts/{imageName}";
            }

            var user = await userManager.GetUserAsync(this.User);

            dbContext.Post.Add(new Post
            {
                UserId = user.Id,
                UserFullname = user.UserName,
                PreviewImageUrl = imageUrl,
                Date = DateTime.UtcNow,
                LikeCounter = 0,
                Message = formPost.Message
            });


            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GetFileExtension(string filename)
            => filename.Substring(filename.LastIndexOf('.'));
    }
}
