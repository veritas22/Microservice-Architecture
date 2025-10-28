using Application;
using EntityCorePostgres;
using Microsoft.OpenApi.Models;
using Notification;
using OtlpInfra;
using Prometheus;
using RabbitMQNeuro;


var builder = WebApplication.CreateBuilder(args);
AddConfig();
// Add services to the container.
builder.Services.AddApplicationServise();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
string pgConfig = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
string secret = Environment.GetEnvironmentVariable("SECRET");
builder.Services.AddEFPostgresrStores(pgConfig);
builder.Services.AddRabbitMQservice();
builder.Services.AddHostedService<NeuroStartup>();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification", Version = "v1" });
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
builder.Services.AddJwtAuthentication(secret);
builder.Services.AddOpenTelemetry(builder.Host, Environment.GetEnvironmentVariable("OTLP_CONNECTION"));

var app = builder.Build();
app.UseMetricServer();
app.UseHttpMetrics();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification_v1");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
void AddConfig()
{
    builder.Services.AddOptions<RabbitMqConfig>()
            .Bind(builder.Configuration.GetSection("RabbitMQ"));
}