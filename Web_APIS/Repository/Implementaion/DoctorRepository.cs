using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Web_APIS.Models;
using Web_APIS.Repository.Interface;

namespace Web_APIS.Repository.Implementaion
{
    public class DoctorRepository : IDoctorRepository                  
    {
        private readonly IUserRepository _userRepository;
        private static LoginResponse sessionDetails;
        public string _connectionString = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configurationSystem;


        public DoctorRepository(IUserRepository userRepository,IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _configurationSystem = configuration;

            sessionDetails = _userRepository.GetSessionDetails().Result ?? new LoginResponse();
        }

        public async Task<List<DoctorClass>> GetDoctorsAsync(int? CenterId, string doctorName)
        {
            var doctorList = new List<DoctorClass>();

            try
            {
                using (SqlConnection conn = new SqlConnection(sessionDetails.Connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetDoctors", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CenterId", CenterId ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DoctorName", string.IsNullOrWhiteSpace(doctorName) ? (object)DBNull.Value : doctorName);

                        await conn.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                doctorList.Add(new DoctorClass
                                {
                                    DoctorID = reader.GetInt32(reader.GetOrdinal("DoctorID")),
                                    DoctorName = reader["DoctorName"]?.ToString(),
                                    MobileNo = reader["MobileNo"]?.ToString(),
                                    EmailId = reader["EmailId"]?.ToString(),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    ModifiedDate = reader["ModifiedDate"] as DateTime?,
                                    CreatedByUserID = reader["CreatedByUserID"] != DBNull.Value ? Guid.Parse(reader["CreatedByUserID"].ToString()) : Guid.Empty,
                                    ModifiedByUserID = reader["ModifiedByUserID"] != DBNull.Value ? Guid.Parse(reader["ModifiedByUserID"].ToString()) : (Guid?)null,
                                    Deleted = reader.GetBoolean(reader.GetOrdinal("Deleted")),
                                    RateListId = reader["RateListId"] as int?,
                                    CenterId = reader.GetInt32(reader.GetOrdinal("CenterId"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetDoctorsAsync: {ex.Message}");
            }

            return doctorList;
        }

        public async Task<int> InsertOrUpdateDoctorAsync(DoctorClass doctor)
        {
            int resultId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(sessionDetails.Connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertOrUpdateDoctor", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DoctorID", doctor.DoctorID == 0 ? (object)DBNull.Value : doctor.DoctorID);
                        cmd.Parameters.AddWithValue("@DoctorName", doctor.DoctorName);
                        cmd.Parameters.AddWithValue("@MobileNo", doctor.MobileNo);
                        cmd.Parameters.AddWithValue("@EmailId", doctor.EmailId);
                        cmd.Parameters.AddWithValue("@CreatedByUserID", doctor.CreatedByUserID);
                        cmd.Parameters.AddWithValue("@ModifiedByUserID", doctor.ModifiedByUserID);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@RateListId", doctor.RateListId ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CenterId", doctor.CenterId);


                        await conn.OpenAsync();
                        var result = await cmd.ExecuteScalarAsync();
                        resultId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in InsertOrUpdateDoctorAsync: " + ex.Message);
            }

            return resultId;
        }

        public async Task<bool>DeleteDoctorAsync(int doctorId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(sessionDetails.Connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteDoctor", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                       // int modifiedByUserId = sessionDetails?.UserID ?? 0;
                        Guid modifiedByUserId = sessionDetails.GlobalUID ;

                        cmd.Parameters.AddWithValue("@DoctorID", doctorId);
                        cmd.Parameters.AddWithValue("@ModifiedByUserID", modifiedByUserId);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                        await conn.OpenAsync();
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in SoftDeleteDoctorAsync: " + ex.Message);
                return false;
            }
        }



    }
}
