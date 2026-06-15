using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyApp.CareerAdvancement.Endpoints.Endpoints.SessionsManagement;
using MyApp.CareerAdvancement.Infrastructure;
using MyApp.Endpoints.Abstractions;
using MyApp.CareerAdvancement.Application.Features.SessionsManagement.CreateSession;
using System;

namespace MyApp.CareerAdvancement.Endpoints
{
    public static class CareerServiceCollectionExtensions
    {
        public static IServiceCollection AddCareerApis(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddApiEndpointsFromAssemblyOf<SessionsManagementEndpoints>();
            return services;
        }

        public static IServiceCollection AddCareerServices(
            this IServiceCollection services, string connectionString)
        {
            services.AddValidatorsFromAssembly(typeof(CreateSessionCommandValidator).Assembly);
            services.AddInfrastructureServices(connectionString, connectionString);
            return services;
        }
    }
}
