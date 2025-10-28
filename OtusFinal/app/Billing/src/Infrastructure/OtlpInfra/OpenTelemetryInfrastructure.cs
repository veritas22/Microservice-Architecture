using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using Serilog;
using Serilog.Sinks.OpenTelemetry;
using static System.Net.Mime.MediaTypeNames;

namespace OtlpInfra
{
    public static class OpenTelemetryInfrastructure
    {
        public static void AddOpenTelemetry(this IServiceCollection self, IHostBuilder hostBuilder, string otelConfig)
        {
            if (!String.IsNullOrEmpty(otelConfig))
            {
                AddLog(self, hostBuilder, otelConfig);
                AddTracing(self, otelConfig);
                AddMetrics(self, otelConfig);
            }
        }

        private static void AddLog(this IServiceCollection self, IHostBuilder hostBuilder, string otelConfig)
        {
            self.AddSerilog();
            var ApplicationBase = System.Reflection.Assembly.GetExecutingAssembly()
;

            hostBuilder.UseSerilog((hostContext, services) =>
            {
                services
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(hostContext.Configuration)
                .Enrich.WithProperty("EnvironmentName", hostContext.HostingEnvironment.EnvironmentName)
                .WriteTo.Console()
                .WriteTo.OpenTelemetry(config =>
                {
                    config.IncludedData = IncludedData.TraceIdField | IncludedData.SpanIdField;
                    config.Protocol = OtlpProtocol.Grpc;
                    config.Endpoint = $"{otelConfig}";
                    config.ResourceAttributes = new Dictionary<string, object>
                    {{"servise.name", hostContext.HostingEnvironment.ApplicationName } };
                });
            });
        }

        private static void AddMetrics(this IServiceCollection self, string otelConfig)
        {
            var otel = self.AddOpenTelemetry();

            otel.WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddMeter("Microsoft.AspNetCore.Hosting")
                .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                .AddMeter("System.Net.NameResolution")
                .AddRuntimeInstrumentation()
                .AddProcessInstrumentation()
                .AddOtlpExporter(option =>
                {
                    option.Endpoint = new Uri(otelConfig);
                    option.ExportProcessorType = ExportProcessorType.Batch;
                    option.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                }));
        }

        private static void AddTracing(this IServiceCollection self, string otelConfig)
        {
            var otel = self.AddOpenTelemetry();

            otel.WithTracing(tracing =>
            {
                tracing.ConfigureResource(res => res.AddService(serviceName: "Billing", serviceVersion: "1.0.0"));
                tracing
                .AddAspNetCoreInstrumentation(option =>
                {
                    option.RecordException = true;
                    option.EnrichWithHttpRequest = (activity, req) =>
                    {
                        activity.DisplayName = req.Method + " " + req.Path;
                        activity.SetTag("api", req.Method);
                    };
                })
                .AddHttpClientInstrumentation()
                .AddSqlClientInstrumentation()
                .AddEntityFrameworkCoreInstrumentation()
                .AddOtlpExporter(option =>
                {
                    option.Endpoint = new Uri(otelConfig);
                    option.ExportProcessorType = ExportProcessorType.Batch;
                    option.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                })
                .AddConsoleExporter();
            });
        }
    }
}
