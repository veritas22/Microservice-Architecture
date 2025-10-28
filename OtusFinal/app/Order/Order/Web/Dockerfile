# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Order/Web/Order.csproj", "Order/Web/"]
COPY ["Order/Infrastructure/EntityCorePostgres/EntityCorePostgres.csproj", "Order/Infrastructure/EntityCorePostgres/"]
COPY ["Order/BusinessModel/Application/Application.csproj", "Order/BusinessModel/Application/"]
COPY ["Order/BusinessModel/Domen/Domen.csproj", "Order/BusinessModel/Domen/"]
COPY ["Order/Infrastructure/Grps/Grps.csproj", "Order/Infrastructure/Grps/"]
COPY ["Order/Infrastructure/OtlpInfra/OtlpInfra.csproj", "Order/Infrastructure/OtlpInfra/"]
COPY ["Order/Infrastructure/RabbitMQ/RabbitMQNeuro.csproj", "Order/Infrastructure/RabbitMQ/"]
COPY ["Order/Infrastructure/Redis/Redis.csproj", "Order/Infrastructure/Redis/"]
RUN dotnet restore "./Order/Web/Order.csproj"
COPY . .
WORKDIR "/src/Order/Web"
RUN dotnet build "./Order.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Order.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.dll"]