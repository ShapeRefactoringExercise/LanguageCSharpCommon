namespace Shape.Lib.Types;

public interface Some<T>
{
    T Value { get; }
}

public class Maybe<T>
{
    private readonly SomeValue? _value;

    private Maybe(SomeValue? value)
    {
        _value = value;
    }

    private Maybe() : this(null) { }

    public bool IsSome => !IsNone;
    public bool IsNone => _value == null;

    public T Value
    {
        get
        {
            if (_value == null)
            {
                throw new ArgumentNullException("Value", "Value is null");
            }

            return _value.Value;
        }
    }

    private class SomeValue(T value): Some<T>
    {
        public T Value => value;
    }

    public static Maybe<T> Some(T value) => new Maybe<T>(new SomeValue(value));
    public static Maybe<T> None => new Maybe<T>();
}
