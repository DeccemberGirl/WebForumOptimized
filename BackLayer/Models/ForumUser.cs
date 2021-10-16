using Microsoft.AspNet.Identity.EntityFramework;

namespace DataLayer.Models
{
    public class ForumUser : IdentityUser
    {
        public virtual ForumProfile ForumProfile { get; set; }
    }
}
