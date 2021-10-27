using BLL.DTO;
using System.Collections.Generic;

namespace BLL.Models
{
    /// <summary>
    ///Model which contain all messages on the page
    /// </summary>
    public class PagedMessagesModel
    {
        public IEnumerable<MessageDTO> Messages { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
