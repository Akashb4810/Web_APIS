using Dapper;
using Microsoft.AspNetCore.Http;
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
    public class TestRepository:ITestRepository
    {
        private readonly IConfiguration _configurationSystem;
        private readonly IUserRepository _userRepository;
        private static LoginResponse sessionDetails;
        public string _connectionString = null;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TestRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _configurationSystem = configuration;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            sessionDetails = _userRepository.GetSessionDetails().Result;
        }

        public async Task<bool> InsertUpdateDepartmentTypeAsync(DepartmentType departmentType)
        {
            _connectionString = _userRepository.GetSessionDetails().Result.Connection;
            var parameters = new DynamicParameters();
            parameters.Add("@DepartmentId", departmentType.DepartmentID);
            parameters.Add("@DepartmentName", departmentType.DepartmentName);
            parameters.Add("@CreatedDate", departmentType.CreatedDate);
            parameters.Add("@ModifiedDate", departmentType.ModifiedDate);
            parameters.Add("@CreatedByUserID", departmentType.CreatedByUserID);
            parameters.Add("@ModifiedByUserID", departmentType.ModifiedByUserID);
            parameters.Add("@FooterText", departmentType.FooterText);
            parameters.Add("@PrintFooterOnReport", departmentType.PrintFooterOnReport);
            parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    await con.ExecuteAsync("Sp_InsertOrUpdateDepartmentType", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<List<DepartmentType>> GetDepartmentType(int? departmentTypeId)
        {
            
                _connectionString = _userRepository.GetSessionDetails().Result.Connection;

                var parameters = new DynamicParameters();
                parameters.Add("@DepartmentID", departmentTypeId);
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    try
                    {
                        await con.OpenAsync();
                        return (await con.QueryAsync<DepartmentType>("Sp_GetDepartmentTypeDetails", parameters, commandType: CommandType.StoredProcedure)).ToList();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error occurred: {ex.Message}");
                        throw new Exception("Error occurred while fetching centers.", ex);
                    }
                }
            
        }

        public async Task<bool> InsertUpdateSampleType(SampleType sampleType)
        {
            _connectionString = _userRepository.GetSessionDetails().Result.Connection;
            var parameters = new DynamicParameters();
            parameters.Add("@SampleId", sampleType.SampleId);
            parameters.Add("@Sample", sampleType.Sample);
            parameters.Add("@CreatedDate", sampleType.CreatedDate);
            parameters.Add("@ModifiedDate", sampleType.ModifiedDate);
            parameters.Add("@CreatedByUserID", sampleType.CreatedByUserID);
            parameters.Add("@ModifiedByUserID", sampleType.ModifiedByUserID);
            parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    await con.ExecuteAsync("Sp_CreateOrUpdateSampleType", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<List<SampleType>> GetSampleType(int? sampleTypeId)
        {
            _connectionString = _userRepository.GetSessionDetails().Result.Connection;

            var parameters = new DynamicParameters();
            parameters.Add("@SampleId", sampleTypeId);
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    return (await con.QueryAsync<SampleType>("Sp_GetSampleTypeDetails", parameters, commandType: CommandType.StoredProcedure)).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    throw new Exception("Error occurred while fetching centers.", ex);
                }
            }
        }
    }
}
