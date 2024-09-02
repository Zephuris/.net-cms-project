using DomainLayer;
using InfrastructureLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    public interface IUserService
    {
        Task<CustomActionResult<string>> LoginUserByUsernameAndPassword(LogonRequest request);
    }
}
