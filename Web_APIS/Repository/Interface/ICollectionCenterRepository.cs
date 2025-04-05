using System.Collections.Generic;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace Web_APIS.Repository.Interface
{
    public interface ICollectionCenterRepository
    {
        Task<bool> InsertUpdateCenterAsync(CollectionCenter collectionCenter);
        Task<List<CollectionCenter>> GetCenterDetails(int? centerID);
        Task<bool> DeleteCenterAsync(int centerID);
    }
}
