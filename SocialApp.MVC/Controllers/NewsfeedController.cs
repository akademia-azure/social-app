using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialApp.MVC.Models;
using System.Diagnostics;

namespace SocialApp.MVC.Controllers
{
    public class NewsfeedController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
