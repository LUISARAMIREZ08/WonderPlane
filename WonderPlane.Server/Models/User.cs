using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WonderPlane.Server.Models;

public class User : IdentityUser
{   

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    public UserGender? Gender { get; set; }

    public byte[]? Image { get; set; }

    public ICollection<Forum>? Forums { get; } = new List<Forum>();

    public ICollection<Message>? Messages { get; } = new List<Message>();
}


public enum UserGender
{
    Male,
    Female,
    Other
}

// public enum UserRole
// {
//     Admin,
//     RegisteredUser,
//     Root
// }