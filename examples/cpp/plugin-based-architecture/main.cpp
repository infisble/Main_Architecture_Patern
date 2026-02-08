#include <algorithm>
#include <cctype>
#include <iostream>
#include <memory>
#include <string>
#include <vector>

class IPlugin {
public:
    virtual ~IPlugin() = default;
    virtual std::string name() const = 0;
    virtual std::string execute(const std::string& input) const = 0;
};

class UppercasePlugin : public IPlugin {
public:
    std::string name() const override { return "UppercasePlugin"; }

    std::string execute(const std::string& input) const override {
        std::string out = input;
        std::transform(out.begin(), out.end(), out.begin(),
                       [](unsigned char c) { return static_cast<char>(std::toupper(c)); });
        return out;
    }
};

class ReversePlugin : public IPlugin {
public:
    std::string name() const override { return "ReversePlugin"; }

    std::string execute(const std::string& input) const override {
        std::string out = input;
        std::reverse(out.begin(), out.end());
        return out;
    }
};

class PluginHost {
public:
    void registerPlugin(std::unique_ptr<IPlugin> plugin) {
        plugins_.push_back(std::move(plugin));
    }

    void runAll(const std::string& input) const {
        for (const auto& plugin : plugins_) {
            std::cout << plugin->name() << ": " << plugin->execute(input) << '\n';
        }
    }

private:
    std::vector<std::unique_ptr<IPlugin>> plugins_;
};

int main() {
    PluginHost host;
    host.registerPlugin(std::make_unique<UppercasePlugin>());
    host.registerPlugin(std::make_unique<ReversePlugin>());

    std::cout << "C++ plugin-based architecture demo\n";
    host.runAll("Architecture");

    return 0;
}
