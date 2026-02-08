namespace PluginBasedArchitectureDemo.Contracts;

public interface IPlugin
{
    string Name { get; }
    string Execute(string input);
}
