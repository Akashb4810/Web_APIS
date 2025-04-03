using DLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Web_APIS.Models;

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
            try
            {
                bool isInserted = await _userServices.InsertUserAsync(user);

                if (!isInserted)
                {
                    return BadRequest("User not inserted");
                }

                return Ok("User inserted successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(string username,string password)
        {
            try
            {
                bool isInserted = await _userServices.Login(username,password);

                if (!isInserted)
                {
                    return BadRequest("Invalid UserName Or Password");
                }

                return Ok("User LoggedIn Successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
    }
}
