namespace DisposableHelperApp.Simple;

public class DisposableHelperSimple<T> : IDisposable
{
    private readonly Action _cleanup;

    public DisposableHelperSimple(T value, Action cleanup)
    {
        Value = value;
        _cleanup = cleanup;
    }

    public T Value { get; }

    public void Dispose()
    {
        _cleanup?.Invoke();
    }
}
