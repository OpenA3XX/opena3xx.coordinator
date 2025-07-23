# OpenA3XX.Peripheral.WebApi Docker Setup

This document explains how to build and run the OpenA3XX.Peripheral.WebApi using Docker.

## Prerequisites

- Docker Desktop or Docker Engine installed
- Docker Compose installed

## Quick Start

### Using Docker Compose (Recommended)

From the repository root, run the entire stack:

```bash
docker-compose up --build
```

This will start:
- **WebApi** on `http://localhost:5000`
- **RabbitMQ** on `localhost:5672` (Management UI: `http://localhost:15672`)
- **Seq** (Logging) on `http://localhost:8080`

### Building Individual Container

To build just the WebApi container:

```bash
# From repository root
docker build -f src/OpenA3XX.Peripheral.WebApi/Dockerfile -t opena3xx-webapi .
```

To run the individual container:

```bash
docker run -p 5000:8080 \
  -v opena3xx_data:/app/data \
  -e ASPNETCORE_ENVIRONMENT=Docker \
  opena3xx-webapi
```

## Configuration

### Environment-Specific Settings

The container uses different configuration files based on the environment:

- **Development**: `appsettings.Development.json`
- **Docker**: `appsettings.Docker.json` (containerized services)
- **Production**: `appsettings.json`

### Key Docker Configuration Changes

The `appsettings.Docker.json` file configures:

- **Database**: SQLite database stored in `/app/data/` volume
- **RabbitMQ**: Connected to `rabbitmq` service
- **Seq Logging**: Connected to `seq` service
- **Flight Simulator**: Connected via `host.docker.internal`
- **CORS**: Includes Docker host origins

### Environment Variables

Set these environment variables when running the container:

```bash
ASPNETCORE_ENVIRONMENT=Docker  # Uses appsettings.Docker.json
ASPNETCORE_URLS=http://+:8080  # Internal container port
```

## Volumes

The container uses the following volumes:

- `/app/data`: SQLite database storage
- This volume is automatically created as `webapi_data` in docker-compose

## Ports

- **Container Port**: 8080
- **Host Port**: 5000 (configured in docker-compose.yml)

## Health Check

The container includes a health check that verifies the application is responding on the `/health` endpoint.

## Security

- Runs as non-root user (`appuser`)
- Uses official Microsoft .NET base images
- Minimal attack surface with multi-stage build

## Development

### Building for Development

```bash
# Build with development configuration
docker build -f src/OpenA3XX.Peripheral.WebApi/Dockerfile \
  --build-arg ASPNETCORE_ENVIRONMENT=Development \
  -t opena3xx-webapi:dev .
```

### Debugging

To run with debug logging:

```bash
docker run -p 5000:8080 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e Logging__LogLevel__Default=Debug \
  opena3xx-webapi
```

## Services Integration

### RabbitMQ Connection

The WebApi connects to RabbitMQ for messaging. Ensure RabbitMQ is running:

```bash
# Start just RabbitMQ
docker-compose up rabbitmq
```

### Seq Logging

Structured logs are sent to Seq for analysis:

```bash
# Start just Seq
docker-compose up seq
```

### Flight Simulator Connection

The WebApi connects to Flight Simulator running on the host machine using `host.docker.internal`.

## Troubleshooting

### Common Issues

1. **Database Permission Errors**
   - Ensure the `/app/data` volume has correct permissions
   - The container runs as user `appuser` (non-root)

2. **RabbitMQ Connection Failed**
   - Verify RabbitMQ container is running: `docker-compose ps`
   - Check network connectivity: `docker network ls`

3. **Flight Simulator Connection**
   - Ensure Flight Simulator is running on the host
   - Verify `host.docker.internal` resolves correctly

### Logs

View container logs:

```bash
# All services
docker-compose logs

# WebApi only
docker-compose logs webapi

# Follow logs in real-time
docker-compose logs -f webapi
```

### Container Shell Access

Access the running container for debugging:

```bash
docker exec -it opena3xx.coordinator.webapi /bin/bash
```

## Production Considerations

For production deployment:

1. Use specific image tags instead of `latest`
2. Configure proper secrets management
3. Set up SSL/TLS termination
4. Configure proper logging levels
5. Set up monitoring and alerting
6. Use production-grade database
7. Configure backup strategies for volumes 