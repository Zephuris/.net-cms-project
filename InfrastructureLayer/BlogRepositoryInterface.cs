﻿using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer
{
    public interface IBlogRepository
    {
        Task<CustomActionResult> CreateBlog(PostModel model);
        Task<CustomActionResult<List<PostModel>>> GetBlogs();
        Task<CustomActionResult<PostModel>> GetBlogById(int id);
        Task<CustomActionResult<PostModel>> UpdateBlog(PostModel model);
        Task<bool> DeleteBlog(int id);
    }
}