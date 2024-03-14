namespace DisposableHelperApp.Standart;

public static class DisposableAsync
{
    public static async Task<DisposableOfStandardAsync<T>> OfAsync<T>(Func<T> factory) where T : IAsyncDisposable
    {
        return new(factory);
    }
}