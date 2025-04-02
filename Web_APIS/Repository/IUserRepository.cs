using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace Web_APIS.Repository
{
    public interface IUserRepository
    {
        List<tbl_users> GetUsers();
    }
}
