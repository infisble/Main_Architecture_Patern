using HexagonalArchitectureDemo.Application.Ports;
using HexagonalArchitectureDemo.Domain;

namespace HexagonalArchitectureDemo.Adapters;

public sealed class InMemoryAccountRepository : IAccountRepository
{
    private readonly Dictionary<string, Account> _accounts =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["A-100"] = new Account("A-100", 1200m),
            ["B-200"] = new Account("B-200", 250m)
        };

    public Account GetById(string id)
    {
        if (!_accounts.TryGetValue(id, out var account))
        {
            throw new KeyNotFoundException($"Account '{id}' not found");
        }

        return account;
    }

    public void Save(Account account)
    {
        _accounts[account.Id] = account;
    }

    public IReadOnlyCollection<Account> Snapshot() => _accounts.Values.ToList();
}
