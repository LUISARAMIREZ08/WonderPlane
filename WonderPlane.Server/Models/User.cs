using System.ComponentModel.DataAnnotations;
using WonderPlane.Shared;

namespace WonderPlane.Server.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(20, ErrorMessage = "El nombre de usuario no puede exceder los 20 caracteres.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(20, ErrorMessage = "El nombre no puede exceder los 20 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(20, ErrorMessage = "El apellido no puede exceder los 20 caracteres.")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        public required DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "El género es obligatorio.")]
        public required UserGender Gender { get; set; }

        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [StringLength(10, MinimumLength =10, ErrorMessage = "El número de teléfono debe tener 10 caracteres.")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico no válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(40, ErrorMessage = "La dirección no puede exceder los 40 caracteres.")]
        public required string Address { get; set; }

        [Required(ErrorMessage = "El país es obligatorio.")]
        [StringLength(20, ErrorMessage = "El país no puede exceder los 50 caracteres.")]
        public required string Country { get; set; }

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public string? Image { get; set; }

        public UserRole? Role { get; set; }

        public ICollection<Forum> Forums { get; } = new List<Forum>();
        public ICollection<Message> Messages { get; } = new List<Message>();
    }

    //public enum UserGender
    //{
    //    Male,
    //    Female,
    //    Other
    //}

    public enum UserRole
    {
        Admin,
        RegisteredUser,
        Root
    }
}