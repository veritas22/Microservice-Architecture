using Application;
using EntityCorePostgres;
using Grps;
using Microsoft.OpenApi.Models;
using Order;
using Order.Filter;
using Order.Grps;
using Order.Idempotent;
using Order.Swagger;
using OtlpInfra;
using Prometheus;
using RabbitMQNeuro;
using Redis;
using static Grps.BillingServiseGrps;
using static Grps.DeliveryServiseGrps;
using static Grps.WarehouseServiseGrps;

var builder = WebApplication.CreateBuilder(args);
string pgConfig = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
string secret = Environment.GetEnvironmentVariable("SECRET");
AddConfig();
// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddGrpsOrder(builder.Configuration);
builder.Services.AddJwtAuthentication(secret);
builder.Services.AddRabbitMQservice();
builder.Services.AddEFPostgresrStores(pgConfig);
builder.Services.AddApplicationServise(builder.Configuration);
builder.Services.AddBillingService();
builder.Services.AddHostedService<NeuroStartup>();
builder.Services.AddSwaggerGen(opt =>
{
    opt.OperationFilter<AddCustomHeaderParameter>();

    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Order", Version = "v1" });
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
builder.Services.AddRedisService(builder.Configuration);
var app = builder.Build();
app.UseMiddleware<HttpLoggingMiddleware>();
app.UseMetricServer();
app.UseHttpMetrics();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order_v1");
    c.RoutePrefix = String.Empty;
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers()
    .AddEndpointFilter(new IdempotencyFilter()); ;

app.Run();
void AddConfig()
{
    builder.Services.AddOptions<RabbitMqConfig>()
            .Bind(builder.Configuration.GetSection("RabbitMQ"));
}