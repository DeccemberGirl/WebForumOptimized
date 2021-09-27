using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebForum
{
    public class ForumContext : IdentityDbContext<ForumUser>
    {
        public ForumContext():base()
        {
            
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<ForumProfile> ForumProfiles { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            

        }
    }
}
