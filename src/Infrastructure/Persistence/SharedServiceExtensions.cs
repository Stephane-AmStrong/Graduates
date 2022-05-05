using Application.Interfaces;
using Domain.Settings;
using Persistence.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Persistence
{
    public static class SharedServiceExtensions
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            //var emailConfig = _config
            //    .GetSection("EmailSettings")
            //    .Get<EmailSettings>();
            //services.AddSingleton(emailConfig);

            services.Configure<EmailSettings>(_config.GetSection("EmailSettings"));
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
