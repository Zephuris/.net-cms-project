using DomainLayer;
using InfrastructureLayer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtConfigModel _jwtConfigModel;
        public UserService(IUserRepository userRepository, IOptions<JwtConfigModel> jwtConfigModel)
        {
            _userRepository = userRepository;
            _jwtConfigModel = jwtConfigModel.Value;
        }

        public async Task<CustomActionResult<string>> LoginUserByUsernameAndPassword(LogonRequest request)
        {
            CustomActionResult<string> result = new CustomActionResult<string>();
            CustomActionResult<GetUserByUsernameAndPasswordModel> checkUserResult = await _userRepository.GetUserByUsernameAndPassword(request);
            result.IsSuccess = checkUserResult.IsSuccess;
            result.Message = checkUserResult.Message;
            if (!result.IsSuccess) return result;




            SymmetricSecurityKey secrectKey = new(Encoding.UTF8.GetBytes(_jwtConfigModel.Key));

            SigningCredentials signingCredentials = new(secrectKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenOptions = new(
                claims:
                [
                     new("UserId", checkUserResult.Data.UserId.ToString()),
                     new("RoleId", checkUserResult.Data.RoleId.ToString()),
                ],
                expires: DateTime.Now.AddMinutes(_jwtConfigModel.ExpireMinute),
                signingCredentials: signingCredentials
            );
            result.IsSuccess = true;
            result.Data = new JwtSecurityTokenHandler().WriteToken(tokenOptions);


            return result;
        }
    }
}
