using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    /// <summary>
    /// Topic form which is used for adding a new Topic
    /// </summary>
    public class TopicFormViewModel
    {
        /// <summary>
        /// Text of the topic
        /// </summary>
        [Required(ErrorMessage = "Message should be filled")]
        public string Text { get; set; }

        /// <summary>
        /// Name of the topic
        /// </summary>
        [Required(ErrorMessage = "Name of topic should be filled")]
        public string Name { get; set; }
    }
}