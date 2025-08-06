using Core.Security;
using Data.Context;
using Data.Entities.User;
using Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class UserSerivce : IUserService
{
    private MasaiContxet _contxet;

    public UserSerivce(MasaiContxet contxet)
    {
        _contxet = contxet;
    }

    public async Task<User> GetUserByUserName(string userName)
    {
        return await _contxet
            .Users
            .SingleAsync(x=>x.UserName==userName);
    }

    public async Task<bool> Login(string username, string password)
    {
        return await _contxet.Users.AnyAsync(u => u.UserName == username && u.Password == password);
    }

    public async Task AddUser(User user)
    {
        user.Password = user.Password.Hash();
        await _contxet.Users.AddAsync(user);
        await _contxet.SaveChangesAsync();
    }

    public async Task<bool> CheckUserNameExists(string username)
    {
        return await _contxet.Users.AnyAsync(x => x.UserName == username);
    }

    public async Task<bool> CheckEmailExists(string emailAddress)
    {
        return await _contxet.Users.AnyAsync(x => x.UserName == emailAddress);
    }

    public async Task<bool> CheckMobileExists(string mobile)
    {
        return await _contxet.Users.AnyAsync(x => x.UserName == mobile);
    }

    public async Task<User> EditProfile(User user,string firstUserName)
    {
        var users=await GetUserByUserName(firstUserName);
        users.UserName = user.UserName;
        users.Email=user.Email;
        users.FName=user.FName;
        users.LName=user.LName;
        users.MobileNumber=user.MobileNumber;
        users.Job=user.Job;
        users.NationalCode=user.NationalCode;
        users.Sheba = user.Sheba;
        users.NumberCart = user.NumberCart;
        _contxet.Users.Update(users);
        await _contxet.SaveChangesAsync();
        return users;
    }

    public async Task EditPassword(string UserName, string password)
    {
        var user =await GetUserByUserName(UserName);
        user.Password =password.Hash();
        _contxet.Users.Update(user);
        await _contxet.SaveChangesAsync();
    }
}