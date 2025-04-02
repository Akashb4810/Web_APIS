using DLL.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public List<tbl_users> GetUsers()
        {
            _userRepository.GetUsers();
            if (_userRepository.GetUsers() == null)
            {
                return null;
            }
            else
            {
                return _userRepository.GetUsers();
            }
        }
    }
}
