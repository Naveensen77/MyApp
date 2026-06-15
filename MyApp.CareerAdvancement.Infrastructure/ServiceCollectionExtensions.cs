using Microsoft.Extensions.DependencyInjection;
using MyApp.CareerAdvancement.Infrastructure.Data;

namespace MyApp.CareerAdvancement.Infrastructure
{
    public static class CareerServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            string queryConnectionString, string commandConnectionString)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentException.ThrowIfNullOrWhiteSpace(queryConnectionString);
            ArgumentException.ThrowIfNullOrWhiteSpace(commandConnectionString);
            services.AddDataServices(queryConnectionString, commandConnectionString);
            return services;
        }
    }
}
