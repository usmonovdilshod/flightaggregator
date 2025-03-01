# Build Stage: Build and publish the Blazor WebAssembly app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
# Copy the project files and restore dependencies
COPY ["FlightAggregatorUI/FlightAggregatorUI.csproj", "FlightAggregatorUI/"]
COPY ["FlightAggregatorShared/FlightAggregatorShared.csproj", "FlightAggregatorShared/"]
RUN dotnet restore "FlightAggregatorUI/FlightAggregatorUI.csproj"
# Copy all source code
COPY . .
WORKDIR "/src/FlightAggregatorUI"
# Publish the app in Release configuration with Production settings
RUN dotnet publish "FlightAggregatorUI.csproj" -c Release -o /app/publish /p:EnvironmentName=Production

# Final Stage: Serve the static files using Nginx
FROM nginx:alpine AS final
# Copy the custom nginx configuration into the image
COPY FlightAggregatorUI/nginx.conf /etc/nginx/nginx.conf
# Copy published output (the static files) to Nginx's html folder
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
