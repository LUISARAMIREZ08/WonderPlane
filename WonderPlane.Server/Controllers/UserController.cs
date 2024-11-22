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

   
    [HttpGet("user/{id}")]
    public async Task<ActionResult<UserInfo>> GetUserById(int id)
    {
        // Buscar al usuario en la base de datos usando su ID
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        // Si el usuario no existe, retornar 404
        if (user == null)
        {
            return NotFound(new { Message = "User not found" });
        }

        // Retornar el objeto completo del usuario
        return Ok(user);
    }

    [HttpGet("user/email/{email}")]
    public async Task<ActionResult<int>> GetUserIdByEmail(string email)
    {
        // Buscar al usuario por su email en la base de datos
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

        // Si el usuario no existe, retornar 404
        if (user == null)
        {
            return NotFound(new { Message = "User not found" });
        }

        // Retornar el ID del usuario
        return Ok(user.Id);
    }



    [HttpPost("register")]
    public async Task<ActionResult<ResponseAPI<User>>> Register(UserRegisterDto registerDTO, TokenProvider tokenProvider)
    {
        if (await EmailExists(registerDTO.Email))
            return BadRequest(new ResponseAPI<User> { EsCorrecto = false, Mensaje = "Email is already used" });

        if (await UserExists(registerDTO.UserName))

            return BadRequest(new ResponseAPI<User> { EsCorrecto = false, Mensaje = "User name is already used" });

        var hmac = new HMACSHA512();

        var user = new User
        {
            Document = registerDTO.Document,
            UserName = registerDTO.UserName,
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

    [HttpPost("createadmin")]
    public async Task<ActionResult<ResponseAPI<User>>> CreateAdmin(CreateAdminDto registerDTO, TokenProvider tokenProvider)
    {
        if (await EmailExists(registerDTO.Email))
            return BadRequest(new ResponseAPI<User> { EsCorrecto = false, Mensaje = "Email is already used" });

        if (await UserExists(registerDTO.UserName))
            return BadRequest(new ResponseAPI<User> { EsCorrecto = false, Mensaje = "User name is already used" });

        if (string.IsNullOrEmpty(registerDTO.Password))
            return BadRequest(new ResponseAPI<User> { EsCorrecto = false, Mensaje = "Password cannot be null or empty" });

        var hmac = new HMACSHA512();

        var user = new User
        {
            Document = registerDTO.Document,
            UserName = registerDTO.UserName,
            Name = registerDTO.Name,
            LastName = registerDTO.LastName,
            BirthDate = DateTime.Now,
            Gender = string.Empty,
            PhoneNumber = string.Empty,
            Email = registerDTO.Email.ToLower(),
            Address = string.Empty,
            Country = string.Empty,
            Role = UserRole.Admin,
            Image = string.Empty,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
            PasswordSalt = hmac.Key,
            IsActive = false,
            IsSuscribedToNews = true
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

    
    [HttpGet("admins")]
    public async Task<ActionResult<IEnumerable<User>>> GetAdmins()
    {
        var admins = await _context.Users
            .Where(u => u.Role == UserRole.Admin && u.IsActive == true)
            .ToListAsync();

        return Ok(admins);
    }

    [HttpPut("admin/deactivate/{id}")]
    public async Task<IActionResult> DeactivateAdmin(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id && u.Role == UserRole.Admin);

        if (user == null)
            return NotFound(new { Message = "Admin not found" });

        user.IsActive = false;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("user/deactivate/{id}")]
    public async Task<IActionResult> DeactivateUser(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id && u.Role == UserRole.RegisteredUser);

        if (user == null)
            return NotFound(new { Message = "User not found" });

        user.IsActive = false;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpPut("user/update")]
    public async Task<IActionResult> UpdateUserInfo([FromBody] UserInfo userInfo)
    {
        // Buscar al usuario en la base de datos usando su ID
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userInfo.Id);

        // Si el usuario no existe, retornar 404
        if (user == null)
        {
            return NotFound(new { Message = "User not found" });
        }

        // Actualizar los campos permitidos
        user.Name = userInfo.Name;
        user.LastName = userInfo.LastName;
        user.UserName = userInfo.UserName;
        user.Gender = userInfo.Gender;
        user.PhoneNumber = userInfo.PhoneNumber;
        user.Email = userInfo.Email;
        user.Address = userInfo.Address;
        user.Country = userInfo.Country;
        user.BirthDate = userInfo.BirthDate;
        user.IsSuscribedToNews = userInfo.IsSuscribedToNews;
        user.ReciveNotifications = userInfo.ReciveNotifications;
        // Actualizar la imagen si es necesario
        if (!string.IsNullOrEmpty(userInfo.Image))
        {
            user.Image = userInfo.Image;
        }

        try
        {
            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
            return Ok(new { Message = "User information updated successfully" });
        }
        catch (Exception ex)
        {
            // Manejo de errores
            return StatusCode(500, new { Message = "An error occurred while updating the user", Details = ex.Message });
        }
    }


    [HttpPost("login")]
    public async Task<ActionResult<ResponseAPI<string>>> Login(UserLoginDto loginDTO, TokenProvider tokenProvider)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDTO.Email.ToLower());

        if (user == null) return Unauthorized(new ResponseAPI<string> { EsCorrecto=false, Mensaje="El usuario no es valido"});

        // Verifica si el usuario está activo
        if (user.IsActive == false) return Unauthorized(new ResponseAPI<string> { EsCorrecto = false, Mensaje = "El usuario no existe en el sistema" });

        using var hmac = new HMACSHA512(user.PasswordSalt!);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

        if (!computedHash.SequenceEqual(user.PasswordHash!))
            return Unauthorized(new ResponseAPI<string> { EsCorrecto=false, Mensaje="La contraseña no es válida"});

        string token = tokenProvider.Create(user);
        
        return Ok(new ResponseAPI<string> { EsCorrecto = true, Mensaje = "Bienvenido", Data = token });
    }


    [HttpPut("user/changepassword")]
    public async Task<ActionResult<ResponseAPI<string>>> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        Console.WriteLine(changePasswordDto.Id);
        // Buscar al usuario autenticado por su ID
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == changePasswordDto.Id);

        if (user == null)
        {
            return NotFound(new ResponseAPI<string> { EsCorrecto = false, Mensaje = "Usario no encontrado" });
        }

        // Verificar que la contraseña actual proporcionada coincida
        using var hmac = new HMACSHA512(user.PasswordSalt!);
        var currentHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(changePasswordDto.OldPassword));

        if (!currentHash.SequenceEqual(user.PasswordHash!))
        {
            return BadRequest(new ResponseAPI<string> { EsCorrecto = false, Mensaje = "La contraseña actual es incorrecta" });
        }

        // Generar la nueva contraseña
        var newHmac = new HMACSHA512();
        user.PasswordHash = newHmac.ComputeHash(Encoding.UTF8.GetBytes(changePasswordDto.NewPassword));
        user.PasswordSalt = newHmac.Key;

        // Guardar los cambios en la base de datos
        try
        {
            await _context.SaveChangesAsync();
            return Ok(new ResponseAPI<string> { EsCorrecto = true, Mensaje = "Contraseña actualizada satisfactoriamente" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Ha ocurrido un error mientras se actualizaba la contraseña", Details = ex.Message });
        }
    }

    [HttpPut("user/resetpassword")]
    public async Task<ActionResult<ResponseAPI<string>>> ResetPassword([FromBody] UpdatePasswordDto updatePasswordDto)
    {
        // Buscar al usuario por su ID
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == updatePasswordDto.Id);

        if (user == null)
        {
            return NotFound(new ResponseAPI<string> { EsCorrecto = false, Mensaje = "Usuario no encontrado" });
        }

        // Generar la nueva contraseña
        var hmac = new HMACSHA512();
        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(updatePasswordDto.NewPassword));
        user.PasswordSalt = hmac.Key;
        user.IsActive = true;   // Activar el usuario

        // Guardar los cambios en la base de datos
        try
        {
            await _context.SaveChangesAsync();
            return Ok(new ResponseAPI<string> { EsCorrecto = true, Mensaje = "Contraseña restablecida satisfactoriamente" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResponseAPI<string> { EsCorrecto = false, Mensaje = "Error al restablecer la contraseña", Details = ex.Message});
        }
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