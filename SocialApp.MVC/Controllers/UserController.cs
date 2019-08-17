using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SocialApp.MVC.Contracts;
using SocialApp.MVC.Data;
using SocialApp.MVC.Models;

namespace SocialApp.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly string storageUri;

        public UserController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.configuration = configuration;

            this.storageUri = configuration.GetValue<string>("ConnectionStrings:StorageUri");
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("user/profile/{userId}")]
        public ActionResult Profile(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                userId = userManager.GetUserId(HttpContext.User);

            var identityUser = userManager.Users.FirstOrDefault(u => u.Id == userId);
            var userInfo = dbContext.UserInfo.FirstOrDefault(ui => ui.UserId == userId);

            if (userInfo == null)
                userInfo = new UserInfo();

            if (identityUser == null)
                return NotFound();

            var userProfileVM = new UserProfileViewModel(
                identityUser.Id,
                identityUser.Email,
                identityUser.NormalizedUserName,
                identityUser.UserName,
                userInfo);

            userProfileVM.CurrentUserId = userManager.GetUserId(HttpContext.User);

            return View(userProfileVM);
        }

        [HttpGet]
        [Route("user/edit")]
        public ActionResult Edit()
        {
            var userId = userManager.GetUserId(HttpContext.User);

            var info = dbContext.UserInfo.Where(ui => ui.UserId == userId).FirstOrDefault();
            if (info == null)
            {
                var currentUserId = userManager.GetUserId(HttpContext.User);
                info = new UserInfo();
                info.UserId = currentUserId;
                dbContext.UserInfo.Add(info);
                dbContext.SaveChanges();
            }

            return View(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserInfo userInfo)
        {
            dbContext.UserInfo.Update(userInfo);
            dbContext.SaveChanges();
            return Redirect($"/user/edit");
        }

        [HttpPost]
        [Route("user/ChangeAvatar")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> ChangeAvatar([FromForm] UserInfo userInfo)
        {
            userInfo.AvatarUrl = userInfo.AvatarUrl;

            if (userInfo.Avatar != null)
            {
                var imageName = $"{Guid.NewGuid().ToString()}{GetFileExtension(userInfo.Avatar.FileName)}";
                userInfo.AvatarUrl = $"{storageUri}/users/{imageName}";
            }


            dbContext.UserInfo.Update(userInfo);
            dbContext.SaveChanges();
            return Redirect($"/user/edit");
        }

        private string GetFileExtension(string filename)
         => filename.Substring(filename.LastIndexOf('.'));
    }
}