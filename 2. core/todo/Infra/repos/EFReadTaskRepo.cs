using Core.Todo.Domain.Contract;
using Domain.Infra.Todo.Entity;
using Microsoft.Extensions.Logging;

namespace Core.Todo.Infra.Repos;

public class EFReadTaskRepo : IReadTaskRepo
{
    private readonly EFDemoDbReadContext _dbContext;
    private readonly ILogger<EFReadTaskRepo> _logger;

    public EFReadTaskRepo(EFDemoDbReadContext dbContext,
        ILogger<EFReadTaskRepo> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public Task<IEnumerable<TodoTask>> All(CancellationToken cancellation)
    {
        if (cancellation.IsCancellationRequested)
        {
            _logger.LogInformation("Request has been cancelled..");
            throw new OperationCanceledException();
        }
        return Task.FromResult(_dbContext.TodoTasks.Where(e => e.Active).AsEnumerable());
    }
}
