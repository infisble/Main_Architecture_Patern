namespace HexagonalArchitectureDemo.Domain;

public sealed class Account
{
    public Account(string id, decimal balance)
    {
        Id = id;
        Balance = balance;
    }

    public string Id { get; }
    public decimal Balance { get; private set; }

    public void Debit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        if (Balance < amount)
        {
            throw new InvalidOperationException("Insufficient funds");
        }

        Balance -= amount;
    }

    public void Credit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        Balance += amount;
    }
}
