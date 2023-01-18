using LP.Bank.Application.Contracts.Persistence;
using LP.Bank.Infra.Data.Bank.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LP.Bank.Infra.Data.Bank
{
    public static class BankAccountServicesRegistration
    {
        public static IServiceCollection ConfigureBankAccountServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
       
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();

            var connectionString = configuration.GetConnectionString("BankAccountsConnectionString");
            services.AddEntityFrameworkNpgsql()
            .AddDbContext<BankAccountsDbContext>(options => options.UseNpgsql(connectionString));

            return services;
        }

        public static IApplicationBuilder AppUseBankAccountMigrations(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BankAccountsDbContext>();

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }

            return app;
        }
    }
}
