using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Shared
{
    public class ChangePasswordDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres y no puede exceder los 50 caracteres.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])\S{8,50}$",
           ErrorMessage = "Debe tener entre 8 y 50 caracteres, al menos un dígito, al menos una minúscula y al menos una mayúscula.")]
        public required string OldPassword { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres y no puede exceder los 50 caracteres.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])\S{8,50}$",
           ErrorMessage = "Debe tener entre 8 y 50 caracteres, al menos un dígito, al menos una minúscula y al menos una mayúscula.")]
        public required string NewPassword { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        public required string ConfirmNewPassword { get; set; }
    }
}
