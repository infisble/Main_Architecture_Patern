using HexagonalArchitectureDemo.Adapters;
using HexagonalArchitectureDemo.Application;

var repository = new InMemoryAccountRepository();
var notification = new ConsoleNotificationAdapter();
var transfer = new TransferFundsUseCase(repository, notification);

Console.WriteLine("Hexagonal architecture demo");
Console.WriteLine("Balances before transfer:");
foreach (var account in repository.Snapshot())
{
    Console.WriteLine($"- {account.Id}: {account.Balance:C}");
}

transfer.Execute("A-100", "B-200", 150m);

Console.WriteLine("Balances after transfer:");
foreach (var account in repository.Snapshot())
{
    Console.WriteLine($"- {account.Id}: {account.Balance:C}");
}
