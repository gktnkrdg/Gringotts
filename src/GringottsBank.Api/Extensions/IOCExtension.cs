using GringottsBank.Api.Handlers;
using GringottsBank.Application.Services.Contracts;
using GringottsBank.Application.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace GringottsBank.Api.Extensions
{
    public static class IOCExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IBankAccountService, BankAccountService>();
        }
    }
}