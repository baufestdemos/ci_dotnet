using Core.Todo.Domain.Command;
using Core.Todo.Domain.Tos;
using Core.Todo.Infra;
using CoreTest.Todo.Mock;
using Domain.Infra.Todo.Entity;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreTest.Todo.Domain.Command;


public class TaskAddCommandTest
{
    [Fact]
    public async Task Handle()
    {
        var data = TodoMocks.GetFakeTodoTaskData();

        var input = new TodoTaskTo
        {
            Subject = "Task 3"
        };

        (Mock<EFDemoDbContext> mockContext, ILogger<TaskAddCommand> logger,
        CancellationToken token) = TodoMocks.GetMockForCommand<TaskAddCommand>(data);

        mockContext.Setup(m => m.TodoTasks.AddAsync(It.IsAny<TodoTask>(),
        It.IsAny<CancellationToken>())).Callback<TodoTask, CancellationToken>((s, token) =>
        {
            data.Add(s);
        });

        var context = mockContext.Object;

        var taskAddCommand = new TaskAddCommand(context, logger);

        var result = await taskAddCommand.Handle(token, input);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Contains(data, item => item.Subject.Equals("Task 3"));
    }
}