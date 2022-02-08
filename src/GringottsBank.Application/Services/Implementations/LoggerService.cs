using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GringottsBank.Application.Services.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GringottsBank.Application.Services.Implementations
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger _logger;

        public LoggerService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LoggerService>();
        }


        public async Task LogInformation(string message, Exception exception = null, string responseBody = null,
            string requestBody = null, HttpMethod httpMethod = null, HttpStatusCode? httpStatusCode = null,
            long? duration = null, string url = null, string origin = null, LogLevel logLevel = LogLevel.Information)
        {
            await Log(logLevel, message, exception, responseBody, requestBody, httpMethod,
                httpStatusCode.GetValueOrDefault(),
                duration,
                url, origin);
        }

        private async Task Log(LogLevel logLevel, string message, Exception exception, string responseBody,
            string requestBody, HttpMethod httpMethod, HttpStatusCode httpStatusCode, long? duration, string url,
            string origin)
        {
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(new
            {
                Message = new
                {
                    CorrelationId = Trace.CorrelationManager.ActivityId,
                    DateTime = DateTime.Now,
                    LogLevel = (int)logLevel,
                    Message = message,
                    Exception = exception?.ToString(),
                    ResponseBody = responseBody,
                    RequestBody = requestBody,
                    HttpMethod = httpMethod?.Method,
                    HttpStatusCode = (int)httpStatusCode,
                    Duration = duration ?? default,
                    Url = url,
                    Origin = origin
                }
            }));
        }
    }
}