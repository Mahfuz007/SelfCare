using Application;
using Application.Common.Interfaces;
using Persistence;
using Persistence.Context;
using Persistence.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
      .AddApplication()
      .AddPersistence(builder.Configuration);

var rabbitMQService = new RabbitMqService(new List<string> { "SelfCare.debug", "SelfcareCommand" });
await rabbitMQService.InitializeAsync();
builder.Services.AddSingleton<IRabbitMqService>(rabbitMQService);
builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.PropertyNamingPolicy = null;
});

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSerilog();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var mongoDbInitializer = scope.ServiceProvider.GetRequiredService<MongoDbInitializer>();
    await mongoDbInitializer.InitializeIndexesAsync();
    await mongoDbInitializer.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost4200");

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
