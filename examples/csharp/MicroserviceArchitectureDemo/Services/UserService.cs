namespace MicroserviceArchitectureDemo.Services;

public interface IUserService
{
    bool Exists(int userId);
}

public sealed class UserService : IUserService
{
    private readonly HashSet<int> _users = [100, 101, 102];

    public bool Exists(int userId) => _users.Contains(userId);
}
