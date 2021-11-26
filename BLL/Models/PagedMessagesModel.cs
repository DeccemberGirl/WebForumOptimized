using BLL.DTO;
using System.Collections.Generic;

namespace BLL.Models
{
    /// <summary>
    /// Model which contains all messages on the page
    /// </summary>
    public class PagedMessagesModel
    {
        /// <summary>
        /// Returns the collection of message dto-s
        /// </summary>
        public IEnumerable<MessageDTO> Messages { get; set; }

        /// <summary>
        /// Returns the total number of the pages on the forum
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Returns the current page number
        /// </summary>
        public int CurrentPage { get; set; }
    }
}
