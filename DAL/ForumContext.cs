using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DAL
{
    /// <summary>
    /// Forum Context for Entity Framework
    /// </summary>
    public class ForumContext : IdentityDbContext<ForumUser>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ForumContext" />
        /// </summary>
        public ForumContext():base()
        {
   
        }

        /// <summary>
        /// Collection of messages
        /// </summary>
        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// Collection of topics
        /// </summary>
        public DbSet<Topic> Topics { get; set; }

        /// <summary>
        /// Collection of forum profiles
        /// </summary>
        public DbSet<ForumProfile> ForumProfiles { get; set; }

        /// <summary>
        /// Configures creating EF models
        /// </summary>
        /// <param name="modelBuilder">DbModelBuilder object</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}
