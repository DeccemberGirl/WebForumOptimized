using System.ComponentModel.DataAnnotations;

namespace WebForum
{
    /// <summary>
    /// Model for the New Message wich then added to the Topic
    /// </summary>
    public class NewMessageFormModel
    {
        [Required]
        public int TopicId { get; set; }
        [Required(ErrorMessage ="Message should be filled")]
        public string Text { get; set; }
    }
}
