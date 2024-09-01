using Dapper;
using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer
{
    public class BlogRepository : IBlogRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public BlogRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;

        }

        public async Task<CustomActionResult> CreateBlog(PostModel model)
        {
            CustomActionResult _result = new CustomActionResult();
            try
            {
                CustomActionResult<System.Data.IDbConnection> connection = await _databaseConnection.GetConnection();
                _result.IsSuccess = connection.IsSuccess;
                _result.Message = connection.Message;
                if (!_result.IsSuccess) return _result;

                var command = "prc_create_blog";


                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(name: "@Id", value: model.id);
                parameters.Add(name: "@title", value: model.Title);
                parameters.Add(name: "@body", value: model.Body);


                await connection.Data.ExecuteAsync(command, parameters, commandType: System.Data.CommandType.StoredProcedure);
                _result.IsSuccess = true;
                _result.Message = "Contact Created Successfully.";

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();

            }
            return _result;

        }

        public async Task<bool> DeleteBlog(int id)
        {
            bool _result;

            try
            {
                CustomActionResult<System.Data.IDbConnection> connection = await _databaseConnection.GetConnection();
                _result = connection.IsSuccess;
                if (!_result) return _result;
                var command = "prc_delete_blog";


                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(name: "@Id", value: id);



                await connection.Data.ExecuteAsync(command, parameters, commandType: System.Data.CommandType.StoredProcedure);
                _result = true;


            }
            catch (Exception ex)
            {
                throw new NotImplementedException();

            }
            return _result;
        }
        public async Task<CustomActionResult<PostModel>> GetBlogById(int id)
        {
            CustomActionResult<PostModel> _result = new CustomActionResult<PostModel>();   
            try
            {
                CustomActionResult<System.Data.IDbConnection> connection = await _databaseConnection.GetConnection();
                _result.IsSuccess = connection.IsSuccess;
                _result.Message = connection.Message;
                if (!_result.IsSuccess) return _result;

                var command = "prc_blog_byId";
                   
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(name: "@Id", value: id);


                _result.Data = (await connection.Data.QueryAsync<PostModel>(command, parameters, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                _result.IsSuccess = true;
            }
            catch
            {
                throw new NotImplementedException();

            }
            return _result;
        }
        public async Task<CustomActionResult<PostModel>> UpdateBlog(PostModel model)
        {
            CustomActionResult<PostModel> _result = new CustomActionResult<PostModel>();
            try
            {
                CustomActionResult<System.Data.IDbConnection> connection = await _databaseConnection.GetConnection();
                _result.IsSuccess = connection.IsSuccess;
                _result.Message = connection.Message;
                if (!_result.IsSuccess) return _result;

                var command = "prc_update_blog";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(name: "@Id", value: model.id);
                parameters.Add(name: "@title", value: model.Title);
                parameters.Add(name: "@body", value: model.Body);


                _result.Data = (await connection.Data.QueryAsync<PostModel>(command, parameters, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                _result.IsSuccess = true;
            }
            catch { throw new NotImplementedException(); }
            return _result;
        }

        public async Task<CustomActionResult<List<PostModel>>> GetBlogs()
        {
            CustomActionResult<List<PostModel>> _result = new CustomActionResult<List<PostModel>>();
            try
            {
                CustomActionResult<System.Data.IDbConnection> connection = await _databaseConnection.GetConnection();
                _result.IsSuccess = connection.IsSuccess;
                _result.Message = connection.Message;
                if (!_result.IsSuccess) return _result;

                var command = "prc_get_blogs";




                _result.Data = (await connection.Data.QueryAsync<PostModel>(command, null, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                _result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
            return _result;

        }
    }
}