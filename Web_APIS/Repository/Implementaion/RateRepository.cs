using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Web_APIS.Models;
using Web_APIS.Repository.Interface;
using Web_APIS.Repository;
using System.Collections.Generic;
using System.Linq;

public class RateRepository : IRateRepository
{
    private readonly IUserRepository _userRepository;
    private static LoginResponse sessionDetails;

    public RateRepository(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        sessionDetails = _userRepository.GetSessionDetails().Result;
    }

    public async Task<List<MstRateList>> GetRateList(int? rateListId = null)
    {
        using (var connection = new SqlConnection(sessionDetails.Connection))
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RateListId", rateListId);

            var result = await connection.QueryAsync<MstRateList>(
                "usp_GetRateList",
                parameters,
                commandType: CommandType.StoredProcedure
            );
            return result.ToList();
        }
    }
    public async Task<int> InsertUpdateRateList(MstRateList mstRateList)
    {
        using (var connection = new SqlConnection(sessionDetails.Connection))
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RateListId", mstRateList.RateListId);
            parameters.Add("@RateTypeName", mstRateList.RateTypeName);
            parameters.Add("@IsDeleted", mstRateList.IsDeleted);
            parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                "usp_InsertUpdateRateList",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            int rowsAffected = parameters.Get<int>("@RowsAffected");
            return rowsAffected;
        }
    }
    public async Task<bool> SoftDeleteRateList(int rateListId)
    {
        using (var connection = new SqlConnection(sessionDetails.Connection))
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RateListId", rateListId);
            parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync("usp_DeleteRateList", parameters, commandType: CommandType.StoredProcedure);
            int rowsAffected = parameters.Get<int>("@RowsAffected");
            return rowsAffected > 0;
        }
    }
}

