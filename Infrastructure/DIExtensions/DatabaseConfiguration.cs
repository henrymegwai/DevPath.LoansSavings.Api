using BlinkCash.Core.Models;
using BlinkCash.Data;
using BlinkCash.Data.DapperConnection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlinkCash.Infrastructure.DIExtensions
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration Configuration)
        {
            var connectionString = Configuration.GetConnectionString("BlinkCashDbContext");

            services.AddDbContextPool<AppDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(connectionString);
                optionsBuilder.UseInternalServiceProvider(serviceProvider);
            });
            services.AddIdentity<IdentityUserExtension, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = false;
                options.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequireNonAlphanumeric = true,
                    RequireLowercase = false,
                    RequireUppercase = false, 
                    RequiredLength = 8, 
                };
                options.Lockout = new LockoutOptions { AllowedForNewUsers = false };

            }).AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddEntityFrameworkSqlServer();
            services.AddTransient<DbContext, AppDbContext>(); 
            services.AddTransient<IDapperContext, DapperContext>();
        }
    }
}
