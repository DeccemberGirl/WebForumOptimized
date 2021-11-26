using BLL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    /// <summary>
    /// TopicDTO for Topic entity which transfers the topic information from DAL to BLL
    /// </summary>
    public class TopicDTO
    {
        /// <summary>
        /// Id of the topic
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Id of the topic author
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Name of the topic
        /// </summary>
        [Required(ErrorMessage = "Name of topic should be filled")]
        public string Name { get; set; }

        /// <summary>
        /// Date when the topic was created
        /// </summary>
        [Required]
        public string Date { get; set; }

        /// <summary>
        /// Name of the topic author
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Text of the topic description
        /// </summary>
        [Required(ErrorMessage = "Topic description should be filled")]
        public string Text { get; set; }

        /// <summary>
        /// Holds model of NewMessage wich should be added to topic
        /// </summary>
        public NewMessageFormModel NewMessage { get; set; }

        /// <summary>
        /// Returns models of all the messages of the topic
        /// </summary>
        public PagedMessagesModel Messages { get; set; }

        /// <summary>
        /// Returns number of the messages in topic
        /// </summary>
        public int MessageCount { get; set; }
    }
}
