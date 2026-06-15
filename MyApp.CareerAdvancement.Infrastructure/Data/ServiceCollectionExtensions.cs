using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyApp.CareerAdvancement.Core.Data;
using MyApp.Common.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.CareerAdvancement.Infrastructure.Data
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services,
            string queryConnectionString, string commandConnectionString)
        {
            // Command context — attaches audit interceptor
            services.AddDbContext<CareerCommandDbContext>((provider, builder) =>
            {
                IUserInfoAccessor userInfo = provider.GetRequiredService<IUserInfoAccessor>();
                builder.AddInterceptors(new AuditCommandInterceptor(userInfo));
                builder.UseSqlServer(commandConnectionString);
            });
            // Query context — NoTracking for performance
            services.AddDbContext<CareerQueryDbContext>(builder =>
            {
                builder.UseSqlServer(queryConnectionString);
                builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            return services;
        }
    }
}
