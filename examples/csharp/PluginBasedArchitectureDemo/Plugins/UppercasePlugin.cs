using PluginBasedArchitectureDemo.Contracts;

namespace PluginBasedArchitectureDemo.Plugins;

public sealed class UppercasePlugin : IPlugin
{
    public string Name => "UppercasePlugin";

    public string Execute(string input) => input.ToUpperInvariant();
}
