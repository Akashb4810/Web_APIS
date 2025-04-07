using DLL.Services.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Web_APIS.Models;
using Web_APIS.Repository.Implementaion;
using Web_APIS.Repository.Interface;

namespace DLL.Services.Implementation
{
    public class LogExceptionService : ILogExceptionService  
    {
        private readonly ILogExceptionRepository _logRepository;

        public LogExceptionService(ILogExceptionRepository logRepository) 
        {
            _logRepository = logRepository;
        }

        public async Task<int> InsertLog(string MessageString, DateTime Timestamp, string LogType)
        {
            return await _logRepository.InsertLog(MessageString, Timestamp, LogType);
        }
    }
}
