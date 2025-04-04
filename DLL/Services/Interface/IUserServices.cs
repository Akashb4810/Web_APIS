using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace DLL.Services.Interface
{
    public interface IUserServices
    {
        Task<List<tbl_users>> GetUsers();
        Task<bool> InsertUserAsync(tbl_users userModel);
        Task<LoginResponse> Login(string username, string password);
        Task<string> GetConnetionByLabId(Guid LabId);
    }
}
