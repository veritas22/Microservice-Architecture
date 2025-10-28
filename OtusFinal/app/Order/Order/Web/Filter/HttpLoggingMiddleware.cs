namespace Order.Filter
{
    public class HttpLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpLoggingMiddleware> _logger;

        public HttpLoggingMiddleware(RequestDelegate next, ILogger<HttpLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("content-type", out var contentType) &&
                contentType.ToString().Contains("application/grpc"))
            {
                // Пропускаем gRPC запрос без логирования
                await _next(context);
                return;
            }

            var request = context.Request;
            var startTime = DateTime.UtcNow;

            // Чтение и логирование тела запроса
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var requestBody = System.Text.Encoding.UTF8.GetString(buffer);
            request.Body.Position = 0;

            _logger.LogInformation($"HttpLoggingMiddleware HTTP {request.Method} {request.Path} - Request Body: {requestBody}");

            // Перехват тела ответа
            var originalBodyStream = context.Response.Body;
            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var duration = DateTime.UtcNow - startTime;
            _logger.LogInformation($"HttpLoggingMiddleware HTTP {request.Method} {request.Path} - Response Body: {responseBodyText} - Duration: {duration.TotalMilliseconds} ms");

            // Возврат ответа клиенту
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
