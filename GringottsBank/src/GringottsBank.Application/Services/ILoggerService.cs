using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GringottsBank.Application.Services
{
    
  
    public interface ILogService
    {
        Task LogInformation(string message, Exception exception = null, string responseBody = null,
            string requestBody = null,
            HttpMethod httpMethod = null, HttpStatusCode? httpStatusCode = null, long? duration = null,
            string url = null, string origin = null, LogLevel logLevel = LogLevel.Information);
    }
}