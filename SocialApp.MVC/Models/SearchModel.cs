namespace SocialApp.MVC.Models
{
    public class SearchModel
    {
        public string Phrase { get; set; }
        public string UserName { get; set; }
        public int LikeMore { get; set; }
        public int LikeLess { get; set; }

        public SearchModel()
        {
            Phrase = string.Empty;
        }
    }
}
