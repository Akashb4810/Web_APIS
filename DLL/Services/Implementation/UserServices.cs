using DLL.Services.Interface;
using System;
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

        public async Task<string> GetConnetionByLabId(Guid LabId)
        {
            return await _userRepository.GetConnetionByLabId(LabId);
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

        public async Task<bool> InsertUserAsync(tbl_users userModel)
        {
            return await _userRepository.InsertUserAsync(userModel);
        }

        public async Task<bool> Login(string username, string password)
        {
            return await _userRepository.Login(username,password);
        }
    }
}
