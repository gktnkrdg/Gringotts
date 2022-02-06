using AutoMapper;
using GringottsBank.Application.Services;
using GringottsBank.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GringottsBank.Api.Extensions
{
    public static class IOCExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<ILogService, LogService>();
            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICustomerService, CustomerService>();
         
            services.AddAutoMapper(typeof(Startup));
            
        }
    }
}