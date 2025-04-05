using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Threading.Tasks;
using Web_APIS.Models;
using Web_APIS.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Web_APIS.Repository.Implementaion
{
    public class CollectionCenterRepository : ICollectionCenterRepository
    {
        private readonly IConfiguration _configurationSystem;
        private readonly IUserRepository _userRepository;
        private static LoginResponse sessionDetails;
        public string _connectionString = null;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CollectionCenterRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _configurationSystem = configuration;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            sessionDetails = _userRepository.GetSessionDetails().Result;
        }

        public async Task<List<CollectionCenter>> GetCenterDetails(int? centerID)
        {
            _connectionString = _userRepository.GetSessionDetails().Result.Connection;

            var parameters = new DynamicParameters();
            parameters.Add("@CenterID", centerID);
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    return (await con.QueryAsync<CollectionCenter>("Sp_GetCenterDetails", parameters, commandType: CommandType.StoredProcedure)).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    throw new Exception("Error occurred while fetching centers.", ex);
                }
            }
        }

        public async Task<bool> InsertUpdateCenterAsync(CollectionCenter collectionCenter)
        {
            _connectionString = _userRepository.GetSessionDetails().Result.Connection;

            var parameters = new DynamicParameters();
            parameters.Add("@CenterID", collectionCenter.CenterID);
            parameters.Add("@CenterName", collectionCenter.CenterName);
            parameters.Add("@Address", collectionCenter.Address);
            parameters.Add("@IsDefaultCenter", collectionCenter.IsDefaultCenter);
            parameters.Add("@CreationDateTime", DateTime.Now);
            parameters.Add("@ModifyDateTime", DateTime.Now);
            parameters.Add("@CreationUID", sessionDetails.GlobalUID);
            parameters.Add("@ModifyUID", sessionDetails.GlobalUID);
            parameters.Add("@MobileNumber", collectionCenter.MobileNumber);
            parameters.Add("@Emailid", collectionCenter.Emailid);
            parameters.Add("@ActiveFlag", collectionCenter.ActiveFlag);
            parameters.Add("@CenterShortName", collectionCenter.CenterShortName);
            parameters.Add("@Labcode", collectionCenter.Labcode ?? (object)DBNull.Value);
            parameters.Add("@Interval", collectionCenter.Interval ?? (object)DBNull.Value);
            parameters.Add("@AutoIncrement", collectionCenter.AutoIncrement ?? (object)DBNull.Value);
            parameters.Add("@VisitCodeStart", collectionCenter.VisitCodeStart ?? (object)DBNull.Value);
            parameters.Add("@VisitCodeLength", collectionCenter.VisitCodeLength ?? (object)DBNull.Value);
            parameters.Add("@RateMstId", collectionCenter.RateMstId ?? (object)DBNull.Value);
            parameters.Add("@VerifyAmtPaidforPrinting", collectionCenter.VerifyAmtPaidforPrinting ?? (object)DBNull.Value);
            parameters.Add("@HeaderImage", collectionCenter.HeaderImage);

            parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);  // For error message

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    await con.ExecuteAsync("Sp_InsertOrUpdateCenter", parameters, commandType: CommandType.StoredProcedure);
                    string errorMessage = parameters.Get<string>("@ErrorMessage");
                    if (errorMessage == "Record inserted successfully." || errorMessage == "Record updated successfully.")
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    return false;
                }
            }
        }

        public async Task<bool> DeleteCenterAsync(int centerID)
        {
            _connectionString = _userRepository.GetSessionDetails().Result.Connection;

            var parameters = new DynamicParameters();
            parameters.Add("@CenterID", centerID);

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    int rowsAffected = await con.ExecuteAsync("Sp_DeleteCenter", parameters, commandType: CommandType.StoredProcedure);
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    throw new Exception("Error occurred while deleting centers.", ex);
                }
            }
        }
    }
}
