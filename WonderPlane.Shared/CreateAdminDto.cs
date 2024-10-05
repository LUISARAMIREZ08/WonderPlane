using System;
using System.ComponentModel.DataAnnotations;


namespace WonderPlane.Shared
{
    public class CreateAdminDto
    {
        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, ErrorMessage = "No puede exceder los 20 caracteres.")]
        [RegularExpression(@"^(?!.*\s{2,}).*$", ErrorMessage = "No puede tener espacios seguidos.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico no válido.")]
        [RegularExpression(@"^(?!.*[Rr]oot)[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El correo no puede contener 'root' o 'Root' y debe seguir el formato abcd@algo.com")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres y no puede exceder los 50 caracteres.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])\S{8,50}$",
           ErrorMessage = "Debe tener entre 8 y 50 caracteres, al menos un dígito, al menos una minúscula y al menos una mayúscula.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Requerida")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public required string ConfirmPassword { get; set; }
    }
}
