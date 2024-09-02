using Dapper;
using DomainLayer.entities;
using DomainLayer.Users;
using InfrastructureLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.User
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public UserRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<CustomActionResult<GetUserByUsernameAndPasswordModel>> GetUserByUsernameAndPassword(LogonRequest request)
        {
            CustomActionResult<GetUserByUsernameAndPasswordModel> _result = new CustomActionResult<GetUserByUsernameAndPasswordModel>();
            try
            {
                CustomActionResult<System.Data.IDbConnection> connection = await _databaseConnection.GetConnection();
                _result.IsSuccess = connection.IsSuccess;
                _result.Message = connection.Message;
                if (!_result.IsSuccess) return _result;

                string command = "prc_login_by_user_pas";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(name: "@Username", value: request.Username);
                parameters.Add(name: "@psw", value: request.psw);


                _result.Data = (await connection.Data.QueryAsync<GetUserByUsernameAndPasswordModel>(command, parameters, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();

                if (_result.Data.UserId != 0)
                {
                    _result.IsSuccess = true;
                }
                else
                {

                    _result.IsSuccess = false;
                    _result.Message = "Invalid Username Or Password";
                }

            }
            catch
            {
                _result.IsSuccess = false;
                _result.Message = "Error Geting Userinfo.";
            }
            return _result;
        }
    }
}
