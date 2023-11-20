namespace Domain.Infra.Todo.Entity;

public class TodoTask
{
    public int Id { get; set; }
    public required string Subject { get; set; }
    public bool Active { get; set; }
}