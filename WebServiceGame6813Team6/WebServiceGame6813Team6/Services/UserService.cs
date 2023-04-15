namespace WebServiceGame6813Team6.Services;

using ServiceDb.Models;
using ServiceDb.Data;
using WebServiceGame6813Team6.Controllers;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

public interface IUserService
{
    Task<User> Authenticate(string username, string password);
    Task<IEnumerable<User>> GetAll();
}

public class UserService : IUserService
{
    private ServiceDbContext _context;

    public UserService (ServiceDbContext context)
    {
        _context = context;
    }

    public async Task<User> Authenticate(string username, string password)
    {
        // wrapped in "await Task.Run" to mimic fetching user from a db
        var user = await Task.Run(() => _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password));

        // on auth fail: null is returned because user is not found
        // on auth success: user object is returned
        return user;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        // wrapped in "await Task.Run" to mimic fetching users from a db
        return await Task.Run(() => _context.Users);
    }
}
