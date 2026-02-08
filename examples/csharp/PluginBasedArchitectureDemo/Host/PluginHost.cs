using PluginBasedArchitectureDemo.Contracts;
using System.Reflection;

namespace PluginBasedArchitectureDemo.Host;

public sealed class PluginHost
{
    private readonly List<IPlugin> _plugins = [];

    public void DiscoverFromAssembly(Assembly assembly)
    {
        var pluginTypes = assembly
            .GetTypes()
            .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in pluginTypes)
        {
            if (Activator.CreateInstance(type) is IPlugin plugin)
            {
                _plugins.Add(plugin);
            }
        }
    }

    public IReadOnlyList<string> ExecuteAll(string input)
    {
        return _plugins
            .Select(plugin => $"{plugin.Name}: {plugin.Execute(input)}")
            .ToList();
    }
}
