FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["myfood.Api/Api/Api.csproj", "myfood.Api/Api/"]
COPY ["myfood.Shared/Shared/Shared.csproj", "myfood.Shared/Shared/"]
COPY ["myfood.Shared/Shared.Contract/Shared.Contract.csproj", "myfood.Shared/Shared.Contract/"]
COPY ["myfood.Shared/myfood.Messages/myfood.Messages.csproj", "myfood.Shared/myfood.Messages/"]
COPY ["myfood.Modules/Identity/Identity.infrastructure/Identity.infrastructure.csproj", "myfood.Modules/Identity/Identity.infrastructure/"]
COPY ["myfood.Modules/Identity/Identity.Domain/Identity.Domain.csproj", "myfood.Modules/Identity/Identity.Domain/"]
COPY ["myfood.Modules/Identity/Identity.Application/Identity.Application.csproj", "myfood.Modules/Identity/Identity.Application/"]
COPY ["myfood.Modules/Common/Common/Common.csproj", "myfood.Modules/Common/Common/"]
COPY ["myfood.Modules/Notification/Notification/Notification.csproj", "myfood.Modules/Notification/Notification/"]
RUN dotnet restore "myfood.Api/Api/Api.csproj"
COPY . .
WORKDIR "/src/myfood.Api/Api"
RUN dotnet build "./Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
