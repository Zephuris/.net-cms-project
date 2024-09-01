using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    public interface IBlogService
    {
        Task<CustomActionResult> CreateBlog(PostModel model);
        Task<CustomActionResult<List<PostModel>>> GetBlogs();
        Task<PostModel> GetBlogById(int id);
        Task<PostModel> UpdateBlog(PostModel model);
        Task<bool> DeleteBlog(int ids);

    }
}
