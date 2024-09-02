using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection;

using Mvc_Web_Application.Models;

namespace Mvc_Web_Application.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7181/api/User");
        private readonly HttpClient _client;
        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress ;
        }
       [Route("user/login")]
        public async Task<IActionResult> Login(LogonRequestViewModel request)
        {

            using (var client = new HttpClient())
            {
                var postTask = _client.PostAsJsonAsync<LogonRequestViewModel>(_client.BaseAddress + "/Login", request);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var responseModel = await result.Content.ReadFromJsonAsync<CustomActionResult<string>>();
                    var jwtToken = responseModel.Data;
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    return RedirectToAction("user");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View("index");




        }
    }
}
