using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForum
{
    /// <summary>
    /// Forum profile repository
    /// </summary>
    public class ForumProfileManager 
    {
        public ForumContext Database { get; set; }
        public ForumProfileManager(ForumContext db)
        {
            Database = db;
        }

        public void Create(ForumUser item)
        {
            Database.ForumUsers.Add(item);
            Database.SaveChanges();
        }
        public async Task Delete(ForumUser item)
        {
            Database.ForumUsers.Remove(item);
            await Database.SaveChangesAsync();
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
