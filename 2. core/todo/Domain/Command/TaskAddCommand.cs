using Core.Cross.Cqrs;
using Core.Cross.Types.General;
using Core.Todo.Domain.Tos;
using Core.Todo.Infra;
using Domain.Infra.Todo.Entity;
using Microsoft.Extensions.Logging;

namespace Core.Todo.Domain.Command;

public class TaskAddCommand : ICommandHandler<TodoTaskTo, ResultTo<TodoTaskTo>>
{
    protected readonly EFDemoDbContext _dbContext;
    protected readonly ILogger<TaskAddCommand> _logger;

    public TaskAddCommand(EFDemoDbContext dbContext,
        ILogger<TaskAddCommand> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<ResultTo<TodoTaskTo>> Handle(CancellationToken cancellation, TodoTaskTo commandIn)
    {
        ResultTo<TodoTaskTo> result = new();
        if (cancellation.IsCancellationRequested)
        {
            _logger.LogInformation("Request has been cancelled..");
            throw new OperationCanceledException();
        }

        await _dbContext.TodoTasks.AddAsync(new TodoTask
        {
            Subject = commandIn.Subject!
        }, cancellation);

        result.Value = commandIn;
        return result;
    }
}