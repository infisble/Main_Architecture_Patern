using HexagonalArchitectureDemo.Domain;

namespace HexagonalArchitectureDemo.Application.Ports;

public interface IAccountRepository
{
    Account GetById(string id);
    void Save(Account account);
}
