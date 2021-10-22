using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Entities
{
    /// <summary>
    /// The entity of the registered forum user
    /// </summary>
    public class ForumUser : IdentityUser
    {
        /// <summary>
        /// The forum profile of the user
        /// </summary>
        public virtual ForumProfile ForumProfile { get; set; }
    }
}
