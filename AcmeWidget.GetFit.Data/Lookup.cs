namespace AcmeWidget.GetFit.Application.Activities.Dtos;

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