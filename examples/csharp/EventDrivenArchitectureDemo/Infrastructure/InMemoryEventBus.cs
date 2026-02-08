namespace EventDrivenArchitectureDemo.Infrastructure;

public interface IEventBus
{
    void Subscribe<TEvent>(Action<TEvent> handler);
    void Publish<TEvent>(TEvent eventData);
}

public sealed class InMemoryEventBus : IEventBus
{
    private readonly Dictionary<Type, List<Delegate>> _handlers = [];

    public void Subscribe<TEvent>(Action<TEvent> handler)
    {
        var key = typeof(TEvent);
        if (!_handlers.ContainsKey(key))
        {
            _handlers[key] = [];
        }

        _handlers[key].Add(handler);
    }

    public void Publish<TEvent>(TEvent eventData)
    {
        var key = typeof(TEvent);
        if (!_handlers.TryGetValue(key, out var handlers))
        {
            return;
        }

        foreach (var handler in handlers.Cast<Action<TEvent>>())
        {
            handler(eventData);
        }
    }
}
