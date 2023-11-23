namespace Domain.Infra.Todo.Entity;

public class TodoTask
{
    public int Id { get; set; }
    public required string Subject { get; set; }
    public string? Description { get; set; }
    public bool Active { get; set; }
    public List<TaskDescriptionHistory>? TaskDescriptionHistories { get; set; }
}