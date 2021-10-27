using BLL.DTO;
using System.Collections.Generic;

namespace BLL.Models
{
    /// <summary>
    ///Model which contain all topics on the page
    /// </summary>
    public class PagedTopicModel
    {
        public IEnumerable<TopicDTO> Topics { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
