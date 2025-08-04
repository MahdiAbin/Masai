using Core.Security;
using Data.Context;
using Data.Entities.User;
using Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class UserSerivce:IUserService
{
    private MasaiContxet _contxet;

    public UserSerivce(MasaiContxet contxet)
    {
        _contxet = contxet;
    }

    public async Task<bool> Login(string username, string password)
    {
        return await _contxet.Users.AnyAsync(u=>u.UserName == username && u.Password == password);
    }

    public async Task AddUser(User user)
    {
        user.Password=user.Password.Hash();
        await _contxet.Users.AddAsync(user);
        await _contxet.SaveChangesAsync();
    }
}