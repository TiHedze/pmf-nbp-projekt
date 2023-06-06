#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Pmf.PublicationTracker.Startup/Pmf.PublicationTracker.Startup.csproj", "src/Pmf.PublicationTracker.Startup/"]
COPY ["src/Application/Pmf.PublicationTracker.Application.Contracts/Pmf.PublicationTracker.Application.Contracts.csproj", "src/Application/Pmf.PublicationTracker.Application.Contracts/"]
COPY ["src/Domain/Pmf.PublicationTracker.Domain/Pmf.PublicationTracker.Domain.csproj", "src/Domain/Pmf.PublicationTracker.Domain/"]
COPY ["src/Application/Pmf.PublicationTracker.Application/Pmf.PublicationTracker.Application.csproj", "src/Application/Pmf.PublicationTracker.Application/"]
COPY ["src/Infrastructure/Pmf.PublicationTracker.Infrastructure.Db.Neo4j/Pmf.PublicationTracker.Infrastructure.Db.Neo4j.csproj", "src/Infrastructure/Pmf.PublicationTracker.Infrastructure.Db.Neo4j/"]
COPY ["src/Infrastructure/Pmf.PublicationTracker.Infrastructure.Db.Postgres/Pmf.PublicationTracker.Infrastructure.Db.Postgres.csproj", "src/Infrastructure/Pmf.PublicationTracker.Infrastructure.Db.Postgres/"]
COPY ["src/Presentation/Pmf.PublicationTracker.Presentation.Api/Pmf.PublicationTracker.Presentation.Api.csproj", "src/Presentation/Pmf.PublicationTracker.Presentation.Api/"]
RUN dotnet restore "src/Pmf.PublicationTracker.Startup/Pmf.PublicationTracker.Startup.csproj"
COPY . .
WORKDIR "/src/src/Pmf.PublicationTracker.Startup"
RUN dotnet build "Pmf.PublicationTracker.Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pmf.PublicationTracker.Startup.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pmf.PublicationTracker.Startup.dll"]