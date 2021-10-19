using System.ComponentModel.DataAnnotations;

namespace BackLayer.Models
{
    public class MessageControlModel
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
