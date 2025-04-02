using DLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userServices.GetUsers();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
    }
}
