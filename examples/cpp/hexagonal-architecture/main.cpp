#include <iostream>
#include <stdexcept>
#include <string>
#include <unordered_map>
#include <utility>

class Account {
public:
    Account(std::string id, double balance) : id_(std::move(id)), balance_(balance) {}

    const std::string& id() const { return id_; }
    double balance() const { return balance_; }

    void debit(double amount) {
        if (amount <= 0 || balance_ < amount) {
            throw std::invalid_argument("Invalid debit");
        }
        balance_ -= amount;
    }

    void credit(double amount) {
        if (amount <= 0) {
            throw std::invalid_argument("Invalid credit");
        }
        balance_ += amount;
    }

private:
    std::string id_;
    double balance_;
};

class IAccountRepositoryPort {
public:
    virtual ~IAccountRepositoryPort() = default;
    virtual Account& getById(const std::string& id) = 0;
    virtual void save(const Account& account) = 0;
};

class INotificationPort {
public:
    virtual ~INotificationPort() = default;
    virtual void notify(const std::string& message) const = 0;
};

class InMemoryAccountRepository : public IAccountRepositoryPort {
public:
    InMemoryAccountRepository() {
        accounts_.emplace("A-100", Account("A-100", 1200));
        accounts_.emplace("B-200", Account("B-200", 250));
    }

    Account& getById(const std::string& id) override {
        auto it = accounts_.find(id);
        if (it == accounts_.end()) {
            throw std::invalid_argument("Account not found: " + id);
        }
        return it->second;
    }

    void save(const Account& account) override {
        accounts_.insert_or_assign(account.id(), account);
    }

private:
    std::unordered_map<std::string, Account> accounts_;
};

class ConsoleNotificationAdapter : public INotificationPort {
public:
    void notify(const std::string& message) const override {
        std::cout << "[NotificationAdapter] " << message << "\n";
    }
};

class TransferFundsUseCase {
public:
    TransferFundsUseCase(IAccountRepositoryPort& repository, const INotificationPort& notification)
        : repository_(repository), notification_(notification) {}

    void execute(const std::string& fromId, const std::string& toId, double amount) {
        auto& from = repository_.getById(fromId);
        auto& to = repository_.getById(toId);

        from.debit(amount);
        to.credit(amount);

        repository_.save(from);
        repository_.save(to);

        notification_.notify("Transferred " + std::to_string(amount) + " from " + fromId + " to " + toId);
    }

private:
    IAccountRepositoryPort& repository_;
    const INotificationPort& notification_;
};

int main() {
    InMemoryAccountRepository repository;
    ConsoleNotificationAdapter notification;
    TransferFundsUseCase transfer(repository, notification);

    std::cout << "C++ hexagonal architecture demo\n";
    std::cout << "Before: A-100=" << repository.getById("A-100").balance()
              << ", B-200=" << repository.getById("B-200").balance() << "\n";

    transfer.execute("A-100", "B-200", 150);

    std::cout << "After : A-100=" << repository.getById("A-100").balance()
              << ", B-200=" << repository.getById("B-200").balance() << "\n";
    return 0;
}
