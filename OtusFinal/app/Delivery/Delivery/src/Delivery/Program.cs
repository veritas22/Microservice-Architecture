using Application;
using Application.Interfaces;
using Delivery;
using EntityCorePostgres;
using GrpcService.Services;
using Microsoft.OpenApi.Models;
using OtlpInfra;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();

string pgConfig = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
string secret = Environment.GetEnvironmentVariable("SECRET");
// Add services to the container.
builder.Services.AddEFPostgresrStores(pgConfig);
builder.Services.AddHostedService<NeuroStartup>();
builder.Services.AddApplicationServise();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddOpenTelemetry(builder.Host, Environment.GetEnvironmentVariable("OTLP_CONNECTION"));

var app = builder.Build();

app.UseMetricServer();
app.UseHttpMetrics();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Warehouse");
//    c.InjectStylesheet("/swagger/custom.css");
//    c.RoutePrefix = String.Empty;
//});
app.MapGrpcService<DeliveryService>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
