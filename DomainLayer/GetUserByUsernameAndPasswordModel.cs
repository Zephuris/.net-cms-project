using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class GetUserByUsernameAndPasswordModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
