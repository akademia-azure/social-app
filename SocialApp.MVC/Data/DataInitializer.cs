using Microsoft.AspNetCore.Identity;
using SocialApp.MVC.Contracts;
using SocialApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialApp.MVC.Data
{
    public class DataInitializer : IDataInitializer
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private IList<UserFullnameWithId> createdUsers;

        public DataInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.createdUsers = new List<UserFullnameWithId>();
        }

        public void Seed()
        {
            var post = dbContext.Post.FirstOrDefault();

            if (post != null)
                return;

            GenerateUsers();

            var rand = new Random();
            var posts = new List<Post>();

            for (int i = 1; i < 20; i++)
            {
                var userIndex = rand.Next(createdUsers.Count());
                posts.Add(new Post
                {
                    UserId = createdUsers[userIndex].Id,
                    UserFullname = createdUsers[userIndex].Fullname,
                    PreviewImageUrl = DefaultPreviewImageUrl,
                    Date = DateTime.Now.AddDays(rand.Next(-100, 0)),
                    LikeCounter = rand.Next(0, 1000),
                    Message = Message.Substring(rand.Next(0, Message.Length - 400), rand.Next(50, 400))
                });
            }

            dbContext.Post.AddRange(posts);
            dbContext.SaveChanges();
        }

        private void GenerateUsers()
        {
            var emails = new List<string>();
            var rand = new Random();
            for (int i = 0; i < 50; i++)
            {
                var firstName = UserFirstnames[rand.Next(UserFirstnames.Count)];
                var lastName = UserLastnames[rand.Next(UserFirstnames.Count)];
                var email = $"{firstName}.{lastName}@mail.com";

                if (!emails.Contains(email))
                {
                    var newUser = new IdentityUser
                    {
                        Email = email,
                        UserName = email
                    };
                    userManager.CreateAsync(newUser, DefaultPassword).GetAwaiter().GetResult();
                    emails.Add(email);
                    createdUsers.Add(new UserFullnameWithId
                    {
                        Id = newUser.Id,
                        Fullname = $"{firstName} {lastName}"
                    });
                }
            }
        }

        private const string DefaultPreviewImageUrl = "https://miro.medium.com/max/640/1*n9k49MxCNYwXBNOVT-oghg.jpeg";
        private const string DefaultPassword = "P@ssw0rd";

        private IList<string> UserFirstnames = new List<string>()
                {
                    "Marcin", "Kacper", "Pawel", "Kamil", "Krzysiek", "Rafal", "Adam", "Michal",
                    "Przemek", "Lukasz", "Romek", "Zdzisiek", "Wieslaw", "Waldek", "Piotr", "Wojtek",
                    "Artur", "Franciszek", "Stanislaw", "Jon", "Sansa", "Cersei"
                };

        private IList<string> UserLastnames = new List<string>()
                {
                    "Nowak", "Kielbasa", "Swislocki", "Stypulkowski", "Tyborowski", "Hryniewski", "Kot", "Waz", "Lis", "Mysz", "Pies", "Koala", "Kirylowicz", "Letowski", "Golab", "Zyrafa",
                    "Kon", "Slon", "Kuropatwa", "Bocian", "Karzelek", "Olbrzym", "Lannister", "Stark", "Targaryen", "Martel", "Arryn", "Mormont", "Tyrell"
                };

        private string Message = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus porta auctor bibendum. Pellentesque tempor nunc vitae quam maximus, vel iaculis dolor consequat. Pellentesque non tellus ut tortor vestibulum tempor et feugiat ex. Proin hendrerit, lacus et mattis volutpat, nunc augue porttitor elit, vitae ornare felis orci eget odio. Phasellus pretium, libero id semper rutrum, augue ex venenatis orci, non porta enim lorem cursus neque. Phasellus porttitor libero non nulla pretium, sit amet varius metus fermentum. Suspendisse vitae finibus lectus, dictum interdum tortor. Praesent a elit consequat ex consectetur dapibus eget ac tellus. Sed scelerisque dolor vitae tristique dictum. Vivamus non nunc nibh. Phasellus porttitor mollis dolor, ut aliquam dui rhoncus et. Aenean sem mi, lacinia ac purus at, semper ullamcorper elit. Cras in enim non massa pellentesque malesuada eget vehicula massa. Morbi maximus dui lacus. Nullam eu felis mauris. Mauris elementum risus vel quam sagittis, eu vestibulum lectus viverra.Quisque sagittis sapien at lectus facilisis, quis ornare sapien finibus.";
    }

    public class UserFullnameWithId
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
    }
}
