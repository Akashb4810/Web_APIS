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
            try
            {
                var users = await _userServices.GetUsers();

                if (users == null || !users.Any())
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "No users found",
                        Data = (object)null
                    });
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Users retrieved successfully",
                    Data = users
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred while retrieving users",
                    Error = ex.Message
                });
            }
        }


        [HttpPost("InsertUser")]
        public async Task<IActionResult> InsertUser([FromBody] tbl_users user)
        {
            try
            {
                bool isInserted = await _userServices.InsertUserAsync(user);

                if (!isInserted)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = "User insertion failed",
                        Data = (object)null
                    });
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "User inserted successfully",
                    Data = user // or return the newly created user with ID if available
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while inserting the user",
                    Error = ex.Message,
                    Data = (object)null
                });
            }
        }


        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(string username, string password)
        {
            try
            {
                var response = await _userServices.Login(username, password);

                if (response == null)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = "Invalid Username or Password",
                        Data = (object)null
                    });
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "User Logged In Successfully",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                // Optionally, log the exception here
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred.",
                    Error = ex.Message
                });
            }
        }


        [HttpGet("GetConnetionByLabId")]
        public async Task<IActionResult> GetConnetionByLabId(Guid LabId)
        {
            try
            {
                var users = await _userServices.GetConnetionByLabId(LabId);

                if (users == null || !users.Any())
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = $"No users found for Lab ID: {LabId}",
                        Data = (object)null
                    });
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Users retrieved successfully",
                    Data = users
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while fetching users by Lab ID",
                    Error = ex.Message,
                    Data = (object)null
                });
            }
        }


        [HttpGet("get-login-info")]
        public IActionResult GetLoginInfo()
        {
            try
            {
                var session = _httpContextAccessor.HttpContext.Session;
                byte[] responseBytes = Web_APIS.Models.SessionExtensions.Get(session, "LoginResponse");

                if (responseBytes != null)
                {
                    var jsonResponse = Encoding.UTF8.GetString(responseBytes);
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);

                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Login data retrieved from session",
                        Data = loginResponse
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "No login data found in session",
                        Data = (object)null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving login info",
                    Error = ex.Message,
                    Data = (object)null
                });
            }
        }


    }
}

