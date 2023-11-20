using Core.Todo.Domain.Command;
using Core.Todo.Infra;
using CoreTest.Todo.Mock;
using Domain.Infra.Todo.Entity;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreTest.Todo.Domain.Command;


public class TaskRemoveCommandTest
{
    [Fact]
    public async Task Handle()
    {
        var data = TodoMocks.GetFakeTodoTaskData();

        (Mock<EFDemoDbContext> mockContext, ILogger<TaskRemoveCommand> logger,
        CancellationToken token) = TodoMocks.GetMockForCommand<TaskRemoveCommand>(data);

        mockContext.Setup(m => m.TodoTasks.FindAsync(It.IsAny<object?[]?>()))
        .Returns((object?[]? r) =>
        {
            return new ValueTask<TodoTask?>(data.FirstOrDefault(b => b.Id == (int)r![0]!));
        });

        var context = mockContext.Object;

        var taskAddCommand = new TaskRemoveCommand(context, logger);

        var result = await taskAddCommand.Handle(token, 1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.DoesNotContain(data, item => item.Id == 1 && item.Active);
    }
}