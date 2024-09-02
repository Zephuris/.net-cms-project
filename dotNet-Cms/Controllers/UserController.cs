using ApplicationLayer.User;
using DomainLayer.Users;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_Cms.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LogonRequest request)
        {
            return Ok(await _userService.LoginUserByUsernameAndPassword(request));
        }
    }
}
