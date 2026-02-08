using PluginBasedArchitectureDemo.Host;
using System.Reflection;

var host = new PluginHost();
host.DiscoverFromAssembly(Assembly.GetExecutingAssembly());

Console.WriteLine("Plugin-based architecture demo");
foreach (var output in host.ExecuteAll("Architecture"))
{
    Console.WriteLine($"- {output}");
}
