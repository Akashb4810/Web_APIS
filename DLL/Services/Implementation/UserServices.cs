using DLL.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web_APIS.Models;
using Web_APIS.Repository;

namespace DLL.Services.Implementation
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
                _userRepository = userRepository;
        }
        public async Task<List<tbl_users>> GetUsers()
        {
            _userRepository.GetUsersAsync();
            if (_userRepository.GetUsersAsync() == null)
            {
                return null;
            }
            else
            {
                return await _userRepository.GetUsersAsync();
            }
        }
    }
}
