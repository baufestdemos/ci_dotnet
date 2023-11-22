namespace Core.Todo.Domain.Tos;

public record TodoTaskDescriptionTo
{
    public long Id { get; set; }
    public int TaskId { get; set; }
    public string? Description { get; set; }
}