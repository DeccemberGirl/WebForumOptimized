using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    /// <summary>
    /// DTO for message Entity which transfers the message information from DAL to BLL
    /// </summary>
    public class MessageDTO
    {
        /// <summary>
        /// Id of the message
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Id of the message author
        /// </summary>
        [Required]
        public string UserForumId { get; set; }

        /// <summary>
        /// Id of the topic corresponding the message
        /// </summary>
        [Required]
        public int TopicId { get; set; }

        /// <summary>
        /// The date when the message was created
        /// </summary>
        [Required]
        public string Date { get; set; }

        /// <summary>
        /// The text of the message
        /// </summary>
        [Required(ErrorMessage = "Message should be filled")]
        public string Text { get; set; }

        /// <summary>
        /// Name of the message author
        /// </summary>
        [Required]
        public string UserName { get; set; }
    }
}
