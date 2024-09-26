using WonderPlane.Server.Models;
using Microsoft.AspNetCore.Mvc;

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
    [Route("server/users")]
    public IActionResult GetAll()
    {
        var users = _context.Users.ToList();
        return Ok(users);
    }

    [HttpPost]
    [Route("server/users")]
    public async Task<IActionResult> OnPostAsync()
    {
        var emptyUser = new User();
        if (await TryUpdateModelAsync<User>(
            emptyUser,
            "user",
            u=> u.Id, u => u.Name, u => u.LastName, u => u.Email, u => u.PasswordHash))
        {
            _context.Users.Add(emptyUser);
            await _context.SaveChangesAsync();
            return Ok(new { UserId = emptyUser.Id, Message = "User registered successfully!" });
        }
        return BadRequest(new { Message = "User registration failed!" });
    }

}