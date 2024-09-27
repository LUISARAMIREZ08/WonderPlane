using WonderPlane.Server.Models;
using WonderPlane.Server.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WonderPlane.Server.Services;
using Microsoft.AspNetCore.Authorization;



namespace WonderPlane.Server.Controllers;

[Route("api")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "RegisteredUser")]
    [HttpGet("users")]
    public IActionResult GetAll()
    {
        var users = _context.Users.ToList();
        return Ok(users);
    }

    [HttpPost("register")]
    public async Task<ActionResult<string>> Register(RegisterDTO registerDTO, TokenProvider tokenProvider)
    {
        if (await UserExists(registerDTO.Email)) return BadRequest("Email is already used");

        var hmac = new HMACSHA512();

        var user = new User
        {
            UserName = registerDTO.UserName.ToLower(),
            Name = registerDTO.Name,
            LastName = registerDTO.LastName,
            BirthDate = registerDTO.BirthDate,
            Gender = registerDTO.Gender,
            PhoneNumber = registerDTO.PhoneNumber,
            Email = registerDTO.Email.ToLower(),
            Role = UserRole.RegisteredUser,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
            PasswordSalt = hmac.Key,

        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        string token = tokenProvider.Create(user);

        return token;
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginDTO loginDTO, TokenProvider tokenProvider)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDTO.Email.ToLower());

        if (user == null) return Unauthorized("Invalid email");

        using var hmac = new HMACSHA512(user.PasswordSalt!);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

        if (!computedHash.SequenceEqual(user.PasswordHash!))
            return Unauthorized("Invalid password");

        string token = tokenProvider.Create(user);
        
        return token;
    }   

    private async Task<bool> UserExists(string Email)
    {
        return await _context.Users.AnyAsync(x => x.Email == Email.ToLower());
    }

}