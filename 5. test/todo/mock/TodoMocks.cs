using Core.Todo.Domain.Contract;
using Core.Todo.Infra;
using Domain.Infra.Todo.Entity;
using Microsoft.Extensions.Logging;
using MockQueryable.Moq;
using Moq;

namespace CoreTest.Todo.Mock;

public static class TodoMocks
{
    public static List<TodoTask> GetFakeTodoTaskData()
    {
        return new List<TodoTask>()
        {
            new() {
                Id = 1,
                Subject="Task 1",
                Active = true
            },
            new() {
                Id = 2,
                Subject="Task 2",
                Active = true
            }
        };
    }
    public static Tuple<IReadTaskRepo, CancellationToken> GetMockForQuery()
    {
        var cts = new CancellationTokenSource(1000);
        var token = cts.Token;

        var dataMock = GetFakeTodoTaskData().BuildMock();
        var demoDbContextMock = new Mock<IReadTaskRepo>();
        demoDbContextMock.Setup(x => x.All(token))
        .ReturnsAsync(dataMock);

        return Tuple.Create(demoDbContextMock.Object, token);
    }

    public static Tuple<Mock<EFDemoDbContext>, ILogger<T>, CancellationToken> GetMockForCommand<T>(IEnumerable<TodoTask> data)
    {
        var cts = new CancellationTokenSource(1000);
        var token = cts.Token;

        var dataMock = data.BuildMock().BuildMockDbSet();
        var demoDbContextMock = new Mock<EFDemoDbContext>();
        demoDbContextMock.Setup(x => x.TodoTasks)
        .Returns(dataMock.Object);

        var logger = new Mock<ILogger<T>>();

        return Tuple.Create(demoDbContextMock, logger.Object, cts.Token);
    }
}