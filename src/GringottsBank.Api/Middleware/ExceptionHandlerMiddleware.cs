using System.Net;
using GringottsBank.Application.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GringottsBank.Api.Middleware
{
    public static class ExceptionHandlerMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            var logger = app.ApplicationServices.GetRequiredService<ILoggerService>();

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        await logger.LogInformation(contextFeature.Error.Message, contextFeature.Error, httpStatusCode: (HttpStatusCode)context.Response.StatusCode, logLevel: LogLevel.Error);

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }
    }


    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    
}