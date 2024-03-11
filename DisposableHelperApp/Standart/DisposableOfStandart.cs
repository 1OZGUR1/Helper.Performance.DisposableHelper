namespace DisposableHelperApp.Standart;

public class DisposableOfStandart<T>
    where T : IDisposable
{
    private Func<T> Factory { get; }
    internal DisposableOfStandart(Func<T> factory) => Factory = factory;
    public void Use(Action<T> action)
    {
        using T target = Factory();
        action(target);
    }
    public TResult Use<TResult>(Func<T, TResult> map)
    {
        using T target = Factory();
        return map(target);
    }
}

