using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Web_APIS.Models;
using Web_APIS.Repository.Interface;

namespace Web_APIS.Repository.Implementaion
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IUserRepository _userRepository;
        private static LoginResponse sessionDetails;

        public PatientRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            sessionDetails = _userRepository.GetSessionDetails().Result;
        }
       
        public async Task<List<PatientType>> GetAllPatientTypes()
        {
            using (var connection = new SqlConnection(sessionDetails.Connection))
            {
                var result = await connection.QueryAsync<PatientType>(
                    "usp_GetAllPatientTypes"
                );

                return result.ToList();
            }
        }
        public async Task<int> InsertPatientType(PatientType patientType)
        {
            using (var connection = new SqlConnection(sessionDetails.Connection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PatientTypeID", patientType.PatientTypeID);
                parameters.Add("@PatientType", patientType.PatientTypeName);
                parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var result = await connection.ExecuteAsync(
                    "usp_UpsertPatientType",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        public async Task<int> DeletePatientType(int patientTypeId)
        {
            using (var connection = new SqlConnection(sessionDetails.Connection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PatientTypeID", patientTypeId);
                parameters.Add("@RowAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = await connection.ExecuteScalarAsync<int>(
                    "usp_DeletePatientTypeByID",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                int rowsAffected = parameters.Get<int>("@RowAffected");
                return rowsAffected;
            }
        }



    }
}
