using Core.Todo.Domain.Contract;
using Core.Todo.Domain.Query;
using CoreTest.Todo.Mock;

namespace CoreTest.Todo.Domain.Query;

public class TaskListQueryTest
{
    [Fact]
    public async Task Handle()
    {
        (IReadTaskRepo repo, CancellationToken token) = TodoMocks.GetMockForQuery();

        var taskListQuery = new TaskListQuery(repo);

        var todoTaskList = await taskListQuery.Handle(token);

        Assert.NotNull(todoTaskList);
        Assert.Equal(3, todoTaskList.Count());
    }
}