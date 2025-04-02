using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace Web_APIS.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configurationSystem;
        public string _connectionString = null;
        public UserRepository(IConfiguration configuration)
        {
             _configurationSystem = configuration;
            _connectionString = _configurationSystem.GetConnectionString("MyCon");
        }
        public async Task<List<tbl_users>> GetUsersAsync()
        {
            string Sql = "SELECT * FROM tbl_users";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();  // Open connection asynchronously

                using (SqlCommand cmd = new SqlCommand(Sql, con))
                {
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())  // Execute command asynchronously
                    {
                        List<tbl_users> users = new List<tbl_users>();

                        // Read asynchronously
                        while (await dr.ReadAsync())
                        {
                            tbl_users user = new tbl_users
                            {
                                id = Convert.ToInt32(dr["id"]),
                                username = dr["username"].ToString(),
                                password = dr["password"].ToString(),
                                email = dr["email"].ToString()
                            };
                            users.Add(user);
                        }

                        return users;
                    }
                }
            }
        }

    }
}
