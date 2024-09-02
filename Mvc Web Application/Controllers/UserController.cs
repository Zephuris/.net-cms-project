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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
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
                    ///_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    Response.Headers["Authorization"] = $"Bearer {jwtToken}";
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(request);




        }
    }
}
