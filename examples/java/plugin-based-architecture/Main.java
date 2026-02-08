import java.util.ArrayList;
import java.util.List;

public class Main {
    interface Plugin {
        String name();
        String execute(String input);
    }

    static class UppercasePlugin implements Plugin {
        public String name() { return "UppercasePlugin"; }
        public String execute(String input) { return input.toUpperCase(); }
    }

    static class ReversePlugin implements Plugin {
        public String name() { return "ReversePlugin"; }
        public String execute(String input) {
            return new StringBuilder(input).reverse().toString();
        }
    }

    static class PluginHost {
        private final List<Plugin> plugins = new ArrayList<>();

        void register(Plugin plugin) {
            plugins.add(plugin);
        }

        void runAll(String input) {
            for (Plugin plugin : plugins) {
                System.out.println(plugin.name() + ": " + plugin.execute(input));
            }
        }
    }

    public static void main(String[] args) {
        PluginHost host = new PluginHost();
        host.register(new UppercasePlugin());
        host.register(new ReversePlugin());

        System.out.println("Java plugin-based architecture demo");
        host.runAll("Architecture");
    }
}
