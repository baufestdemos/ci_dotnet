namespace Domain.Infra.Todo.Entity;

public class TodoTask
{
    public int Id { get; set; }
    public string Subject { get; set; }
    public bool Active { get; set; }
}