using System.Collections.Generic;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace DLL.Services.Interface
{
    public interface ICollectionCenterService
    {
        Task<bool> InsertUpdateCenterAsync(CollectionCenter collectionCenter);
        Task<List<CollectionCenter>> GetCenterDetails(int? centerID);
        Task<bool> DeleteCenterAsync(int centerID);
    }
}
