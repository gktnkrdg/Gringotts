using GringottsBank.Application.Services.Contracts;
using GringottsBank.Application.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace GringottsBank.Api.Extensions
{
    public static class IOCExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<ILogService, LogService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddAutoMapper(typeof(Startup));
        }
    }
}