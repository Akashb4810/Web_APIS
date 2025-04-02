using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
        public List<tbl_users> GetUsers()
        {
            string Sql = "SELECT * FROM tbl_users";
            using (SqlConnection con= new SqlConnection(_connectionString))
            {
                con.Open();
               using (SqlCommand cmd = new SqlCommand(Sql, con))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<tbl_users> users = new List<tbl_users>();
                    while (dr.Read())
                    {
                        tbl_users user = new tbl_users();
                        user.id = Convert.ToInt32(dr["id"]);
                        user.username = dr["username"].ToString();
                        user.password = dr["password"].ToString();
                        user.email = dr["email"].ToString();
                        users.Add(user);
                    }
                    return users;
                }   
            }
        }

    }
}
