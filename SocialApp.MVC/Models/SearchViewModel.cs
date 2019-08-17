using System.Collections.Generic;
using System.Linq;

namespace SocialApp.MVC.Models
{
    public class SearchViewModel
    {
        public IEnumerable<Post> Posts { get; set; } = Enumerable.Empty<Post>();
        public long Count { get; set; } = 0;
    }
}
