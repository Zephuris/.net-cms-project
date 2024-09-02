using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Blog
{
    public class PostModel : EntityBlogModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
    }
}
