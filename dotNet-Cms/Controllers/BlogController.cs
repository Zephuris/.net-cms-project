using DomainLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationLayer;
using Microsoft.AspNetCore.Http.HttpResults;
namespace dotNet_Cms
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BlogController : ControllerBase
    {

        private readonly IBlogService _logservice;
        public BlogController(IBlogService blogservice)
        {
            _logservice = blogservice;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(PostModel model)
        {

            CustomActionResult result = await _logservice.CreateBlog(model);

            return Ok(result);
        }

        [HttpPost]
        public  async Task<IActionResult> DeletBlog(int id)
        {
            bool result = await _logservice.DeleteBlog(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {

            CustomActionResult<List<PostModel>> result = await _logservice.GetBlogs();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetBlogById(int id)
        {
            CustomActionResult<PostModel> result = await _logservice.GetBlogById(id);
            return Ok(result);
        }
    }
}

