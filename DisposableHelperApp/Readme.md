
# Disposable Pattern

The Disposable Pattern is a design pattern in object-oriented programming for resource management. In this pattern, a resource is held by an object and released by calling a method, typically named Dispose, Close, or Free. This prevents resources from being unnecessarily occupied and improves program performance.

The Disposable Pattern is particularly useful in the following cases:

Managing unmanaged resources: In some programming languages, such as .NET, the garbage collector only cleans up managed resources. The Disposable Pattern is used to clean up unmanaged resources such as files and network connections.
Releasing resources at a specific time: The lifetime of some resources may depend on the program flow. With the Disposable Pattern, these resources can be released when they are no longer needed.
The Disposable Pattern is commonly used with classes that implement the IDisposable interface. This interface contains a single Dispose method. The necessary operations for releasing the held resources are written in the Dispose method.

If you are using .NET, there are many classes that implement the IDisposable interface. For example, the FileStream class represents a file resource and implements the IDisposable interface. Therefore, you need to call the Dispose method and release the file resources when you are finished using the file.

The Disposable Pattern simplifies resource management and makes programs more reliable and performant.

## CSharp Disposable Pattern Helper and Example Usage

The following code block is a structure that uses the Disposable Pattern in the C# language. This pattern is used to manage resources (memory, file openers, network connections, etc.) that need to be released after use.

Classes in the Code Block:

- DisposableHelperSimple<T>: This class implements the IDisposable interface and holds a value and a function for the cleanup process.
- Disposable: This static class creates a DisposableOfStandart<T> class instance using the Of method.
- DisposableHelperSimple<T> Class:

- Value: This property shows the held value.
- _cleanup: This private variable holds the function that performs the cleanup process.
- DisposableHelperSimple(T value, Action cleanup): This constructor creates the object by taking the value and the cleanup function.
- Dispose(): This method calls the cleanup function and releases the resources.

### Disposable Class:

- Of<T>(Func<T> factory): This static method creates an IDisposable object from the factory function and returns a DisposableOfStandart<T> class instance.

### DisposableOfStandart<T> Class:

- Factory: This property holds the value creation function.
- DisposableOfStandart(Func<T> factory): This constructor creates the object by taking the value creation function.
- Use(Action<T> action): This method creates an IDisposable object, calls the action delegate on the object, and makes the object disposable (Dispose method is called).
- Use<TResult>(Func<T, TResult> map): This method creates an IDisposable object, calls the map delegate on the object, returns the result, and makes the object disposable (Dispose method is called).

### Advantages:

- The Disposable pattern simplifies resource management and reduces the risk of memory leaks.
- It increases the readability and understandability of the code.
- It requires less code than try-finally blocks.

### Example Usages

#### Usage One
```Csharp
using System;

public class Program
{
    public static void Main(string[] args)
    {
        using (var disposable = Disposable.Of(() => new FileStream("file.txt", FileMode.Open)))
        {
            // File operations are performed.
        }

        // The file is automatically closed and resources are released.
    }
}
```
#### Usage Two
```Csharp
public interface IRepository<T> where T : IDisposable
{
    T GetById(int id);
}

public class Repository<T> : IRepository<T> where T : IDisposable
{
    private readonly Func<T> _factory;

    public Repository(Func<T> factory)
    {
        _factory = factory;
    }

    public T GetById(int id)
    {
        return Disposable.Of(_factory).Use(t => t.GetById(id));
    }
}

public class PersonRepository : Repository<Person>
{
    public PersonRepository() : base(() => new Person())
    {
    }
}

public class Person : IDisposable
{
    public int Id { get; set; }

    public string Name { get; set; }

    public void Dispose()
    {
        // Dispose işlemleri
    }

    public Person GetById(int id)
    {
        // Veritabanından kişiyi getirme işlemleri
        return new Person { Id = id, Name = "Kişi Adı" };
    }
}

// Simple
using (var repository = new PersonRepository())
{
    var person = repository.GetById(1);
    Console.WriteLine(person.Name);
}
```
#### Usage Three
```Csharp
public interface IRepository<T> where T : IDisposable
{
    T GetById(int id);
}

public class DisposableOfStandartRepository<T> : IRepository<T> where T : IDisposable
{
    private readonly Func<T> _factory;

    public DisposableOfStandartRepository(Func<T> factory)
    {
        _factory = factory;
    }

    public T GetById(int id)
    {
        return Disposable.Of(_factory).Use(t => t.GetById(id));
    }
}

public class PersonRepository : DisposableOfStandartRepository<Person>
{
    public PersonRepository() : base(() => new Person())
    {
    }
}

public class Person : IDisposable
{
    public int Id { get; set; }

    public string Name { get; set; }

    public void Dispose()
    {
        // Dispose işlemleri
    }

    public Person GetById(int id)
    {
        // Veritabanından kişiyi getirme işlemleri
        return new Person { Id = id, Name = "Kişi Adı" };
    }
}

// Kullanım örneği
using (var repository = new PersonRepository())
{
    var person = repository.GetById(1);
    Console.WriteLine(person.Name);
}
```
#### Simple Disposing Methods
- For Database connections:
```Csharp
public void Dispose()
{
    if (_connection != null)
    {
        _connection.Close();
        _connection = null;
    }
}

```
- For Write Files:
```Csharp
public void Dispose()
{
    if (_stream != null)
    {
        _stream.Close();
        _stream = null;
    }
}

```

- For Garbage Collection:
```Csharp

public void Dispose()
{
    GC.Collect();
    GC.WaitForPendingFinalizers();
}


```