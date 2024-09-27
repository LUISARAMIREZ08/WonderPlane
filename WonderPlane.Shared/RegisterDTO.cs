using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Shared;

public class RegisterDTO
{
    [StringLength(50)]
    public required string UserName { get; set; }

    [StringLength(50)]
    public required string Name { get; set; }

    [StringLength(50)]
    public required string LastName { get; set; }

    [DataType(DataType.Date)]
    public required DateTime BirthDate { get; set; }

    public required string Gender { get; set; }

    [StringLength(50)]
    public required string PhoneNumber { get; set; }

    [EmailAddress]
    public required string Email { get; set; }
    [StringLength(50)]
    public required string Address { get; set; }
    [StringLength(50)]
    public required string Country { get; set; }

    [Required]
    [StringLength(50)]
    public required string Password { get; set; }
}
