using DataLayer.Models;
using Microsoft.AspNet.Identity;

namespace DataLayer.Sourses.Managers
{
    public class ForumUserManager:UserManager<ForumUser>
    {
        public ForumUserManager(IUserStore<ForumUser> store)
                : base(store)
        {
        }
    }
}
