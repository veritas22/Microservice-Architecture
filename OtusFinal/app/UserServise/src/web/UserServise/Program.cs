using Application;
using Domain;
using EntityCorePostgres;
using Grps;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Prometheus;
using UserServise;
using OtlpInfra;

using static Grps.AccountServiseGrps;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<AuthSetting>()
    .Bind(builder.Configuration.GetSection("AuthSetting"));
string pgConfig = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
string secret = Environment.GetEnvironmentVariable("SECRET");
builder.Services.AddGrpcClient<AccountServiseGrpsClient>(o =>
{
    o.Address = new Uri($"{builder.Configuration.GetSection("GrpsConnection").Value}");
});
// Add services to the container.
builder.Services.AddGrpsService();
builder.Services.AddEFPostgresrStores(pgConfig);
builder.Services.AddApplicationServise(secret);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
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
builder.Services.AddHostedService<NeuroStartup>();
builder.Services.AddOpenTelemetry(builder.Host, Environment.GetEnvironmentVariable("OTLP_CONNECTION"));

var app = builder.Build();
app.UseMetricServer();
app.UseHttpMetrics();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserService");
    c.InjectStylesheet("/swagger/custom.css");
    c.RoutePrefix = String.Empty;
});




app.UseHttpsRedirection();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();


