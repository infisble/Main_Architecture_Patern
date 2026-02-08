using PluginBasedArchitectureDemo.Contracts;

namespace PluginBasedArchitectureDemo.Plugins;

public sealed class ReversePlugin : IPlugin
{
    public string Name => "ReversePlugin";

    public string Execute(string input)
    {
        var chars = input.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
}
