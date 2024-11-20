using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Shared
{
    public class UserInfo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(10, MinimumLength = 7, ErrorMessage = "El documento de identidad debe tener entre 7 y 10 caracteres.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de documento debe contener solo dígitos.")]
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

        [StringLength(10, ErrorMessage = "Debe tener máximo 10 caracteres.")]
        public string? Gender { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "Debe tener 10 dígitos.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de teléfono debe contener solo dígitos.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico no válido.")]
        [RegularExpression(@"^(?!.*[Rr]oot)[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El correo no puede contener 'root' o 'Root' y debe seguir el formato abcd@algo.com")]
        public required string Email { get; set; }

        [StringLength(40, ErrorMessage = "No puede exceder los 40 caracteres.")]
        [RegularExpression(@"^(?!.*\s{2,}).*$", ErrorMessage = "No puede tener espacios seguidos.")]
        public string? Address { get; set; }

        [StringLength(30, ErrorMessage = "No puede exceder los 30 caracteres.")]
        [RegularExpression(@"^(?!.*\s{2,}).*$", ErrorMessage = "No puede tener espacios seguidos.")]
        public string? Country { get; set; }

        public bool? IsSuscribedToNews { get; set; } = false;

        public bool? ReciveNotifications { get; set; } = false;

        public bool? IsActive { get; set; } = true;

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public string? Image { get; set; }

        public decimal? Balance { get; set; }

    }
}
