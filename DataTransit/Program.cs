using DataTransit.Contract;
using DataTransit.Datalayer.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
DataTransit.Common.Helper.ConfigurationManager.Configuration = builder.Configuration;


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Context
builder.Services.AddDbContext<DataTransitContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr"), providerOptions => providerOptions.EnableRetryOnFailure()));
#endregion

#region IOC
DataTransit.Contract.StartUp.Start(builder.Services);
builder.Services.AddMediatR(typeof(DataTransit.Contract.StartUp).Assembly);
#endregion

#region Redis
builder.Services.AddDistributedMemoryCache();
var redisConnection = builder.Configuration["RedisConnection:Redis"];
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnection;
    options.InstanceName = "DataTransit_Exel_";
});

#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
