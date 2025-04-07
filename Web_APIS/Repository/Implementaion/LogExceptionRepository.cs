using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_APIS.Models;
using Web_APIS.Repository.Interface;

namespace Web_APIS.Repository.Implementaion
{
    public class LogExceptionRepository : ILogExceptionRepository
    {
        private readonly IUserRepository _userRepository;
        private static LoginResponse sessionDetails;
        public LogExceptionRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            sessionDetails = _userRepository.GetSessionDetails().Result;
        }
        public async Task<int> InsertLog(string MessageString,DateTime Timestamp,string LogType)
        {
            using (SqlConnection conn = new SqlConnection(sessionDetails.Connection))
            using (SqlCommand cmd = new SqlCommand("sp_InsertLogException", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MessageString",MessageString);
                cmd.Parameters.AddWithValue("@Timestamp", Timestamp);
                cmd.Parameters.AddWithValue("@LogType", LogType);

                conn.Open();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

    }
}
