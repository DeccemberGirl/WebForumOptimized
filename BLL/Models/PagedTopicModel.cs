using BLL.DTO;
using System.Collections.Generic;

namespace BLL.Models
{
    /// <summary>
    /// Model which contains all topics on the page
    /// </summary>
    public class PagedTopicModel
    {
        /// <summary>
        /// Returns the collection of all topics dto-s on the page
        /// </summary>
        public IEnumerable<TopicDTO> Topics { get; set; }

        /// <summary>
        /// Returns the total number of pages 
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Returns the number of the current page
        /// </summary>
        public int CurrentPage { get; set; }
    }
}
