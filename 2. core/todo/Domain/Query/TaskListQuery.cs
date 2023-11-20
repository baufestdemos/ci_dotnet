using Core.Cross.Cqrs;
using Core.Todo.Domain.Contract;
using Core.Todo.Domain.Tos;

namespace Core.Todo.Domain.Query;

public class TaskListQuery : IQueryHandler<object?, IEnumerable<TodoTaskTo>>
{
    protected readonly IReadTaskRepo _repo;
    public TaskListQuery(IReadTaskRepo repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<TodoTaskTo>> Handle(CancellationToken cancellation, object? queryIn = null)
    {
        var allTask = await _repo.All(cancellation);

        return allTask
        .Select(e => new TodoTaskTo
        {
            Id = e.Id,
            Subject = e.Subject
        });
    }
}