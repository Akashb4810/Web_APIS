using DLL.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_APIS.Models;
using Web_APIS.Repository.Interface;

namespace DLL.Services.Implementation
{
    public class RateServices : IRateServices
    {
        private readonly IRateRepository _rateRepository;

        public RateServices(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }
        public async Task<List<MstRateList>> GetRateList(int? rateListId = null)
        {
            var result = await _rateRepository.GetRateList(rateListId);
            return result.ToList();
        }
        public async Task<int> InsertUpdateRateList(MstRateList mstRateList)
        {
            return await _rateRepository.InsertUpdateRateList(mstRateList);
        }
        public async Task<bool> SoftDeleteRateList(int rateListId)
        {
            return await _rateRepository.SoftDeleteRateList(rateListId);
        }
    }
}
