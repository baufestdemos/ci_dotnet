namespace Domain.Infra.Todo.Entity;

public class TaskDescriptionHistory
{
    public long Id { get; set; }
    public int TaskId { get; set; }
    public required TodoTask TodoTask { get; set; }
    public required string BeforeDescription { get; set; }
    public bool Active { get; set; }
}