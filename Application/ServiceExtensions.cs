using Application.Common;
using Application.Features.CategoryFeatures.CreateCategory;
using Application.Features.UpdateCategory;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(ServiceExtensions).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehaviour<,>));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>(ServiceLifetime.Singleton);
            services.AddValidatorsFromAssemblyContaining<UpdateCategoryValidator>(ServiceLifetime.Singleton);
            return services;
        }
    }
}
