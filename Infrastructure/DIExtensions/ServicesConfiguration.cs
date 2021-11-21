using BlinkCash.Core.Configs;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Core.Managers;
using BlinkCash.Core.Models.JwtModels;
using BlinkCash.Data.Repository;
using BlinkCash.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace BlinkCash.Infrastructure.DIExtensions
{
    public static class ServicesConfiguration
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuration
            services.AddMemoryCache();
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.Configure<JwtTokenConfig>(configuration.GetSection("JwtTokenConfig"));
            services.AddSingleton(configuration.GetSection("AppSettings").Get<AppSettings>());

            // Services 
            services.AddHttpClient();
            services.AddTransient<IHttpService, HttpService>();
            services.AddScoped<IResponseService, ResponseService>();
            services.AddScoped<IUtilityService, UtilityService>();
            services.AddScoped<IPayStackService, PayStackService>();
            services.AddScoped<IBackgroundService, BackgroundService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAccountService, AccountService>();
            // Managers 
            services.AddScoped<IJwtManager, JwtManager>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IPlanManager, PlanManager>();
            services.AddScoped<IBankManager, BankManager>();
            services.AddScoped<ICardRequestManager, CardRequestManager>();
            services.AddScoped<ITransactionManager, TransactionManager>();
            services.AddScoped<IWithdrawalSettingManager, WithdrawalSettingManager>();
            // Repository 
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IUserBankRepository, UserBankRepository>();
            services.AddScoped<ICardRequestRepository, CardRequestRepository>();
            services.AddScoped<IStandingOrderRepository, StandingOrderRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IWalletBalanceRepository, WalletBalanceRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IWithDrawalSettingRepository, WithDrawalSettingRepository>();

        }

        public static void AddDocumentationServices(this IServiceCollection services, string swaggerTitle = "")
        {
            services.AddSwaggerGen(c =>
            {
                string title = !string.IsNullOrEmpty(swaggerTitle) ? swaggerTitle : "BlinkCash";
                c.SwaggerDoc("v1", new OpenApiInfo { Title = $"{ swaggerTitle}", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                  {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
