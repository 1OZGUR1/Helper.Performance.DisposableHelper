namespace DisposableHelperApp.Simple;

public class DisposableHelperSimple<T> : IDisposable
{
    public T Value { get; }
    private Action _cleanup;

    public DisposableHelperSimple(T value, Action cleanup)
    {
        Value = value;
        _cleanup = cleanup;
    }

    public void Dispose() => _cleanup?.Invoke();
}
