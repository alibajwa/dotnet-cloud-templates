# API Service

This template is a small ASP.NET Core Web API intended to run as an AWS-ready service. It can run locally as a self-hosted ASP.NET Core API, in a container, or behind AWS deployment targets. The application code stays deployment-neutral, while AWS deployment-specific files live under `deploy/`.

## Layout

```text
api-service/
  APIService.csproj
  Program.cs
  appsettings.json
  appsettings.Debug.json
  appsettings.Release.json
  Controllers/
  Services/
  deploy/
    container/
      Dockerfile
    lambda/
      aws-lambda-tools-defaults.json
```

## Run Locally

```powershell
dotnet run --project APIService.csproj
```

When run locally, the API is self-hosted by the standard ASP.NET Core web host. Local URLs are controlled by `Properties/launchSettings.json` during development or by the `ASPNETCORE_URLS` environment variable when running outside the debugger.

Call the configuration example endpoint:

```powershell
curl http://localhost:49342/api/math/configuration
```

The endpoint reads `Service:DisplayName` from `appsettings.json`. The same value can be overridden with the `Service__DisplayName` environment variable in container or cloud deployments.

Call the dependency injection example endpoint:

```powershell
curl http://localhost:49342/api/dependencies/lifetimes
```

The endpoint demonstrates:

- Singleton dependency: one service instance is reused for the lifetime of the running application process.
- Transient dependency: a new service instance is created each time the dependency is requested.
- Static helper: stateless utility code is called without dependency injection.

## Dependency Lifetime Guidance

Singleton dependencies work well for expensive, thread-safe clients and application-wide helpers, such as AWS SDK clients, HTTP clients created through `IHttpClientFactory`, cached metadata providers, and configuration-backed services. In Lambda, singletons can be reused across warm invocations in the same execution environment. In ECS and App Runner, singletons live for the lifetime of each container instance. Do not store request-specific or user-specific state in singleton services.

Transient dependencies work well for lightweight, stateless operations where a fresh instance is useful, such as small calculators, mappers, validators, and command-style services. They behave consistently across Lambda, ECS, and App Runner because each request receives newly requested instances. Avoid transient services for expensive clients that should be reused.

Static helpers work best for pure, stateless utility functions that do not need configuration, logging, dependency injection, disposal, or test-time substitution. They are deployment-neutral across Lambda, ECS, and App Runner, but should not hold mutable global state because all three deployment models can reuse a running process for multiple requests.

Build-specific configuration files are also included:

- `appsettings.Debug.json` is loaded for Debug builds.
- `appsettings.Release.json` is loaded for Release builds.

Switch between them by changing the build configuration:

```powershell
dotnet run --project APIService.csproj -c Debug
dotnet run --project APIService.csproj -c Release
```

## Build Container Image

Run this command from the `aws/api-service` directory:

```powershell
docker build -f deploy/container/Dockerfile -t api-service .
```

The same image can be used as a starting point for ECS or AWS App Runner deployments.

## Lambda Defaults

Lambda deployment defaults are stored in `deploy/lambda/aws-lambda-tools-defaults.json`. The application enables Lambda HTTP API hosting in `Program.cs` while still running normally as an ASP.NET Core app for container-based deployments.
