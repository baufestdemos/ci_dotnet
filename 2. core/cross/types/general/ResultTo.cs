namespace Core.Cross.Types.General;

public record ResultTo<T>
{

    public ResultTo()
    {
        Success = true;
    }

    public T? Value { get; set; }
    public bool Success { get; set; }
}