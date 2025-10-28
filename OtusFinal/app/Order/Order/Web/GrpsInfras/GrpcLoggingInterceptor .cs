using Grpc.Core;
using Grpc.Core.Interceptors;
using static Grpc.Core.Interceptors.Interceptor;

namespace Order.Grps
{
    public class GrpcLoggingInterceptor : Interceptor
    {
        private readonly ILogger<GrpcLoggingInterceptor> _logger;

        public GrpcLoggingInterceptor(ILogger<GrpcLoggingInterceptor> logger)
        {
            _logger = logger;
        }


        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            _logger.LogInformation("Проверка Starting call. Type/Method: {Type} / {Method}",
                context.Method.Type, context.Method.Name);
            return continuation(request, context);
        }
    }
}
