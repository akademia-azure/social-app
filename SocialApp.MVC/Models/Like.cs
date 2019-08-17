using System;
using System.Collections.Generic;

namespace SocialApp.MVC.Models
{
    public partial class Like
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
        public DateTime Date { get; set; }

        public virtual Post Post { get; set; }
    }
}
