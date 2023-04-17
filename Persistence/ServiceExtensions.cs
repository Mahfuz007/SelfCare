using Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistence (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoSettings");
            var dbName = configuration.GetSection("MongoDbSettings");
            services.Configure<DataContext>(opt =>
            {
                opt.ConnectionString = "mongodb://localhost:27017";
                opt.DatabaseName = "SelfCare";
            });
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}
