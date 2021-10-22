using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    /// <summary>
    /// Topic entity
    /// </summary>
    public class Topic : BaseEntity
    {
        /// <summary>
        /// Topic Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Topic name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The date of the topic creating
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Description of the topic
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The topic creator
        /// </summary>
        public virtual ForumUser ForumUser { get; set; }

        /// <summary>
        /// The collection of messages which corresponds to the topic
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; }
    }
}
