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

    protected bool Equals(Lookup<T> other)
    {
        return EqualityComparer<T>.Default.Equals(Id, other.Id) && Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        return Equals((Lookup<T>)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Value);
    }
}