# dotnet-cloud-templates

Cloud-ready .NET project templates for building, containerizing, and deploying services across cloud platforms.

This repository collects practical .NET templates that can be used as starting points for production-style cloud applications. The templates favor simple application code, clear deployment boundaries, and layouts that can grow as more cloud targets are added.

## Templates

### AWS API Service

Location: `aws/api-service`

A small ASP.NET Core Web API template for an AWS-ready microservice. The application can run locally as a self-hosted ASP.NET Core API, in a container, or behind AWS deployment targets such as Lambda, ECS, and App Runner with minimal application changes.

Current deployment assets:

- `deploy/container/Dockerfile` for container-based deployments.
- `deploy/lambda/aws-lambda-tools-defaults.json` for AWS Lambda defaults.

## Repository Layout

```text
dotnet-cloud-templates/
  aws/
    aws.slnx
    api-service/
      APIService.csproj
      Program.cs
      Controllers/
      deploy/
        container/
        lambda/
```

## Getting Started

Build the AWS templates solution:

```powershell
dotnet build aws/aws.slnx
```

Run the API service locally as a self-hosted API:

```powershell
dotnet run --project aws/api-service/APIService.csproj
```

When run locally, the service uses the standard ASP.NET Core web host and listens on the URLs configured by `Properties/launchSettings.json` or `ASPNETCORE_URLS`.

Build the API service container image:

```powershell
docker build -f aws/api-service/deploy/container/Dockerfile -t api-service aws/api-service
```

## Goals

- Provide small, focused .NET templates for cloud application patterns.
- Keep application code separate from deployment-specific configuration.
- Make each template easy to run locally before deploying to cloud infrastructure.
- Grow toward multiple cloud providers and deployment targets over time.
