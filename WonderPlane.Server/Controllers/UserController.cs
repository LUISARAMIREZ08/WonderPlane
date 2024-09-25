using WonderPlane.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace WonderPlane.Server.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDBContext _context;

    public UserController(ApplicationDBContext context)
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
}