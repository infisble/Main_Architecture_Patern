#include <algorithm>
#include <cctype>
#include <iostream>
#include <string>

class Server {
public:
    std::string handle(const std::string& payload) const {
        std::string response = payload;
        std::transform(response.begin(), response.end(), response.begin(),
                       [](unsigned char c) { return static_cast<char>(std::toupper(c)); });
        return "ACK: " + response;
    }
};

class Network {
public:
    explicit Network(const Server& server) : server_(server) {}

    std::string send(const std::string& payload) const {
        return server_.handle(payload);
    }

private:
    const Server& server_;
};

class Client {
public:
    explicit Client(const Network& network) : network_(network) {}

    std::string request(const std::string& payload) const {
        return network_.send(payload);
    }

private:
    const Network& network_;
};

int main() {
    Server server;
    Network network(server);
    Client clientA(network);
    Client clientB(network);

    std::cout << "C++ client-server architecture demo\n";
    std::cout << "[Client A] " << clientA.request("hello from client A") << "\n";
    std::cout << "[Client B] " << clientB.request("hello from client B") << "\n";
    return 0;
}
