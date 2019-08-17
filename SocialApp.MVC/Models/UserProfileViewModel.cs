using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialApp.MVC.Models
{
    public class UserProfileViewModel
    {
        // Identity User
        public string Id { get; set; }
        public string Email { get; set; }
        public string NormalizedUserName { get; set; }
        public string UserName { get; set; }

        // User Info
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string PersonalDescription { get; set; }
        public string Interests { get; set; }
        public string Hobbys { get; set; }


        public string Fullname => $"{Firstname} {Lastname}";
        public string CurrentUserId { get; set; }

        public UserProfileViewModel()
        {

        }

        public UserProfileViewModel(string id, string email, string normalizedUserName, string userName, UserInfo userInfo) : this(userInfo)
        {
            Id = id;
            Email = email;
            NormalizedUserName = normalizedUserName;
            UserName = userName;
        }

        public UserProfileViewModel(UserInfo userInfo)
        {
            Firstname = userInfo.Firstname;
            Lastname = userInfo.Lastname;
            Position = userInfo.Position;
            AvatarUrl = userInfo.AvatarUrl;
            BirthDate = userInfo.BirthDate;
            City = userInfo.City;
            PersonalDescription = userInfo.PersonalDescription;
            Interests = userInfo.Interests;
            Hobbys = userInfo.Hobbys;
        }
    }
}
