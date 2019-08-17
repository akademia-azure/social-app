using System.Collections.Generic;

namespace SocialApp.MVC.Models
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public bool CurrentUserLikePost { get; set; }
    }
}
