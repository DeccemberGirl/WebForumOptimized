
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackLayer.Models
{
    public class PagedMessagesModel
    {
        public IEnumerable<MessageControl> Messages { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
