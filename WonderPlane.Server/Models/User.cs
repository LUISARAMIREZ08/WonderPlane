using System.ComponentModel.DataAnnotations;
using WonderPlane.Shared;

namespace WonderPlane.Server.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El documento de identidad es obligatorio.")]
        [StringLength(10, MinimumLength = 7, ErrorMessage = "El documento de identidad debe tener entre 7 y 10 caracteres.")]
        public required string Document { get; set; }

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

        public string? Gender { get; set; }

        [StringLength(10, MinimumLength =10, ErrorMessage = "El número de teléfono debe tener 10 caracteres.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico no válido.")]
        public required string Email { get; set; }

        [StringLength(40, ErrorMessage = "La dirección no puede exceder los 40 caracteres.")]
        public string? Address { get; set; }

        [StringLength(20, ErrorMessage = "El país no puede exceder los 50 caracteres.")]
        public string? Country { get; set; }

        public bool? IsSuscribedToNews { get; set; } = false;

        public bool? ReciveNotifications { get; set; } = false;

        public bool? IsActive { get; set; } = true;

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public string? Image { get; set; }

        public UserRole? Role { get; set; }

        public int? TravelerId { get; set; }
        public Traveler? Traveler { get; set; }

        public ICollection<Forum> Forums { get; } = new List<Forum>();
        public ICollection<Message> Messages { get; } = new List<Message>();
        public ICollection<Purchase> Purchases { get; } = new List<Purchase>();
        public ICollection<Reservation> Reservations { get; } = new List<Reservation>();
        public ICollection<Search> Searches { get; } = new List<Search>();
        public ICollection<Card> Cards { get; } = new List<Card>();
    }

    public enum UserRole
    {
        Admin,
        RegisteredUser,
        Root
    }
}