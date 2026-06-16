using APIService.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIService.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class DependenciesController(
    IApplicationInstanceService applicationInstance,
    IRequestOperationService firstOperation,
    IRequestOperationService secondOperation) : ControllerBase
{
    [HttpGet("lifetimes")]
    public ActionResult<DependencyLifetimeResult> Lifetimes()
    {
        var generatedAtUtc = StaticResponseFormatter.GetGeneratedAtUtc();

        return Ok(new DependencyLifetimeResult(
            new SingletonDependencyResult(
                applicationInstance.InstanceId,
                applicationInstance.CreatedAtUtc),
            new TransientDependencyResult(
                firstOperation.OperationId,
                secondOperation.OperationId,
                firstOperation.OperationId == secondOperation.OperationId),
            new StaticDependencyResult(
                generatedAtUtc,
                StaticResponseFormatter.FormatMessage("Stateless static helper"))));
    }
}

public sealed record DependencyLifetimeResult(
    SingletonDependencyResult Singleton,
    TransientDependencyResult Transient,
    StaticDependencyResult Static);

public sealed record SingletonDependencyResult(Guid InstanceId, DateTimeOffset CreatedAtUtc);

public sealed record TransientDependencyResult(Guid FirstOperationId, Guid SecondOperationId, bool SameInstance);

public sealed record StaticDependencyResult(DateTimeOffset GeneratedAtUtc, string Message);
