using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialApp.MVC.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserFullname { get; set; }
        public string PreviewImageUrl { get; set; }
        public string Message { get; set; }
        public int LikeCounter { get; set; }
        public DateTime Date { get; set; }


        public virtual ICollection<Like> Like { get; set; }

        [NotMapped]
        public string ShortMessage
        {
            get
            {
                if (string.IsNullOrEmpty(Message))
                    return string.Empty;
                return Message.Length >= 100 ? Message.Substring(0, 100) : Message.Substring(0, Message.Length);
            }
        }

        public Post()
        {
            Like = new HashSet<Like>();
        }
    }
}
