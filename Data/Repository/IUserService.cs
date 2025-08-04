using Data.Entities.User;

namespace Data.Repository;

public interface IUserService
{
    public Task<bool> Login(string username, string password);
    public Task AddUser(User user);
}