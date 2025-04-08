using System.Collections.Generic;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace Web_APIS.Repository.Interface
{
    public interface IRateRepository
    {
        Task<List<MstRateList>> GetRateList(int? rateListId = null);
        Task<int> InsertUpdateRateList(MstRateList mstRateList);
        Task<bool> SoftDeleteRateList(int rateListId);
    }
}
