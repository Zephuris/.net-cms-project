using DomainLayer;
using InfrastructureLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _respository;

        public BlogService(IBlogRepository respository)
        {
            _respository = respository;
        }

        public async Task<CustomActionResult> CreateBlog(PostModel model)
        {
            return await _respository.CreateBlog(model);

        }
        public Task<bool> DeleteBlog(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PostModel> UpdateBlog(PostModel model)
        {
            throw new NotImplementedException();
        }

        public Task<PostModel> GetBlogById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomActionResult<List<PostModel>>> GetBlogs()
        {
            return await _respository.GetBlogs();
        }
    }
}
