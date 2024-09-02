 using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Mvc_Web_Application.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;

namespace Mvc_Web_Application.Controllers
{
    public class BlogController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7181/api");
        private readonly HttpClient _client;
        public BlogController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult GetBlogs()


        {
            try
            {

                CustomActionResult<List<PostViewModel>> posts = new CustomActionResult<List<PostViewModel>>();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Blog/GetBlogs").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    posts = JsonConvert.DeserializeObject<CustomActionResult<List<PostViewModel>>>(data);
                    return View(posts);

                }

            }
            catch
            {
                throw new NotImplementedException();
            }
            return View();
        }


        public async Task<IActionResult> UpdateBlog(PostViewModel post)
        {
            using (var client = new HttpClient())
            {
                var postTask = _client.PostAsJsonAsync<PostViewModel>(_client.BaseAddress + "/Blog/UpdateBlog", post);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetBlogs");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(post);
        }

        public async Task<IActionResult> CreateBlog(PostViewModel post)
        {
            using (var client = new HttpClient())
            {
                var postTask = _client.PostAsJsonAsync<PostViewModel>(_client.BaseAddress + "/Blog/CreateBlog", post);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetBlogs");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(post);
        }

        [Route("blog/DeleteBLog/{id:int}")]
        public async Task<IActionResult> DeleteBLog(int id)
        {
            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("id", id.ToString())
                });
                var response = _client.PostAsync(_client.BaseAddress + $"/Blog/DeletBlog?id={id}", content);



                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetBlogs");
                }

            }
            catch (HttpRequestException ex)
            {
                return View("Error", new ErrorViewModel
                {
                    ErrorMessage = "There was an issue connecting to the server. Please try again later."
                });
            }

            return View(id);

        }

        [Route("blog/getblogs/{id:int}")]
        public async Task<IActionResult>  GetById(int id)
        {
            try
            {
                var content = new FormUrlEncodedContent(new[]
{
                new KeyValuePair<string, string>("id", id.ToString())
                });
                CustomActionResult<PostViewModel> post = new CustomActionResult<PostViewModel>();
                HttpResponseMessage response = (await _client.PostAsJsonAsync<CustomActionResult<PostViewModel>>(_client.BaseAddress + $"/Blog/GetBlogById?id={id}", post));

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    post = JsonConvert.DeserializeObject<CustomActionResult<PostViewModel>>(data);
                    return View(post);

                }

            }
            catch
            {
                throw new NotImplementedException();
            }
            return View();
        }
    }
}

