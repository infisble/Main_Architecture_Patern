using HexagonalArchitectureDemo.Application.Ports;

namespace HexagonalArchitectureDemo.Application;

public sealed class TransferFundsUseCase(IAccountRepository repository, INotificationPort notification)
{
    public void Execute(string fromId, string toId, decimal amount)
    {
        var from = repository.GetById(fromId);
        var to = repository.GetById(toId);

        from.Debit(amount);
        to.Credit(amount);

        repository.Save(from);
        repository.Save(to);

        notification.Notify($"Transferred {amount:C} from {fromId} to {toId}");
    }
}
