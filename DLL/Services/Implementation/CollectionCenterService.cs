using DLL.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web_APIS.Models;
using Web_APIS.Repository.Interface;
namespace DLL.Services.Implementation
{
    public class CollectionCenterService : ICollectionCenterService
    {
        private readonly ICollectionCenterRepository _collectionCenterRepository;

        public CollectionCenterService(ICollectionCenterRepository collectionCenterRepository)
        {
            _collectionCenterRepository = collectionCenterRepository;
        }

        public async Task<bool> DeleteCenterAsync(int centerID)
        {
            return await _collectionCenterRepository.DeleteCenterAsync(centerID);
        }

        public async Task<List<CollectionCenter>> GetCenterDetails(int? centerID)
        {
            return await _collectionCenterRepository.GetCenterDetails(centerID);
        }

        public async Task<bool> InsertUpdateCenterAsync(CollectionCenter collectionCenter)
        {
            return await _collectionCenterRepository.InsertUpdateCenterAsync(collectionCenter);
        }
    }
}
