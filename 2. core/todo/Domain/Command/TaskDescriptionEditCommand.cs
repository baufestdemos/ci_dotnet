using Core.Cross.Cqrs;
using Core.Cross.Types.General;
using Core.Todo.Domain.Tos;
using Core.Todo.Infra;
using Domain.Infra.Todo.Entity;
using Microsoft.Extensions.Logging;

namespace Core.Todo.Domain.Command;

public class TaskDescriptionEditCommand : ICommandHandler<TodoTaskDescriptionTo, ResultTo<TodoTaskDescriptionTo?>>
{
    protected readonly EFDemoDbContext _dbContext;
    protected readonly ILogger<TaskDescriptionEditCommand> _logger;

    public TaskDescriptionEditCommand(EFDemoDbContext dbContext,
        ILogger<TaskDescriptionEditCommand> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<ResultTo<TodoTaskDescriptionTo?>> Handle(CancellationToken cancellation, TodoTaskDescriptionTo commandIn)
    {
        ResultTo<TodoTaskDescriptionTo?> result = new();
        if (cancellation.IsCancellationRequested)
        {
            _logger.LogInformation("Request has been cancelled..");
            throw new OperationCanceledException();
        }

        TodoTask? task = await _dbContext.TodoTasks.FindAsync(commandIn.TaskId, cancellation);
        if (task is null)
        {
            result.Success = false;
        }
        else
        {
            if (task.Description is not null)
            {
                TaskDescriptionHistory history = new()
                {
                    BeforeDescription = task.Description,
                    TodoTask = task
                };

                _dbContext.TaskDescriptionHistories.Add(history);
            }

            task.Description = commandIn.Description;
            result.Value = commandIn;
        }
        return result;
    }
}