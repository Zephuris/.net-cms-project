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
        public async Task<bool> DeleteBlog(int id)
        {
            return await _respository.DeleteBlog(id);
        }

        public async Task<CustomActionResult<PostModel>> UpdateBlog(PostModel model)
        {
            return await _respository.UpdateBlog(model);
        }

        public async Task<CustomActionResult<PostModel>> GetBlogById(int id)
        {
            return await _respository.GetBlogById(id);
        }

        public async Task<CustomActionResult<List<PostModel>>> GetBlogs()
        {
            return await _respository.GetBlogs();
        }
    }
}
