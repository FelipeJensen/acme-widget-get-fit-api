namespace AcmeWidget.GetFit.Domain.ResultHandling;

public class Result
{
    public bool Success { get; private set; }
    public List<Error> Error { get; private set; }

    public Result()
    {
        Success = true;
    }

    public Result(Error error)
    {
        Success = false;
        Error = new List<Error> { error };
    }

    public Result(List<Error> error)
    {
        Success = false;
        Error = error;
    }
}

public class Result<T> : Result
{
    public T Value { get; private set; }

    public Result(T value)
    {
        Value = value;
    }

    public Result(Error error) : base(new List<Error> { error })
    {
    }
}