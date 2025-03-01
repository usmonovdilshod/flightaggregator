# Use the official ASP.NET runtime as the base image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 7104
EXPOSE 7100

# Build stage: restore, build, and publish the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["SkyLinkApi/SkyLinkApi.csproj", "SkyLinkApi/"]
RUN dotnet restore "SkyLinkApi/SkyLinkApi.csproj"
COPY . .
WORKDIR "/src/SkyLinkApi"
RUN dotnet build "SkyLinkApi.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "SkyLinkApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage: copy the published output and define the entry point
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkyLinkApi.dll"]
