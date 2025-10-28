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
COPY ["src/Web/Notification/Notification.csproj", "src/Web/Notification/"]
COPY ["src/BusinessModel/Application/Application.csproj", "src/BusinessModel/Application/"]
COPY ["src/BusinessModel/Entity/Entity.csproj", "src/BusinessModel/Entity/"]
COPY ["src/Infrastructure/EntityCorePostgres/EntityCorePostgres.csproj", "src/Infrastructure/EntityCorePostgres/"]
COPY ["src/Infrastructure/OtlpInfra/OtlpInfra.csproj", "src/Infrastructure/OtlpInfra/"]
COPY ["src/Infrastructure/RabbitMQ/RabbitMQNeuro.csproj", "src/Infrastructure/RabbitMQ/"]
RUN dotnet restore "./src/Web/Notification/Notification.csproj"
COPY . .
WORKDIR "/src/src/Web/Notification"
RUN dotnet build "./Notification.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Notification.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.dll"]