using System.ComponentModel.DataAnnotations;
namespace WonderPlane.Server.Models;
public class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public required string UserName { get; set; }

    [StringLength(50)]
    public required string Name { get; set; }

    [StringLength(50)]
    public required string LastName { get; set; }

    [DataType(DataType.Date)]
    public required DateTime BirthDate { get; set; }

    public required UserGender Gender { get; set; }

    [StringLength(10)]
    public required string PhoneNumber { get; set; }

    [EmailAddress]
    public required string Email { get; set; }
    [StringLength(50)]
    public required string Address { get; set; }
    [StringLength(50)]
    public required string Country { get; set; }
    public byte[]? PasswordHash { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public byte[]? Image { get; set; }

    public UserRole? Role { get; set; }

    public ICollection<Forum> Forums { get; } = new List<Forum>();
    public ICollection<Message> Messages { get; } = new List<Message>();
}
public enum UserGender
{
    Male,
    Female,
    Other
}
public enum UserRole
{
    Admin,
    RegisteredUser,
    Root
}