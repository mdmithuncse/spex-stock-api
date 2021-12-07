FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build

WORKDIR /app
COPY ["src/presentation/Spex.Stock.Api/Spex.Stock.Api.csproj", "./src/presentation/Spex.Stock.Api/"]
COPY ["src/core/Application/Application.csproj","./src/core/Application/"]
COPY ["src/core/Common/Common.csproj","./src/core/Common/"]
COPY ["src/core/DataModel/DataModel.csproj","./src/core/DataModel/"]
COPY ["src/core/DomainModel/DomainModel.csproj","./src/core/DomainModel/"]
COPY ["src/infrastructure/Persistence/Persistence.csproj","./src/infrastructure/Persistence/"]
COPY ["src/shared/Extensions/Extensions.csproj","./src/shared/Extensions/"]
COPY ["src/shared/Pagination/Pagination.csproj","./src/shared/Pagination/"]

RUN dotnet restore "./src/presentation/Spex.Stock.Api/Spex.Stock.Api.csproj"
COPY . .

FROM build AS publish
RUN dotnet publish "./src/presentation/Spex.Stock.Api/Spex.Stock.Api.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Spex.Stock.Api.dll"]