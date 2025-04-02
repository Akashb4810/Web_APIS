using System.Collections.Generic;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace DLL.Services.Interface
{
    public interface IUserServices
    {
        Task<List<tbl_users>> GetUsers();
    }
}
