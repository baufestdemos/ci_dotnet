using Domain.Infra.Todo.Entity;

namespace Core.Todo.Domain.Contract;

public interface IReadTaskRepo
{
    Task<IEnumerable<TodoTask>> All(CancellationToken cancellation);
}