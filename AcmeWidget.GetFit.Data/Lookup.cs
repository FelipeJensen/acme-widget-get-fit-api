namespace AcmeWidget.GetFit.Data;

public class Lookup<T>
{
    public T Id { get; }
    public string Value { get; }

    public Lookup(T id, string value)
    {
        Id = id;
        Value = value;
    }
}