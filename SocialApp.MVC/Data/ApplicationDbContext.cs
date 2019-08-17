using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialApp.MVC.Models;

namespace SocialApp.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Like> Like { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
