using System.ComponentModel.DataAnnotations.Schema;

namespace WebForum
{
    /// <summary>
    /// Message entity
    /// </summary>
    public class Message 
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual ForumUser ForumUser { get; set; }
    }
}
