using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    /// <summary>
    /// Forum profile entity wich contains user info
    /// </summary>
    public class ForumProfile
    {
        /// <summary>
        /// Forum profile Id which is the foreign key from the ForumUser entity
        /// </summary>
        [Key]
        [ForeignKey("ForumUser")]
        public string Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Forum user which corresponds to the profile
        /// </summary>
        public virtual ForumUser ForumUser { get; set; }
    }
}
