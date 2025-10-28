# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.packages.props", "."]
COPY ["Vito.Transverse.Identity.Presentation.Api/Directory.Build.props", "Vito.Transverse.Identity.Presentation.Api/"]
COPY ["Vito.Transverse.Identity.Presentation.Api/Vito.Transverse.Identity.Presentation.Api.csproj", "Vito.Transverse.Identity.Presentation.Api/"]
COPY [" Vito.Transverse.Identity.Application/ Vito.Transverse.Identity.Application.csproj", " Vito.Transverse.Identity.Application/"]
COPY [" Vito.Transverse.Identity.Infrastructure/ Vito.Transverse.Identity.Infrastructure.csproj", " Vito.Transverse.Identity.Infrastructure/"]
COPY ["Vito.Transverse.Identity.Domain/Vito.Transverse.Identity.Domain.csproj", "Vito.Transverse.Identity.Domain/"]
RUN dotnet restore "./Vito.Transverse.Identity.Presentation.Api/Vito.Transverse.Identity.Presentation.Api.csproj"
COPY . .
WORKDIR "/src/Vito.Transverse.Identity.Presentation.Api"
RUN dotnet build "./Vito.Transverse.Identity.Presentation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Vito.Transverse.Identity.Presentation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vito.Transverse.Identity.Presentation.Api.dll"]