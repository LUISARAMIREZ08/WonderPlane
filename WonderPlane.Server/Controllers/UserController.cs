using WonderPlane.Server.Models;
using WonderPlane.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WonderPlane.Server.Services;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;



namespace WonderPlane.Server.Controllers;

[Route("api")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly TokenProvider tokenProvider;

    public UserController(ApplicationDbContext context, TokenProvider tokenProvider)
    {
        _context = context;
        this.tokenProvider = tokenProvider;
    }

    [Authorize(Roles = "RegisteredUser")]
    [HttpGet("users")]
    public IActionResult GetAll()
    {
        var users = _context.Users.ToList();
        return Ok(users);
    }

    [HttpPost("register")]
    public async Task<ActionResult<ResponseAPI<User>>> Register(RegisterDTO registerDTO, TokenProvider tokenProvider)
    {
        if (await EmailExists(registerDTO.Email))
            return BadRequest(new ResponseAPI<User> { EsCorrecto = false, Mensaje = "Email is already used" });

        if (await UserExists(registerDTO.UserName))
            return BadRequest(new ResponseAPI<User> { EsCorrecto = false, Mensaje = "User name is already used" });

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
            Address = registerDTO.Address,
            Country = registerDTO.Country,
            Role = UserRole.RegisteredUser,
            Image = registerDTO.Image,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
            PasswordSalt = hmac.Key
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        string token = tokenProvider.Create(user);

        var response = new ResponseAPI<User>
        {
            EsCorrecto = true,
            Mensaje = "User registered successfully",
            Data = user
        };

        return Ok(response);
    }


    [HttpPost("login")]
    public async Task<ActionResult<ResponseAPI<string>>> Login(LoginDTO loginDTO, TokenProvider tokenProvider)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDTO.Email.ToLower());

        if (user == null) return Unauthorized(new ResponseAPI<string> { EsCorrecto=false, Mensaje="El usuario no es valido"});

        using var hmac = new HMACSHA512(user.PasswordSalt!);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

        if (!computedHash.SequenceEqual(user.PasswordHash!))
            return Unauthorized(new ResponseAPI<string> { EsCorrecto=false, Mensaje="La contraseña no es válida"});

        string token = tokenProvider.Create(user);
        
        return Ok(new ResponseAPI<string> { EsCorrecto = true, Mensaje = "Bienvenido", Data = token });
    }   

    private async Task<bool> EmailExists(string Email)
    {
        return await _context.Users.AnyAsync(x => x.Email == Email.ToLower());
    }

    private async Task<bool> UserExists(string UserName)
    {
        return await _context.Users.AnyAsync(x => x.UserName == UserName.ToLower());
    }

}