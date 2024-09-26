using WonderPlane.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;


namespace WonderPlane.Server.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("account/[controller]")]
    public IActionResult GetAll()
    {
        var users = _context.Users.ToList();
        return Ok(users);
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(
        string username, 
        string name,
        string lastname,
        DateTime birthday,
        UserGender gender,
        string phonenumber,
        string email,
        string password,
        UserRole role
        )
    {
        var hmac = new HMACSHA512();

        var user = new User
        {
            UserName = username,
            Name = name,
            LastName = lastname,
            BirthDate = birthday,
            Gender = gender,
            PhoneNumber = phonenumber,
            Email = email,
            Role = role,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            PasswordSalt = hmac.Key,

        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

}