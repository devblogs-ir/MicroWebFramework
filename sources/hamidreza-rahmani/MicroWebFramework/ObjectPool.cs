using System.Net;

namespace MicroWebFramework;

public class ObjectPool<T>
{
    private readonly Func<T> _createObject;
    private readonly Stack<T> _objects;

    public ObjectPool(Func<T> createObject, int initialSize)
    {
        _createObject = createObject;
        _objects = new Stack<T>(initialSize);

        for (var i = 0; i < initialSize; i++) _objects.Push(createObject());
    }

    public T Get()
    {
        lock (_objects)
        {
            return _objects.Count > 0 ? _objects.Pop() : _createObject();
        }
    }

    public void Return(T obj)
    {
        lock (_objects)
        {
            _objects.Push(obj);
        }
    }
}

public class StateObject
{
    public HttpListenerContext Context { get; set; }
}