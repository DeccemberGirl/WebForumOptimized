
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebForum
{
    public class TopicControl
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Name of topic should be filled")]
        public string Name { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Topic message should be filled")]
        public string Text { get; set; }
        /// <summary>
        /// Holds model of NewMessage wich should be added to topic
        /// </summary>
        public NewMessageFormModel NewMessage { get; set; }
        public PagedMessagesModel Messages { get; set; }
        public int MessageCount { get; set; }
    }
}
