using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Shared
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Requerido")]
        [StringLength(10, MinimumLength = 7, ErrorMessage = "El documento de identidad debe tener entre 7 y 10 caracteres.")]
       // [RegularExpression()]
        public required string Document { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, ErrorMessage = "No puede exceder los 20 caracteres.")]
        [RegularExpression(@"^(?!.*\s{2,}).*$", ErrorMessage = "No puede tener espacios seguidos.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, ErrorMessage = "No puede exceder los 20 caracteres.")]
        [RegularExpression(@"^(?!.*\s{2,}).*$", ErrorMessage = "No puede tener espacios seguidos.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, ErrorMessage = "No puede exceder los 20 caracteres.")]
        [RegularExpression(@"^(?!.*\s{2,}).*$", ErrorMessage = "No puede tener espacios seguidos.")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [DataType(DataType.Date)]
        public required DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Requerido.")]
        public required string Gender { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Debe tener 10 dígitos.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de teléfono debe contener solo dígitos.")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico no válido.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El correo debe seguir el formato abcd@algo.com")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(40, ErrorMessage = "No puede exceder los 40 caracteres.")]
        [RegularExpression(@"^(?!.*\s{2,}).*$", ErrorMessage = "No puede tener espacios seguidos.")]
        public required string Address { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, ErrorMessage = "No puede exceder los 20 caracteres.")]
        [RegularExpression(@"^(?!.*\s{2,}).*$", ErrorMessage = "No puede tener espacios seguidos.")]
        public required string Country { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres y no puede exceder los 50 caracteres.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])\S{8,50}$",
            ErrorMessage = "Debe tener entre 8 y 50 caracteres, al menos un dígito, al menos una minúscula y al menos una mayúscula.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Requerida")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public required string ConfirmPassword { get; set; }

        public string? Image { get; set; }
    }

}
