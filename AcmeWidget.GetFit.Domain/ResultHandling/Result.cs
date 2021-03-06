namespace AcmeWidget.GetFit.Domain.ResultHandling;

public class Result
{
    public bool Success { get; private set; }
    public List<Error> Errors { get; private set; } = new();

    public Result()
    {
        Success = true;
    }

    public Result(Error error)
    {
        Success = false;
        Errors = new List<Error> { error };
    }

    public Result(List<Error> errors)
    {
        Success = false;
        Errors = errors;
    }
}

public class Result<T> : Result
{
    public T? Value { get; private set; }

    public Result(T value)
    {
        Value = value;
    }

    public Result(Error error) : base(new List<Error> { error })
    {
    }

    public Result(List<Error> validation) : base(validation)
    {
    }
}