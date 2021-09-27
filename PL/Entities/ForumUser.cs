using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebForum
{
    public class ForumUser : IdentityUser
    {
        [Key]
        [ForeignKey("ForumUser")]

        public string Name { get; set; }
        public string Address { get; set; }
    }
}
