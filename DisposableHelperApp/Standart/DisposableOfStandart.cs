namespace DisposableHelperApp.Standart;

public class DisposableOfStandart<T>
    where T : IDisposable
{
    internal DisposableOfStandart(Func<T> factory)
    {
        Factory = factory;
    }

    private Func<T> Factory { get; }

    public void Use(Action<T> action)
    {
        using var target = Factory();
        action(target);
    }

    public TResult Use<TResult>(Func<T, TResult> map)
    {
        using var target = Factory();
        return map(target);
    }
}

