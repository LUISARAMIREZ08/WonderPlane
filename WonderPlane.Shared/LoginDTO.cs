using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Shared;

public class LoginDTO
{
    [Required(ErrorMessage = "Requerido")]
    [EmailAddress(ErrorMessage = "Formato de correo electrónico no válido.")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El correo debe seguir el formato abcd@algo.com")]
    public required string Email { get; set; }
    [Required(ErrorMessage = "Requerido")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres y no puede exceder los 50 caracteres.")]
    [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])\S{8,50}$",
            ErrorMessage = "Contraseña no válida.")]
    public required string Password { get; set; }
}
