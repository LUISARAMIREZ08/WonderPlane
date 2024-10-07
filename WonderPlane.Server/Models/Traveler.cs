using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.Models;

public class Traveler
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(20, ErrorMessage = "El nombre no puede exceder los 20 caracteres.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [StringLength(20, ErrorMessage = "El apellido no puede exceder los 20 caracteres.")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "El documento de identidad es obligatorio.")]
    [StringLength(10, MinimumLength = 7, ErrorMessage = "El documento de identidad debe tener entre 7 y 10 caracteres.")]
    public required string Document { get; set; }

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    [DataType(DataType.Date)]
    public required DateTime BirthDate { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "Formato de correo electrónico no válido.")]
    public required string Email { get; set; }

    public int? RegisteredUserId { get; set; }
    public User? RegisteredUser { get; set; }

    public ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}