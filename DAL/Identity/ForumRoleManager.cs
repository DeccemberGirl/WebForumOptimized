using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Identity
{
    /// <summary>
    /// Forum role manager entity
    /// </summary>
    public class ForumRoleManager : RoleManager<ForumRole>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ForumRoleManager" />
        /// </summary>
        /// <param name="store">RoleStore object</param>
        public ForumRoleManager(RoleStore<ForumRole> store)
                   : base(store)
        { }
    }
}
