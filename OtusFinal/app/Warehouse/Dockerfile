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
COPY ["Warehouse/src/Web/Warehouse/Warehouse.csproj", "Warehouse/src/Web/Warehouse/"]
COPY ["Warehouse/src/Application/Application/Application.csproj", "Warehouse/src/Application/Application/"]
COPY ["Warehouse/src/Application/Domain/Domain.csproj", "Warehouse/src/Application/Domain/"]
COPY ["Warehouse/src/Infrastructure/EntityCorePostgres/EntityCorePostgres.csproj", "Warehouse/src/Infrastructure/EntityCorePostgres/"]
COPY ["Warehouse/src/Infrastructure/Grps/Grps.csproj", "Warehouse/src/Infrastructure/Grps/"]
COPY ["Warehouse/src/Infrastructure/OtlpInfra/OtlpInfra.csproj", "Warehouse/src/Infrastructure/OtlpInfra/"]
RUN dotnet restore "./Warehouse/src/Web/Warehouse/Warehouse.csproj"
COPY . .
WORKDIR "/src/Warehouse/src/Web/Warehouse"
RUN dotnet build "./Warehouse.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Warehouse.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Warehouse.dll"]