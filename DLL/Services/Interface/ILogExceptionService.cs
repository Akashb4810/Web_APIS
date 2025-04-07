using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Services.Interface
{
    public interface ILogExceptionService
    {
        Task<int> InsertLog(string MessageString, DateTime Timestamp, string LogType);
    }
}
