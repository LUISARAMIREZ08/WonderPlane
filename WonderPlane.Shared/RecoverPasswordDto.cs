

using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Shared
{
    public class RecoverPasswordDto
    {
        [EmailAddress(ErrorMessage = "Formato de correo electrónico no válido.")]
        [RegularExpression(@"^(?!.*[Rr]oot)[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El correo no puede contener 'root' o 'Root' y debe seguir el formato abcd@algo.com")]
        public string Email { get; set; } = string.Empty;

        public int? InputCode { get; set; }
    }
}
