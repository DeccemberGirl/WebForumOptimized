using Microsoft.AspNet.Identity.EntityFramework;

namespace WebForum
{
    public class ForumUser : IdentityUser
    {
        public virtual ForumProfile ForumProfile { get; set; }
    }
}
