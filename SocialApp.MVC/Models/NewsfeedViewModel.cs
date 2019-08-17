using System.Collections.Generic;

namespace SocialApp.MVC.Models
{
    public class NewsfeedViewModel
    {
        public FormPostModel FormPostModel{ get; set; }
        public List<Post> Posts { get; set; }
    }
}
