using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace Web_APIS.Repository.Interface
{
    public interface ILogExceptionRepository
    {
        Task<int> InsertLog(string MessageString, DateTime Timestamp, string LogType);
    }
}
