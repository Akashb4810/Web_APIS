using System.Collections.Generic;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace DLL.Services.Interface
{
    public interface IRateServices
    {
        Task<int> InsertUpdateRateList(MstRateList mstRateList);
        Task<List<MstRateList>> GetRateList(int? rateListId = null);
        Task<bool> SoftDeleteRateList(int rateListId);
    }
}
