using DLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Web_APIS.Models;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(IUserServices userServices, IHttpContextAccessor httpContextAccessor)
        {
            _userServices = userServices;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userServices.GetUsers();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpPost("InsertUser")]
        public async Task<IActionResult> InsertUser([FromBody] tbl_users user)
        {
            bool isInserted = await _userServices.InsertUserAsync(user);

            if (!isInserted)
            {
                return BadRequest("User not inserted");
            }

            return Ok("User inserted successfully");

        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(string username,string password)
        {
            var response = await _userServices.Login(username, password);

            if (response == null)
            {
                return BadRequest("Invalid UserName Or Password");
            }

            return Ok(new { message = "User Logged In Successfully", Data = response });
        }

        [HttpGet("GetConnetionByLabId")]
        public async Task<IActionResult> GetConnetionByLabId(Guid LabId)
        {
            var users = await _userServices.GetConnetionByLabId(LabId);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("get-login-info")]
        public IActionResult GetLoginInfo()
        {
            var session = _httpContextAccessor.HttpContext.Session;  
            byte[] responseBytes = Web_APIS.Models.SessionExtensions.Get(session, "LoginResponse"); 

            if (responseBytes != null)
            {
                var jsonResponse = Encoding.UTF8.GetString(responseBytes);
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);

                return Ok(loginResponse);
            }
            else
            {
                return NotFound("No login data in session.");
            }
        }

    }
}

