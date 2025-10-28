using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Order.Swagger
{
    public class AddCustomHeaderParameter
        : IOperationFilter
    {
        public void Apply(
            OpenApiOperation operation,
            OperationFilterContext context)
        {
            // Проверить, что параметры не null
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            // Получить имя контроллера
            var controllerName = context.MethodInfo.DeclaringType.Name;

            // Применять фильтр только если контроллер нужный, например, "MyController"
            if (controllerName == "OrderController")
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Idempotency-Key",
                    In = ParameterLocation.Header,
                    Description = "Idempotency description",
                    Required = true,
                });
            }
        }
    }
}
