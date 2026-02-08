import java.util.HashMap;
import java.util.Map;

public class Main {
    static class Account {
        private final String id;
        private double balance;

        Account(String id, double balance) {
            this.id = id;
            this.balance = balance;
        }

        String id() {
            return id;
        }

        double balance() {
            return balance;
        }

        void debit(double amount) {
            if (amount <= 0 || balance < amount) {
                throw new IllegalArgumentException("Invalid debit");
            }
            balance -= amount;
        }

        void credit(double amount) {
            if (amount <= 0) {
                throw new IllegalArgumentException("Invalid credit");
            }
            balance += amount;
        }
    }

    interface AccountRepositoryPort {
        Account getById(String id);
        void save(Account account);
    }

    interface NotificationPort {
        void notify(String message);
    }

    static class InMemoryAccountRepository implements AccountRepositoryPort {
        private final Map<String, Account> accounts = new HashMap<>();

        InMemoryAccountRepository() {
            accounts.put("A-100", new Account("A-100", 1200));
            accounts.put("B-200", new Account("B-200", 250));
        }

        public Account getById(String id) {
            if (!accounts.containsKey(id)) {
                throw new IllegalArgumentException("Account not found: " + id);
            }
            return accounts.get(id);
        }

        public void save(Account account) {
            accounts.put(account.id(), account);
        }
    }

    static class ConsoleNotificationAdapter implements NotificationPort {
        public void notify(String message) {
            System.out.println("[NotificationAdapter] " + message);
        }
    }

    static class TransferFundsUseCase {
        private final AccountRepositoryPort repository;
        private final NotificationPort notification;

        TransferFundsUseCase(AccountRepositoryPort repository, NotificationPort notification) {
            this.repository = repository;
            this.notification = notification;
        }

        void execute(String fromId, String toId, double amount) {
            Account from = repository.getById(fromId);
            Account to = repository.getById(toId);

            from.debit(amount);
            to.credit(amount);

            repository.save(from);
            repository.save(to);

            notification.notify("Transferred " + amount + " from " + fromId + " to " + toId);
        }
    }

    public static void main(String[] args) {
        InMemoryAccountRepository repository = new InMemoryAccountRepository();
        ConsoleNotificationAdapter notification = new ConsoleNotificationAdapter();
        TransferFundsUseCase transfer = new TransferFundsUseCase(repository, notification);

        System.out.println("Java hexagonal architecture demo");
        System.out.println("Before: A-100=" + repository.getById("A-100").balance() + ", B-200=" + repository.getById("B-200").balance());

        transfer.execute("A-100", "B-200", 150);

        System.out.println("After : A-100=" + repository.getById("A-100").balance() + ", B-200=" + repository.getById("B-200").balance());
    }
}
