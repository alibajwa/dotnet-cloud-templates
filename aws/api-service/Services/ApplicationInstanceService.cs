namespace APIService.Services;

public interface IApplicationInstanceService
{
    Guid InstanceId { get; }

    DateTimeOffset CreatedAtUtc { get; }
}

public sealed class ApplicationInstanceService : IApplicationInstanceService
{
    public Guid InstanceId { get; } = Guid.NewGuid();

    public DateTimeOffset CreatedAtUtc { get; } = DateTimeOffset.UtcNow;
}
