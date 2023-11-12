using Core.Cross.Cqrs;
using Core.Cross.Types.General;
using Core.Todo.Domain.Tos;
using Core.Todo.Infra;
using Domain.Infra.Todo.Entity;
using Microsoft.Extensions.Logging;

namespace Core.Todo.Domain.Command;

public class TaskRemoveCommand : ICommandHandler<int, ResultTo<TodoTaskTo?>>
{
    protected readonly EFDemoDbContext _dbContext;

    protected readonly ILogger<TaskAddCommand> _logger;

    public TaskRemoveCommand(EFDemoDbContext dbContext,
        ILogger<TaskAddCommand> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }


    public async Task<ResultTo<TodoTaskTo?>> Handle(CancellationToken cancellation, int commandIn)
    {
        ResultTo<TodoTaskTo?> result = new();
        if (cancellation.IsCancellationRequested)
        {
            _logger.LogInformation("Request has been cancelled..");
            throw new OperationCanceledException();
        }

        TodoTask? task = await _dbContext.TodoTasks.FindAsync(commandIn, cancellation);
        if (task is null)
        {
            result.Success = false;
        }
        else
        {
            task.Active = false;
        }
        return result;
    }
}