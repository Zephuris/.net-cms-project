using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class CommentModel : EntityBlogModel
    {
        public int PostId { get; set; }
        public string content { get; set; }
        public string author { get; set; }

    }
}
