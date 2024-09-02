using DomainLayer.entities;
using DomainLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.User
{
    public interface IUserRepository
    {
        Task<CustomActionResult<GetUserByUsernameAndPasswordModel>> GetUserByUsernameAndPassword(LogonRequest request);
    }
}
