namespace Core.Todo.Domain.Tos;

public record TodoTaskTo
{
    public int Id { get; set; }
    public string? Subject { get; set; }
    public string? Description { get; set; }
}