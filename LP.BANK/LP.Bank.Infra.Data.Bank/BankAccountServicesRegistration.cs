using LP.Bank.Application.Contracts.Persistence;
using LP.Bank.Infra.Data.Bank.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LP.Bank.Infra.Data.Bank
{
    public static class BankAccountServicesRegistration
    {
        public static IServiceCollection ConfigureBankAccountServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BankAccountsDbContext>(options =>
               options.UseSqlServer(
                   ));

            var connectionString = configuration.GetConnectionString("PricingDB");

            services.AddEntityFrameworkNpgsql()
            .AddDbContext<BankAccountsDbContext>(options => configuration.GetConnectionString("BankAccountsConnectionString"));



            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
       
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();

            return services;
        }
    }
}
