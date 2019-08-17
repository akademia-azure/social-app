using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialApp.MVC.Models
{
    public partial class UserInfo
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string PersonalDescription { get; set; }
        public string Interests { get; set; }
        public string Hobbys { get; set; }

        [NotMapped]
        public IFormFile Avatar { get; set; }

        public UserInfo()
        {
            BirthDate = DateTime.Now;
        }

        public UserInfo(string userId) : base()
        {
            UserId = userId;
        }
    }
}
