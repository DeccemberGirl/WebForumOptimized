
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForum
{
    public class PagedTopicModel
    {
        public IEnumerable<TopicControl> Topics { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
