using Application;
using Application.Common.Mappings;
using Application.Interfaces;
using EntityCorePostgres;
using Grps.Services;
using Microsoft.OpenApi.Models;
using OtlpInfra;
using Prometheus;
using System.Reflection;
using Warehouse;
using Warehouse.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();

string pgConfig = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
string secret = Environment.GetEnvironmentVariable("SECRET");
// Add services to the container.
builder.Services.AddEFPostgresrStores(pgConfig);
builder.Services.AddJwtAuthentication(secret);
builder.Services.AddHostedService<NeuroStartup>();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
});
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Warehouse", Version = "v1" });
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

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Warehouse");
    c.InjectStylesheet("/swagger/custom.css");
    c.RoutePrefix = String.Empty;
});

app.UseHttpsRedirection();
app.MapGrpcService<WarehouseServise>();

app.UseAuthorization();

app.MapControllers();

app.Run();
