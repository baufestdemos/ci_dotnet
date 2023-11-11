using Core.Cross.Cqrs;
using Core.Todo.Domain.Tos;
using Core.Todo.Infra;
using Microsoft.Extensions.Logging;

namespace Core.Todo.Domain.Query;

public class TaskListQuery : IQueryHandler<object, IEnumerable<TodoTaskTo>>
{
    protected readonly EFDemoDbReadContext _dbContext;
    protected readonly ILogger<TaskListQuery> _logger;

    public TaskListQuery(EFDemoDbReadContext dbContext,
        ILogger<TaskListQuery> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public Task<IEnumerable<TodoTaskTo>> Handle(CancellationToken cancellation, object queryIn = null)
    {
        if (cancellation.IsCancellationRequested)
        {
            _logger.LogInformation("Request has been cancelled..");
            throw new OperationCanceledException();
        }

        return Task.FromResult(_dbContext.TodoTasks.Where(e => e.Active)
        .AsEnumerable()
        .Select(e => new TodoTaskTo
        {
            Id = e.Id,
            Subject = e.Subject
        }));
    }
}