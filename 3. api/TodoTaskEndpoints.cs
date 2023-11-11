using Core.Cross.Cqrs;
using Core.Todo.Domain.Tos;
using Microsoft.AspNetCore.Mvc;

namespace Api;
public static class TodoTaskEndpointExtension
{

    public static WebApplication UseTodoTaskEndpoints(this WebApplication app)
    {
        app.MapGet("/api/task/all", async (CancellationToken cancellationToken,
        [FromServices] IQueryHandler<object, IEnumerable<TodoTaskTo>> listQuery) =>
        {
            return await listQuery.Handle(cancellationToken);
        });
        return app;
    }
}