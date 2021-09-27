using System.ComponentModel.DataAnnotations;

namespace WebForum
{
    /// <summary>
    /// DTO for message Entity
    /// </summary>
    public class MessageControl
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserForumId { get; set; }
        [Required]
        public int TopicId { get; set; }
        [Required]
        public string Date { get; set; }
        [Required(ErrorMessage = "Message should be filled")]
        public string Text { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
