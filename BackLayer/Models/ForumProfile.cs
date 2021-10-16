using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class ForumProfile
    {
        [Key]
        [ForeignKey("ForumUser")]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ForumUser ForumUser { get; set; }
    }
}
