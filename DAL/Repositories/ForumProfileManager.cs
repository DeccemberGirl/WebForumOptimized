using DAL.Entities;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// Forum profile repository
    /// </summary>
    public class ForumProfileManager 
    {
        /// <summary>
        /// Forum context
        /// </summary>
        public ForumContext Database { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="ForumProfileManager" />
        /// </summary>
        /// <param name="db">ForumContext object</param>
        public ForumProfileManager(ForumContext db)
        {
            Database = db;
        }

        /// <summary>
        /// Creates forum profile object
        /// </summary>
        /// <param name="item">ForumProfile entity</param>
        public void Create(ForumProfile item)
        {
            Database.ForumProfiles.Add(item);
            Database.SaveChanges();
        }

        /// <summary>
        /// Deletes forum profile object
        /// </summary>
        /// <param name="item">ForumProfile</param>
        public async Task Delete(ForumProfile item)
        {
            Database.ForumProfiles.Remove(item);
            await Database.SaveChangesAsync();
        }

        /// <summary>
        /// DB desposing
        /// </summary>
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
