using Core.Todo.Domain.Command;
using Core.Todo.Domain.Tos;
using Core.Todo.Infra;
using CoreTest.Todo.Mock;
using Domain.Infra.Todo.Entity;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreTest.Todo.Domain.Command;


public class TaskDescriptionEditCommandTest
{
    [Fact]
    public async Task Handle()
    {
        TodoTaskDescriptionTo inputDescriptionTaskTo = new()
        {
            TaskId = 2,
            Description = "Tarea numero 2"
        };

        var data = TodoMocks.GetFakeTodoTaskData();

        (Mock<EFDemoDbContext> mockContext, ILogger<TaskDescriptionEditCommand> logger,
        CancellationToken token) = TodoMocks.GetMockForCommand<TaskDescriptionEditCommand>(data);

        mockContext.Setup(m => m.TodoTasks.FindAsync(It.IsAny<object?[]?>()))
        .Returns((object?[]? r) =>
        {
            return new ValueTask<TodoTask?>(data.FirstOrDefault(b => b.Id == (int)r![0]!));
        });

        var context = mockContext.Object;

        var taskAddCommand = new TaskDescriptionEditCommand(context, logger);

        var result = await taskAddCommand.Handle(token, inputDescriptionTaskTo);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Contains(data, item => item.Description is not null && item.Description.Equals("Tarea numero 2"));
    }
}