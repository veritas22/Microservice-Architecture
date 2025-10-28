using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Order.Idempotent
{
    internal sealed class IdempotencyFilter(int cacheTimeInMinutes = 60)
        : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            if (context.HttpContext.GetEndpoint()?.Metadata.GetMetadata<IdempotencyAttribute>() == null)
            {
                return await next(context);
            }
            var headers = context.HttpContext.Request.Headers;

            // Parse the Idempotence-Key header from the request
            if (!headers.TryGetValue("Idempotency-Key", out var idempotencyheadersKey))
            {
                return Results.BadRequest("Invalid or missing Idempotence-Key header");
            }

            IDistributedCache cache = context.HttpContext
                .RequestServices.GetRequiredService<IDistributedCache>();

            // Check if we already processed this request and return a cached response (if it exists)
            var idempotencyKey = idempotencyheadersKey.ToString();
            string cacheKey = $"Idempotent_{idempotencyKey}";

            string? cachedResult = await cache.GetStringAsync(cacheKey);
            if (cachedResult is not null)
            {
                IdempotentResponse response = JsonSerializer.Deserialize<IdempotentResponse>(cachedResult)!;
                return new IdempotentResult(response.StatusCode, response.Value);
            }

            var result = await next(context);

            // Execute the request and cache the response for the specified duration
            if (result is OkObjectResult { StatusCode: >= 200 and < 300 } statusCodeResult)
            {

                int statusCode = statusCodeResult.StatusCode ?? StatusCodes.Status200OK;
                IdempotentResponse response = new(statusCode, statusCodeResult.Value);

                await cache.SetStringAsync(
                    cacheKey,
                    JsonSerializer.Serialize(response),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheTimeInMinutes)
                    }
                );
            }

            return result;
        }
    }
    internal sealed class IdempotentResponse
    {
        [JsonConstructor]
        public IdempotentResponse(int statusCode, object? value)
        {
            StatusCode = statusCode;
            Value = value;
        }

        public int StatusCode { get; }
        public object? Value { get; }
    }
}
