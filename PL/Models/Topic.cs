using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebForum
{
    /// <summary>
    /// Topic Entity
    /// </summary>
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }
        public virtual ForumUser ForumUser { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
