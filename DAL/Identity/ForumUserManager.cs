using DAL.Entities;
using Microsoft.AspNet.Identity;

namespace WebForum
{
    /// <summary>
    /// Forum user manager entity
    /// </summary>
    public class ForumUserManager:UserManager<ForumUser>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ForumUserManager" />
        /// </summary>
        /// <param name="store"></param>
        public ForumUserManager(IUserStore<ForumUser> store)
                : base(store)
        {
        }
    }
}
