using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace Web_APIS.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configurationSystem;
        public string _connectionString = null;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
             _configurationSystem = configuration;
            _connectionString = _configurationSystem.GetConnectionString("MyCon");
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<tbl_users>> GetUsersAsync()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    return (await con.QueryAsync<tbl_users>("Sp_GetUsers", commandType: CommandType.StoredProcedure)).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    throw new Exception("Error occurred while fetching users.", ex);
                }
            }
        }

        public async Task<bool> InsertUserAsync(tbl_users userModel)
        {
            userModel.Password = HashPassword(userModel.Password);

            string userJson = JsonConvert.SerializeObject(userModel);
            var parameters = new DynamicParameters();
            parameters.Add("@UserJson", userJson, DbType.String);

            parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    await con.OpenAsync();

                    await con.ExecuteAsync("Sp_InsertUser", parameters, commandType: CommandType.StoredProcedure);

                    int status = parameters.Get<int>("@Status");

                    return status == 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    return false;
                }
            }
        }
        public async Task<tbl_users> GetUserByUserName(string username)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", username, DbType.String);

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    await con.OpenAsync();

                    var user = await con.QuerySingleOrDefaultAsync<tbl_users>(
                        "Sp_GetUserByUserName",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return user;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<string> GetConnetionByLabId(Guid LabId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LabId", LabId.ToString(), DbType.String);

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    await con.OpenAsync();

                    var user = await con.QuerySingleOrDefaultAsync<string>("Sp_GetConnetionByLabId", parameters,commandType: CommandType.StoredProcedure
                    );

                    return user;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        private bool CheckPassword(string enteredPassword, string storedHash)
        {
            string hashedEnteredPassword = HashPassword(enteredPassword);

            return hashedEnteredPassword == storedHash;
        }

        public async Task<LoginResponse> Login(string username, string password)
        {
            try
            {
                tbl_users user = await GetUserByUserName(username);
                bool isValid = CheckPassword(password, user.Password);
                if (isValid)
                {
                    var response = new LoginResponse()
                    {
                        Emailid = user.Emailid,
                        MobileNumber = user.MobileNumber,
                        LABID = user.LABID.ToString(),
                        Connection = await GetConnetionByLabId(user.LABID),
                        GlobalUID = user.GlobalUID,
                    };
                    var responseBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));

                    var session = _httpContextAccessor.HttpContext.Session;
                    session.Set("LoginResponse", responseBytes);
                    return response;
                }
                else { return null; }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public async Task<LoginResponse> GetSessionDetails()
        {
            try
            {
                var session = _httpContextAccessor.HttpContext.Session;
                byte[] responseBytes = Web_APIS.Models.SessionExtensions.Get(session, "LoginResponse");

                if (responseBytes != null)
                {
                    var jsonResponse = Encoding.UTF8.GetString(responseBytes);
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);
                    return loginResponse;
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
