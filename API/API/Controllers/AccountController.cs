using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly DataContext _context;

    public AccountController(DataContext context)
    {
        _context = context;
    }

    [HttpPost("register")] // api/account/register
    public async Task<ActionResult<AppUser>> Register(RegisterDto request)
    {
        if (await UserExists(request.Username))
            return BadRequest("Incorrect username");
        
        using var hmac = new HMACSHA512();

        var user = new AppUser()
        {
            UserName = request.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
            PasswordSalt = hmac.Key
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }
    
    [HttpPost("login")] // api/account/login
    public async Task<ActionResult<AppUser>> Login(LoginDto request)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == request.Username);

        if (user == null)
            return Unauthorized("User does not exists");

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
                return Unauthorized("Incorrect password");
        }

        return user;
    }

    private async Task<Boolean> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
}