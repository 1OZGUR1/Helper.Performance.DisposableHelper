namespace DisposableHelperApp.Standart;

public class DisposableOfStandardAsync<T>
    where T : IAsyncDisposable
{
    internal DisposableOfStandardAsync(Func<T> factory)
    {
        Factory = factory;
    }

    private Func<T> Factory { get; }

    public async Task UseAsync(Action<T> action)
    {
        await using (var target = Factory()) // No await here
        {
            action(target);
        }
    }

    public async Task<TResult> UseAsync<TResult>(Func<T, Task<TResult>> map)
    {
        await using (var target = Factory()) // No await here
        {
            return await map(target).ConfigureAwait(false);
        }
    }
}