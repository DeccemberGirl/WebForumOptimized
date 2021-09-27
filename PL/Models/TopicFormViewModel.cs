using System.ComponentModel.DataAnnotations;

namespace WebForum
{
    /// <summary>
    /// Topic form wich used for adding a new Topic
    /// </summary>
    public class TopicFormViewModel
    {
        [Required(ErrorMessage = "Message should be filled")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Name of topic should be filled")]
        public string Name { get; set; }
    }
}