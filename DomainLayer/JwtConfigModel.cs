using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class JwtConfigModel
    {
        public string Key { get; set; }
        public int ExpireMinute { get; set; }
    }
}
