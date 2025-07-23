# Use the official .NET 8.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file (if building from solution root)
COPY ["OpenA3XX.Coordinator.sln", "./"]

# Copy project files for dependency restoration
COPY ["src/OpenA3XX.Peripheral.WebApi/OpenA3XX.Peripheral.WebApi.csproj", "src/OpenA3XX.Peripheral.WebApi/"]
COPY ["src/OpenA3XX.Core/OpenA3XX.Core.csproj", "src/OpenA3XX.Core/"]

# Restore dependencies
RUN dotnet restore "src/OpenA3XX.Peripheral.WebApi/OpenA3XX.Peripheral.WebApi.csproj"

# Copy all source code
COPY . .

# Build the application
WORKDIR "/src/src/OpenA3XX.Peripheral.WebApi"
RUN dotnet build "OpenA3XX.Peripheral.WebApi.csproj" -c Release -o /app/build

# Publish the application
RUN dotnet publish "OpenA3XX.Peripheral.WebApi.csproj" -c Release -o /app/publish --no-restore

# Use the official .NET 8.0 runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Create directory for SQLite database
RUN mkdir -p /app/data

# Copy the published application
COPY --from=build /app/publish .

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

# Create a non-root user for security
RUN groupadd -r appgroup && useradd -r -g appgroup appuser
RUN chown -R appuser:appgroup /app
USER appuser

# Expose the port
EXPOSE 8080

# Health check using dotnet tool
HEALTHCHECK --interval=30s --timeout=10s --start-period=60s --retries=3 \
    CMD dotnet --version || exit 1

# Set the entry point
ENTRYPOINT ["dotnet", "OpenA3XX.Peripheral.WebApi.dll"] 