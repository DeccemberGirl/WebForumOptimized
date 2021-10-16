using Microsoft.AspNet.Identity;

namespace WebForum
{
    public class ForumUserManager:UserManager<ForumUser>
    {
        public ForumUserManager(IUserStore<ForumUser> store)
                : base(store)
        {
        }
    }
}
