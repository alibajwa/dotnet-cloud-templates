namespace APIService.Services;

public interface IRequestOperationService
{
    Guid OperationId { get; }
}

public sealed class RequestOperationService : IRequestOperationService
{
    public Guid OperationId { get; } = Guid.NewGuid();
}
