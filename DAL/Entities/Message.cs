namespace DAL.Entities
{
    /// <summary>
    /// Message entity
    /// </summary>
    public class Message : BaseEntity
    {
        /// <summary>
        /// The date of the message creating
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Text of the message
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Topic which the message corresponds to
        /// </summary>
        public virtual Topic Topic { get; set; }

        /// <summary>
        /// The author of the message
        /// </summary>
        public virtual ForumUser ForumUser { get; set; }
    }
}
