using System;

namespace SocialApp.MVC.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
        public string Message { get; set; }
        public string Lastname { get; set; }
        public DateTime Date { get; set; }
    }
}
