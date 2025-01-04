using Application.Common.Interfaces;
using Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IExpenseRepository, ExpenseRepository>();
            services.AddSingleton<ITaxRepository, TaxRepository>();

            services.AddSingleton<MongoDbInitializer>(sp =>
            {
                var dbSetting = sp.GetRequiredService<IMongoDbSettings>();
                return new MongoDbInitializer(dbSetting);
            });

            services.AddScoped<IExcelReader, ExcelReader>();
            services.AddSingleton<IServiceClient, ServiceClient>();
            return services;
        }
    }
}
