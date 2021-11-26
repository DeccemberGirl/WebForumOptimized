using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    /// <summary>
    /// Model for the New Message wich then added to the Topic
    /// </summary>
    public class NewMessageFormModel
    {
        /// <summary>
        /// Id of the topic
        /// </summary>
        [Required]
        public int TopicId { get; set; }

        /// <summary>
        /// Text of the topic message
        /// </summary>
        [Required(ErrorMessage = "Message should be filled")]
        public string Text { get; set; }
    }
}
