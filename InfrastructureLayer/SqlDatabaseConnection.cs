using DomainLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace InfrastructureLayer
{
    public class SqlDatabaseConnection : IDatabaseConnection
    {

        private IDbConnection _connection;
        private readonly SqlDatabaseConnectionModel _connectionModel;
        public SqlDatabaseConnection(IOptions<SqlDatabaseConnectionModel> options)
        {
            _connectionModel = options.Value;
        }


        public async Task<CustomActionResult<IDbConnection>> GetConnection()
        {
            CustomActionResult<IDbConnection> _result = new CustomActionResult<IDbConnection>();
            try
            {

                if (_connection == null)
                {
                    _connection = new SqlConnection(_connectionModel.ConnectionString);
                }
                _result.IsSuccess = true;
                _result.Data = _connection;

            }
            catch (Exception)
            {
                _result.IsSuccess = false;
                _result.Message = "Error Connecting Database";

            }
            return _result;


        }
    }
}