using Application;
using Billing;
using EntityCorePostgres;
using GrpcMailService.Services;
using Grps.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OtlpInfra;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
string pgConfig = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
string secret = Environment.GetEnvironmentVariable("SECRET");

// Add services to the container.
builder.Services.AddEFPostgresrStores(pgConfig);
builder.Services.AddJwtAuthentication(secret);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHostedService<NeuroStartup>();
builder.Services.AddApplicationServise();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Billing", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                 Reference = new OpenApiReference
                {
                     Type=ReferenceType.SecurityScheme,
                     Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddOpenTelemetry(builder.Host, Environment.GetEnvironmentVariable("OTLP_CONNECTION"));

var app = builder.Build();

app.UseMetricServer();
app.UseHttpMetrics();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Billing");
    c.InjectStylesheet("/swagger/custom.css");
    c.RoutePrefix = String.Empty;
});

app.UseHttpsRedirection();

app.MapGrpcService<AccountServise>();
app.MapGrpcService<BillingServise>();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
