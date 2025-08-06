using Data.Entities.User;

namespace Data.Repository;

public interface IUserService
{
    public Task<User> GetUserByUserName(string userName);
    public Task<bool> Login(string username, string password);
    public Task AddUser(User user);
    public Task<bool> CheckUserNameExists(string username);
    public Task<bool> CheckEmailExists(string emailAddress);
    public Task<bool> CheckMobileExists(string mobile);
    public Task<User> EditProfile(User user,string firstUserName);
    public Task EditPassword(string UserName,string password);
}