 using Microsoft.AspNetCore.Mvc;
using Mvc_Web_Application.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

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
        public IActionResult Index()
        
        
        {
            try
            {

            List < CustomActionResult < PostViewModel >> posts = new List<CustomActionResult<PostViewModel>>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Blog/GetBlogs").Result;

            if (response.IsSuccessStatusCode) 
            { 
                string data = response.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<List<CustomActionResult<PostViewModel>>>(data);
                return View(posts);

                }

            }
            catch{
                throw new NotImplementedException();
            }
            return View();
        }
    }
}
