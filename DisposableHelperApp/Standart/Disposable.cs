namespace DisposableHelperApp.Standart;

public static class Disposable
{
    public static DisposableOfStandart<T> Of<T>(Func<T> factory) where T : IDisposable
    {
        return new(factory);
    }
}