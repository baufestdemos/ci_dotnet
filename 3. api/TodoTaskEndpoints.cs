using Core.Cross.Cqrs;
using Core.Cross.Transactions;
using Core.Cross.Types.General;
using Core.Todo.Domain.Tos;

namespace Api;
public static class TodoTaskEndpointExtension
{

    public static WebApplication UseTodoTaskEndpoints(this WebApplication app)
    {
        string groupTag = "Todo";
        app.MapGet("/api/task", async (CancellationToken cancellationToken,
            IQueryHandler<object, IEnumerable<TodoTaskTo>> listQuery) =>
        {
            return await listQuery.Handle(cancellationToken);
        }).WithTags(groupTag)
        .WithOpenApi(options =>
        {
            options.Summary = "List task";
            options.Description = "Return all todo tasks";
            return options;
        });

        app.MapPost("/api/task", async (CancellationToken cancellationToken,
            ICommandHandler<TodoTaskTo, ResultTo<TodoTaskTo>> addCommand, ITransactor transactor,
            TodoTaskTo task) =>
        {
            return await transactor.BeginAsync(async () =>
            {
                return await addCommand.Handle(cancellationToken, task);
            });
        }).WithTags(groupTag)
        .WithOpenApi(options =>
        {
            options.Summary = "Add task";
            options.Description = "Add new task";
            return options;
        });

        app.MapDelete("/api/task", async (CancellationToken cancellationToken,
            ICommandHandler<int, ResultTo<TodoTaskTo?>> removeCommand, ITransactor transactor,
            int id) =>
        {
            return await transactor.BeginAsync(async () =>
            {
                return await removeCommand.Handle(cancellationToken, id);
            });
        }).WithTags(groupTag)
        .WithOpenApi(options =>
        {
            options.Summary = "Remove task";
            options.Description = "Remove existing task";
            return options;
        });

        app.MapGet("/api/test", (CancellationToken cancellationToken, IConfiguration configuration) =>
        {
            return Results.Json(new { value = configuration["Test:Var"] });
        }).WithTags(groupTag)
        .WithOpenApi(options =>
        {
            options.Summary = "Remove task";
            options.Description = "Remove existing task";
            return options;
        });

        return app;
    }
}