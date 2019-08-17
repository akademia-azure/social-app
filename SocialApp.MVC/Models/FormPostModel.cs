using Microsoft.AspNetCore.Http;

namespace SocialApp.MVC.Models
{
    public class FormPostModel
    {
        public string Message { get; set; }
        public IFormFile File { get; set; }
    }
}
