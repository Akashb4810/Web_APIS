using System.Collections.Generic;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace Web_APIS.Repository
{
    public interface IUserRepository
    {
        Task<List<tbl_users>> GetUsersAsync();
    }
}
