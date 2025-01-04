using Application;
using Application.Common.Interfaces;
using Persistence;
using Persistence.Context;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services
      .AddApplication()
      .AddPersistence(builder.Configuration);

var rabbitMQService = new RabbitMqService(new List<string> { "SelfCare.debug", "SelfcareCommand" });
await rabbitMQService.InitializeAsync();
builder.Services.AddSingleton<IRabbitMqService>(rabbitMQService);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var mongoDbInitializer = scope.ServiceProvider.GetRequiredService<MongoDbInitializer>();
    await mongoDbInitializer.InitializeIndexesAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
